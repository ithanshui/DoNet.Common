using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoNet.Common.DbUtility
{
    /// <summary>
    /// 对象
    /// 数据库访问
    /// </summary>
    public class JmDB<T> : DbORM
    {
        //对象缓存KEY
        const string ModelCacheKey = "DoNet.Common.DbUtility.JmDB.Model.{0}.";

        private static DBConfig _dbConfig = null;
        /// <summary>
        /// 数据库对象配置
        /// </summary>
        public static DBConfig DbConfig
        {
            get {
                if (_dbConfig == null) { 
                    _dbConfig = new DBConfig();                   
                }
                return _dbConfig; 
            }
        }
        
        private ModelSetting _modelconfig;
        /// <summary>
        /// 对象配置
        /// </summary>
        public ModelSetting Modelconfig
        {
            get
            {
                if (_modelconfig == null) { 
                    _modelconfig = DbConfig.GetModelSettingByContract(Contract);
                    if (_modelconfig == null)
                    {
                        throw new Exception("获取对象配置失败:" + Contract);
                    }
                }
                
                return _modelconfig;
            }
        }

        Type _t;
        /// <summary>
        /// 当前对象的类型
        /// </summary>
        protected Type ThisType
        {
            get
            {
                if (_t == null) _t = typeof(T);
                return _t;
            }
        }

        System.Reflection.PropertyInfo[] _protertyInfos;
        /// <summary>
        /// 当前对象的所有属性
        /// </summary>
        protected System.Reflection.PropertyInfo[] PropertyInfos
        {
            get
            {
                if (_protertyInfos == null)
                    _protertyInfos = Reflection.ClassHelper.GetTypePropertys(ThisType);//获取所有属性
                return _protertyInfos;
            }
        }

        string _contract;
        /// <summary>
        /// 当前对应的类路径
        /// </summary>
        protected string Contract
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_contract))
                {
                    _contract = ThisType.FullName;
                }
                return _contract;
            }
        }

        /// <summary>
        /// 初始化连接
        /// </summary>
        /// <param name="connConfigName"></param>
        protected void InitConn(string connConfigName="")
        {
            var conn = DbConfig.GetConSettingByName(connConfigName);
            if (conn == null)
            {
                throw new Exception("无法获取数据库连接:" + connConfigName);
            }
            base.SetConnection(conn.ProviderName, conn.ConnectionString);//初始化连接
        }

        /// <summary>
        /// 不提供参数黑认会取第一个连接
        /// </summary>
        public JmDB()
        {
            InitConn();
        }

        /// <summary>
        /// 通过数据库连接配置名实例化
        /// </summary>
        /// <param name="connConfigName"></param>
        public JmDB(string connConfigName)
        {
            InitConn(connConfigName);
        }

        /// <summary>
        /// 通过配置路径和数据库连接配置初始化类
        /// </summary>
        /// <param name="configPath"></param>
        /// <param name="connConfigName"></param>
        public JmDB(string configPath, string connConfigName)
        {
            DbConfig.DBConfigPath = configPath;//设置路径

            _dbConfig = null;//重新初始化配置

            InitConn(connConfigName);
        } 

        /// <summary>
        /// 批量更新对象
        /// </summary>
        /// <param name="objs"></param>
        /// <param name="updateConfigName"></param>
        /// <returns></returns>
        public int Update(ICollection<T> objs, string updateConfigName = "")
        {
            var pars = new List<Dictionary<string, object>>();//参数集

            var sqls = new List<string>(); //语句集合

            foreach (var obj in objs)
            {
                var par = new Dictionary<string, object>();
                var sql = CreateUpdateSql(obj, par);//生成参数和SQL

                sqls.Add(sql);
                pars.Add(par);
            }

            var re = base.ExecuteSql(sqls, pars);//事务执行
          
            return re;
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Delete(T obj)
        { 
            var pars=new Dictionary<string,object>();
            //生成删除语句
            var sql = CreateDeleteSql(obj, pars);
            var re = base.ExecuteNonQuery(sql, pars);//执行            
            return re;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        public int Delete(ICollection<T> objs)
        {
            var pars = new List<Dictionary<string, object>>();//参数集

            var sqls = new List<string>(); //语句集合

            foreach (var obj in objs)
            {
                var par = new Dictionary<string, object>();
                var sql = CreateDeleteSql(obj, par);//生成参数和SQL

                sqls.Add(sql);
                pars.Add(par);
            }

            var re = base.ExecuteSql(sqls, pars);//事务执行

            return re;
        }


        /// <summary>
        /// 生成新增SQL语句
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="updateConfigName"></param>
        /// <returns></returns>
        private string CreateInsertSql(T obj, Dictionary<string, object> pars)
        {
            //组合SQL语句
            var sql = "insert into {0}({1}) values({2})";

            var parsql = new StringBuilder();//插入参数
            var fieldsql = new StringBuilder();//插入的字段

            //组合语句
            foreach (var p in pars)
            {
                //组合字段
                fieldsql.Append(Modelconfig.GetFieldNameByProperty(p.Key));
                fieldsql.Append(DbORM.MappingFieldSplit);
                //组命参数
                parsql.Append(base.GetParamChar());
                parsql.Append(p.Key);
                parsql.Append(DbORM.MappingFieldSplit);
            }

            sql = string.Format(sql, Modelconfig.TableName,
                fieldsql.ToString().Trim(DbORM.MappingFieldSplit),
                parsql.ToString().Trim(DbORM.MappingFieldSplit));

            return sql;
        }

        /// <summary>
        /// 生成更新语句
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        private string CreateUpdateSql(T obj, Dictionary<string, object> pars)
        {
            //组合SQL语句
            var sql = "update {0} set {1} where {2}";

            var wheresql = new StringBuilder("1=1 ");//条件
            var fieldsql = new StringBuilder();//修改的字段
           
            //组合语句
            foreach (var p in pars)
            {
                //组合修改字段
                fieldsql.Append(Modelconfig.GetFieldNameByProperty(p.Key));
                fieldsql.Append("=");
                fieldsql.Append(base.GetParamChar());
                fieldsql.Append(p.Key);
                fieldsql.Append(DbORM.MappingFieldSplit);               
            }

            int paramindex = 0;
            //通过主健组合条件
            foreach (var key in Modelconfig.PrimaryKeys)
            {
                wheresql.Append(" and ");
                wheresql.Append(Modelconfig.GetFieldNameByProperty(key));
                wheresql.Append("=");
                wheresql.Append(base.GetParamChar());
                var parname = "p" + paramindex;
                wheresql.Append(parname);
                //把此参数加入到参数队列
                pars.Add(parname, Reflection.ClassHelper.GetPropertyValue(obj, key));
                paramindex++;
            }

            sql = string.Format(sql, Modelconfig.TableName,
                fieldsql.ToString().Trim(DbORM.MappingFieldSplit),
                wheresql.ToString());

            return sql;
        }

        /// <summary>
        /// 生成删除语句
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        private string CreateDeleteSql(T obj, Dictionary<string, object> pars)
        {
            //组合SQL语句
            var sql = "delete from {0} where {1}";

            var wheresql = new StringBuilder("1=1 ");//条件           

            int paramindex = 0;
            //通过主健组合条件
            foreach (var key in Modelconfig.PrimaryKeys)
            {
                wheresql.Append(" and ");
                wheresql.Append(Modelconfig.GetFieldNameByProperty(key));
                wheresql.Append("=");
                wheresql.Append(base.GetParamChar());
                var parname = "p" + paramindex;
                wheresql.Append(parname);
                //把此参数加入到参数队列
                pars.Add(parname, Reflection.ClassHelper.GetPropertyValue(obj, Modelconfig.GetPropertyNameByField(key)));
            }

            sql = string.Format(sql, Modelconfig.TableName,
                wheresql.ToString());

            return sql;
        }

        /// <summary>
        /// 通过对象获取需要更新的字段与段
        /// </summary>
        /// <param name="pros"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private Dictionary<string, object> GetParams(string fields, T obj)
        {
            var dic = new Dictionary<string, object>();
            fields = DbORM.MappingFieldSplit + fields.Trim(DbORM.MappingFieldSplit) + DbORM.MappingFieldSplit;
            foreach (var p in PropertyInfos)
            {
                //限定为唯一KEY
                var pname = DbORM.MappingFieldSplit + p.Name + DbORM.MappingFieldSplit;
                if (fields.IndexOf(pname, StringComparison.CurrentCultureIgnoreCase) >= 0)//如果在要更新之列
                { 
                    //读取当前值
                    dic.Add(p.Name, Reflection.ClassHelper.GetPropertyValue(obj, p));
                }
            }

            return dic;
        }
    }
}
