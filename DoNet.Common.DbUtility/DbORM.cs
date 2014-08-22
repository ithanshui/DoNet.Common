using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoNet.Common.DbUtility
{
    /// <summary>
    /// 数据库对象转换类
    /// </summary>
    public class DbORM:BaseDB
    {        
        /// <summary>
        /// 字段映射的分隔符
        /// </summary>
        public const char MappingFieldSplit = ',';

        /// <summary>
        /// 属性与字段的分隔符
        /// </summary>
        public const char MappingSplitChar = '|';

        /// <summary>
        /// 数据库操作类
        /// </summary>
        public BaseDB DbManager
        {
            get {
                return this;
            }
        }

        /// <summary>
        /// 用基础类实例化
        /// </summary>
        /// <param name="db"></param>
        public DbORM(BaseDB db)
        {
            SetConnection(db.StrDBType, db.StrConnectionString);
        }

        /// <summary>
        /// 构造函数
        /// 从配置文件中读取数据库类型、连接字符串
        /// </summary>
        public DbORM()
        {           
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strDBType">数据库类型:"System.Data.SqlClient" or "System.Data.OracleClient" or "System.Data.Odbc" or "System.Data.OleDb"</param>
        /// <param name="strConn">连接字符串</param>
        public DbORM(string strDBType_)
            :base(strDBType_)
        {            
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strDBType">数据库类型:"System.Data.SqlClient" or "System.Data.OracleClient" or "System.Data.Odbc" or "System.Data.OleDb"</param>
        /// <param name="strConn">连接字符串</param>
        public DbORM(string strDBType, string strConn)
            :base(strDBType,strConn)
        {
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="tableName">表名</param>
        /// <param name="strFields">表字段，用逗号分隔</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="pageIndex">查询第几页，页码从1开始</param>
        /// <param name="countInPage">每页显示多少页记录,当此参数为0时，将会查出所有符合条件的记录</param>
        /// <param name="order">排序字段,比如:ondate desc</param>
        /// <param name="fieldMapping">字段映射</param>
        /// <returns>当前页数据对象</returns>
        public PageData<T> SearchDataPage<T>(string tableName,
            string strFields,
            string strWhere,            
            int pageIndex,
            int countInPage,
            string order,           
            string fieldMapping = "") where T : class
        {
            //总页数
            var pagecount = 1;
            //符合条件的数据条数
            var datacount = 0;

            var data = new PageData<T>();
            data.Data = SearchDataPage<T>(tableName, strFields, strWhere, ref pageIndex, ref pagecount, ref datacount, countInPage,order, fieldMapping);

            //获取结果值
            data.Index = pageIndex;
            data.PageCount = pagecount;
            data.DataCount = datacount;

            return data;
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="tableName">表名</param>
        /// <param name="strFields">字段列表，用逗号分隔</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="pageIndex">第几页,从1开始</param>
        /// <param name="pageCount">返回的页数</param>
        /// <param name="dataCount">返回符合条件的总条数</param>
        /// <param name="countInPage">每页显示多少条记录,当此参数为0时，将会查出所有符合条件的记录</param>
        /// <param name="order">排序字段,比如:ondate desc</param>
        /// <param name="fieldMapping">字段映射，可选</param>
        /// <returns></returns>
        public ICollection<T> SearchDataPage<T>(string tableName, 
            string strFields, 
            string strWhere,                             
            ref int pageIndex, 
            ref int pageCount, 
            ref int dataCount, 
            int countInPage,
            string order,                                           
            string fieldMapping = "") where T : class
        {
            return SearchDataPage<T>(tableName, strFields, strWhere, order,
                    ref pageIndex, ref pageCount, ref dataCount, countInPage, fieldMapping);
        }

        /// <summary>
        /// 按页查询数据
        /// </summary>
        /// <typeparam name="T">生成的对象</typeparam>
        /// <param name="orderType">排序方式：比如asc,desc</param>
        /// <param name="pageIndex">当前第几页,起始页为1</param>
        /// <param name="pageCount">总查得页数</param>
        /// <param name="countInPage">每页显示多少条,当此参数为0时，将会查出所有符合条件的记录</param>
        /// <param name="tableName">表名</param>
        /// <param name="strFields">需要查询的字段</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="order">排序字段,比如:ondate desc</param>
        /// <param name="fieldMapping">字段映射,key为属性名，value为字段名</param>
        /// <returns></returns>
        public ICollection<T> SearchDataPage<T>(string tableName, 
            string strFields, 
            string strWhere,                                                               
            string order,                                                               
            ref int pageIndex, 
            ref int pageCount, 
            ref int dataCount, 
            int countInPage,                                                              
            string fieldMapping="") where T:class
        {
            return SearchDataPage<T>(tableName, strFields, strWhere, order,
                ref pageIndex, ref pageCount, ref dataCount, countInPage,DoNet.Common.Data.DataConvert.CreateMapping(fieldMapping,MappingFieldSplit,MappingSplitChar));
        }

        /// <summary>
        /// 按页查询数据
        /// </summary>
        /// <typeparam name="T">生成的对象</typeparam>
        /// <param name="orderType">排序方式：比如asc,desc</param>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="pageCount">总查得页数</param>
        /// <param name="countInPage">每页显示多少条,当此参数为0时，将会查出所有符合条件的记录</param>
        /// <param name="tableName">表名</param>
        /// <param name="strFields">需要查询的字段</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="order">排序字段,比如:ondate desc</param>
        /// <param name="fieldMapping">字段映射,key为属性名，value为字段名</param>
        /// <returns></returns>
        public ICollection<T> SearchDataPage<T>(string tableName, 
            string strFields, 
            string strWhere,
            string order,
            ref int pageIndex,
            ref int pageCount, 
            ref int dataCount, 
            int countInPage,
            IDictionary<string, string> fieldMapping) where T : class
        {
            if (countInPage > 0)
            {
                //总查得数据条数dataCount
                object obj = this.ExecuteScalar(string.Format("select count(0) from {0} where {1}", tableName, strWhere));
                //查询符合条件的记录有多少条
                if (obj != null && obj != DBNull.Value)
                {
                    dataCount = int.Parse(obj.ToString()); //获得符合条件的数据数
                }

                pageCount = dataCount / countInPage; //记算查得多少页
                if (dataCount % countInPage != 0)
                {
                    pageCount++; //如果没有除尽则应该再加一页
                }

                //如果页面索引小于等于0则为第一页
                if (pageIndex <= 0)
                {
                    pageIndex = 1;
                }

                //如果页面索引大于总页数则索引等于最后一页
                if (pageIndex > pageCount && pageCount >= 0)
                {
                    pageIndex = pageCount;
                }

                int startIndex = (pageIndex - 1) * countInPage; //计算数据的起始位置
                startIndex = startIndex < 0 ? 0 : startIndex;
                string strSelectSql = "";

                //组合查询语句
                switch (this.GetDbType())
                {
                    case DbType.Sqlite:
                    case DbType.MySql:
                        {
                            strSelectSql = string.Format("select {0} from {1} where {2} order by {3} limit {4},{5}",
                                                         strFields, tableName, strWhere, order, startIndex,
                                                         countInPage);
                            break;
                        }
                    case DbType.MSSqlServer:
                        {
                            strSelectSql =
                                string.Format(
                                    "select * from (select {0}, ROW_NUMBER() OVER(ORDER BY {1}) AS _RowNum from {2} where {5}) _t1 where _t1._RowNum between {3} and {4}",
                                    strFields, order, tableName, startIndex + 1, startIndex + countInPage, strWhere);//因为sqlserver的行号是从一开始的
                            break;
                        }
                    case DbType.Oracle:
                        {
                            strSelectSql =
                                string.Format(
                                    "select * from (select {0}, ROWNUM rn from {1} where {4} order by {5}) _t1 where _t1.rn between {2} and {3}",
                                    strFields, tableName, startIndex + 1, startIndex + countInPage, strWhere,order);//因为行号是从一开始的
                            break;
                        }
                }

                return GetDataCollectionBySql<T>(strSelectSql, fieldMapping); //查询对象集合
            }
            else//如果每页显示的数据小于或等于0,则全查出
            {
                return GetDataCollectionBySql<T>(string.Format("select {0} from {1} where {2} order by {3}",
                strFields, tableName, strWhere, order), fieldMapping); //查询对象集合
            }
        }

        /// <summary>
        /// 通过SQL语句获取数据对象
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <param name="fieldMapping">字段映射,key为属性名，value为字段名</param>
        /// <returns></returns>
        public ICollection<T> GetDataCollectionBySql<T>(string strSql, string fieldMapping) where T : class
        {
            return GetDataCollectionBySql<T>(strSql, Common.Data.DataConvert.CreateMapping(fieldMapping, MappingFieldSplit, MappingSplitChar));
        }

        /// <summary>
        /// 获取对象集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strSql"></param>
        /// <param name="fieldMapping"></param>
        /// <returns></returns>
        public ICollection<T> GetDataCollectionBySql<T>(string strSql, IDictionary<string, string> fieldMapping) where T : class
        {
            return GetDataCollectionBySql<T>(strSql, fieldMapping, null);
        }

        /// <summary>
        /// 获取对象集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="strSql">查询语句</param>
        /// <param name="fieldMapping">字段映射</param>
        /// <param name="dbparams">参数，可为空</param>
        /// <returns></returns>
        public ICollection<T> GetDataCollectionBySql<T>(string strSql, string fieldMapping, IDictionary<string, object> dbparams) where T : class
        {
            return GetDataCollectionBySql<T>(strSql, Common.Data.DataConvert.CreateMapping(fieldMapping, MappingFieldSplit, MappingSplitChar), dbparams);
        }

        /// <summary>
        /// 通过SQL语句获取数据对象
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <param name="fieldMapping">字段映射,key为属性名，value为字段名</param>
        /// <returns></returns>
        public ICollection<T> GetDataCollectionBySql<T>(string strSql, IDictionary<string, string> fieldMapping, IDictionary<string, object> dbparams) where T : class
        {
            try
            {
                var dataCollection = new List<T>();

                System.Data.DataSet ds = null;
                if (dbparams == null)
                {
                    ds = this.GetDataSet(strSql);
                }
                else
                {
                    ds = this.GetDataSet(strSql,dbparams.Keys.ToArray<string>(),dbparams.Values.ToArray<object>());
                }


                var t = typeof(T);//为了避免多次获取,在循环之前
                //循环获取对象
                foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
                {
                    var obj = Common.Data.DataConvert.ConvertDataToObject<T>(dr, fieldMapping, t);
                    dataCollection.Add(obj);
                }

                return dataCollection;
            }
            finally
            {
                this.Close(); //关闭连接
            }
        }

        /// <summary>
        /// 通过SQL语句获取单个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strSql"></param>
        /// <param name="fieldMapping"></param>
        /// <returns></returns>
        public T GetDataBySql<T>(string strSql, string fieldMapping) where T : class
        {
            return GetDataBySql<T>(strSql, Common.Data.DataConvert.CreateMapping(fieldMapping, MappingFieldSplit, MappingSplitChar));
        }

        /// <summary>
        /// 通过SQL语句获取单个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strSql"></param>
        /// <param name="fieldMapping"></param>
        /// <returns></returns>
        public T GetDataBySql<T>(string strSql, IDictionary<string, string> fieldMapping) where T : class
        {
            return GetDataBySql<T>(strSql, fieldMapping, null);           
        }

        /// <summary>
        /// 通过语句获取对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="strSql">查询语句</param>
        /// <param name="fieldMapping">映射</param>
        /// <param name="dbparams">参数</param>
        /// <returns></returns>
        public T GetDataBySql<T>(string strSql, IDictionary<string, string> fieldMapping, IDictionary<string, object> dbparams) where T : class
        {
            var cmd = base.GetCommand(strSql, dbparams);

            var ds = this.GetDataSet(cmd);
            T obj = default(T);

            if (ds.Tables.Count > 0)
            {
                foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
                {
                    var t = typeof(T);
                    //获取对象

                    obj = Common.Data.DataConvert.ConvertDataToObject<T>(dr, fieldMapping);
                    break;
                }
            }
            return obj;
        }

        
    }
}
