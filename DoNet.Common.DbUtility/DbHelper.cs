//////////////////////////////////////////////////
// Author   : jiamao
// Date     : 2010/09/15
// Usage    : 数据库操作类
//////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Configuration;

namespace DoNet.Common.DbUtility
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DbType
    {
        Unknown = 0,
        MySql,
        Sqlite,
        MSSqlServer,
        Oracle
    }

    /// <summary>
    /// 数据库操作类
    /// </summary>
    public abstract class BaseDB:IDisposable
    {
        //数据连接接口
        private IDbConnection _IConn = null;        

        //数据提供程序
        private DbProviderFactory _DbFactory = null;

        Proxy.DBInfo _dbinfo = new Proxy.DBInfo();
        /// <summary>
        /// 当前数据库信息
        /// </summary>
        public Proxy.DBInfo DBInfo
        {
            get {
                return _dbinfo;
            }
        }

        //锁
        static object _synLock = new object();

        //数据库类型
        private string _strDBType;
        /// <summary>
        /// 数据库类型标识
        /// </summary>
        public string StrDBType
        {
            get { return _strDBType; }
            set { _strDBType = value; }
        }

        //数据库连接字符串
        private string _strConnectionString;
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string StrConnectionString
        {
            get { return _strConnectionString; }
            set { _strConnectionString = value; }
        }

        /// <summary>
        /// 所有驱动的路径集合
        /// 不同的数据库驱动不同
        /// </summary>
        static Dictionary<string, string> _providerDllPaths = new Dictionary<string, string>();

        /// <summary>
        /// 数据库驱动路径
        /// </summary>
        public string DbProviderDllPath
        {
            get
            {
                //通过数据库类型获取驱动路径
                if (!string.IsNullOrWhiteSpace(StrDBType))
                {
                    if (_providerDllPaths.ContainsKey(StrDBType))
                    {
                        return _providerDllPaths[StrDBType];
                    }
                }
                return string.Empty;
            }
            set
            {
                //数据库类型
                if (!string.IsNullOrWhiteSpace(StrDBType))
                {
                    if (_providerDllPaths.ContainsKey(StrDBType))
                    {
                        _providerDllPaths[StrDBType] = value;
                    }
                    else
                    {
                        lock (_synLock)
                        {
                            if (!_providerDllPaths.ContainsKey(StrDBType))
                            {
                                _providerDllPaths.Add(StrDBType, value);
                            }
                            else
                            {
                                _providerDllPaths[StrDBType] = value;
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("请在设置驱动路径前指定数据库类型");
                }
            }
        }

        /// <summary>
        /// 设置数据库驱动路径
        /// </summary>
        /// <param name="strDbType">驱动类型</param>
        /// <param name="path">驱动路径</param>
        public static void SetDBProviderPath(string strDbType, string path)
        {
            lock (_synLock)
            {
                if (!_providerDllPaths.ContainsKey(strDbType))
                {
                    _providerDllPaths.Add(strDbType, path);
                }
                else
                {
                    _providerDllPaths[strDbType] = path;
                }
            }
        }

        /// <summary>
        /// 参数标识字符
        /// </summary>
        static Dictionary<DbType, char> _ParamMarkChar = new Dictionary<DbType, char>();

        /// <summary>
        /// 参数标识字符
        /// </summary>
        public static Dictionary<DbType, char> ParamMarkChar
        {
            get
            {
                if (_ParamMarkChar == null || _ParamMarkChar.Count == 0)
                {
                    lock (_synLock)
                    {
                        if (_ParamMarkChar == null || _ParamMarkChar.Count == 0)
                        {
                            _ParamMarkChar.Add(DbType.MSSqlServer, '@');
                            _ParamMarkChar.Add(DbType.Sqlite, '@');
                            _ParamMarkChar.Add(DbType.Oracle, ':');
                            _ParamMarkChar.Add(DbType.MySql, '?');
                        }
                    }
                }
                return _ParamMarkChar;
            }
            set { BaseDB._ParamMarkChar = value; }
        }

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// 从配置文件中读取数据库类型、连接字符串
        /// </summary>
        public BaseDB()
        {           
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strDBType">数据库类型:"System.Data.SqlClient" or "System.Data.OracleClient" or "System.Data.Odbc" or "System.Data.OleDb"</param>
        /// <param name="strConn">连接字符串</param>
        public BaseDB(string strDBType_)
        {
            _strDBType = strDBType_;            
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strDBType">数据库类型:"System.Data.SqlClient" or "System.Data.OracleClient" or "System.Data.Odbc" or "System.Data.OleDb"</param>
        /// <param name="strConn">连接字符串</param>
        public BaseDB(string strDBType,string strConn)
        {
            //获取连接
            SetConnection(strDBType, strConn);
        }

        /// <summary>
        /// 设置连接
        /// </summary>
        /// <param name="strDBType">数据库类型:"System.Data.SqlClient" or "System.Data.OracleClient" or "System.Data.Odbc" or "System.Data.OleDb,MySql.Data.MySqlClient"</param>
        /// <param name="strConn">连接字符串</param>
        public void SetConnection(string strdbtype, string strConn)
        {
            _strDBType = strdbtype.ToUpper();
            StrConnectionString = strConn;

            _dbinfo.DBType = GetDbType();//数据库类别
            _dbinfo.ConnectionString = strConn;
            if (_IConn == null)
            {
                if (_dbinfo.DBType == DbType.MySql)
                {
                    LoadProviderFactoery("MySql.Data.dll", "MySql.Data", "MySql.Data.MySqlClient.MySqlClientFactory");                    
                }
                else if (_dbinfo.DBType == DbType.Sqlite)
                {
                    LoadProviderFactoery("System.Data.SQLite.dll", "System.Data.SQLite", "System.Data.SQLite.SQLiteFactory");
                }
                else if (_dbinfo.DBType == DbType.Oracle)
                {
                    _DbFactory = System.Data.OracleClient.OracleClientFactory.Instance;
                    //LoadProviderFactoery("System.Data.OracleClient.dll", "System.Data.OracleClient", "System.Data.OracleClient.OracleClientFactory");                    
                }
                else if (!string.IsNullOrEmpty(strdbtype))
                {
                    //获取指定提供程序名称的 DbProviderFactory 的一个实例
                    _DbFactory = DbProviderFactories.GetFactory(strdbtype);
                }
                if (!string.IsNullOrEmpty(strdbtype) && _DbFactory != null)
                {
                    //创建连接
                    _IConn = _DbFactory.CreateConnection();
                  
                    //连接字符串
                    if (!string.IsNullOrEmpty(strConn)) _IConn.ConnectionString = strConn;
                }
            }
        }

        /// <summary>
        /// 设置数据库连接信息
        /// </summary>
        /// <param name="strdbtype"></param>
        /// <param name="server"></param>
        /// <param name="port"></param>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <param name="dbname"></param>
        /// <param name="charset"></param>
        public void SetConnection(string strdbtype, string server, int port, string username, string pwd, string dbname,string otherAttr="")
        {
            var connstring = "";
             _strDBType = strdbtype.ToUpper();
            var dbType = GetDbType();//数据库类别
            switch (dbType)
            {
                case DbType.MSSqlServer:
                    {
                        connstring = string.Format("Data Source={0};Database={1};user id={2};password={3};" + otherAttr, server + (port > 0 ? "," + port : ""),
                            dbname,username,pwd);
                        break;
                    }
                case DbType.Sqlite:
                    {
                        connstring = string.Format("Data Source={0};" + otherAttr, dbname);
                        break;
                    }
                case DbType.MySql:
                    {
                        connstring = string.Format("server={0};database={1};user id={2};password={3};", server,
                            dbname, username, pwd) + (port>0?"port=" + port + ";":"") + otherAttr;
                        break;
                    }
                case DbType.Oracle:
                    {
                        connstring = string.Format("Data Source={0};user id={1};password={2};" + otherAttr, server,
                            username, pwd);
                        break;
                    }
                default: {
                    throw new Exception("不支持的数据库类型!");
                }
            }
            SetConnection(strdbtype, connstring);
        }

        /// <summary>
        /// 加载数据库连接驱动
        /// </summary>
        /// <param name="dll">DLL文件名</param>
        /// <param name="nameSpace">驱动的命名空间</param>
        /// <param name="className">驱动类全名</param>
        void LoadProviderFactoery(string dll,string nameSpace, string className)
        {
            DbProviderDllPath = string.IsNullOrEmpty(DbProviderDllPath) ? IO.PathMg.CheckPath(dll) : DbProviderDllPath;
            //如果驱动文件存在
            if (System.IO.File.Exists(DbProviderDllPath))
            {
                _DbFactory = (DbProviderFactory)Reflection.ClassHelper.GetClassObject(DbProviderDllPath, className);
            }
            //如果不存在则从当前加载
            else
            {
                var ass = System.Reflection.Assembly.Load(nameSpace);
                if (ass != null)
                {
                    var t = ass.GetType(className);//加载类型
                    if (t == null)
                    {
                        throw new Exception("无法加载类型：" + className);
                    }
                    _DbFactory = (DbProviderFactory)Activator.CreateInstance(t);
                }
                else
                {
                    throw new Exception("找不到命名空间：" + nameSpace);                
                }
            }
        }
        #endregion

        #region 获取数据库连接接口
        /// <summary>
        /// 获取数据库连接接口
        /// </summary>
        public IDbConnection IConn
        {
            get
            {
                return _IConn;
            }
        }
        #endregion

        /// <summary>
        /// 获取数据库类型
        /// </summary>
        /// <returns></returns>
        public DbType GetDbType()
        {
            switch (_strDBType)
            {

                case "MYSQL.DATA.MYSQLCLIENT":
                    return DbType.MySql ;
                case "SYSTEM.DATA.SQLITE":
                    return DbType.Sqlite;
                case "SYSTEM.DATA.SQLCLIENT":
                    return DbType.MSSqlServer ;
                case "SYSTEM.DATA.ORACLECLIENT":
                    return DbType.Oracle;               
            }
            if (_strDBType.Contains("MYSQL"))
            {
                return DbType.MySql;
            }
            else if (_strDBType.Contains("SQLITE"))
            {
                return DbType.Sqlite;
            }
            else if (_strDBType.Contains("SQLSERVER"))
            {
                return DbType.MSSqlServer;
            }
            else if (_strDBType.Contains("ORACLE"))
            {
                return DbType.Oracle;
            }
            return DbType.Unknown;
        }

        #region 获取事务
        /// <summary>
        /// 获取事务
        /// </summary>
        /// <returns></returns>
        public IDbTransaction GetTransaction()
        {
            return _IConn.BeginTransaction();
        }
        #endregion

        #region 打开连接

        /// <summary>
        /// 打开连接
        /// </summary>
        public IDbConnection Open()
        {
            if (_IConn.State == ConnectionState.Closed)
            {
                //关闭状态，打开连接
                _IConn.Open();
            }
            else if (_IConn.State == ConnectionState.Broken)
            {
                //中断状态，先关闭后打开连接
                _IConn.Close();
                _IConn.Open();
            }
            return _IConn;
        }

        #endregion

        #region 关闭连接
        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            if (_IConn == null)
            {
                return;
            }
            else if (_IConn.State != ConnectionState.Closed)
            {
                _IConn.Close();
            }
        }
        #endregion

        #region 获取数据库命令接口
        /// <summary>
        /// 获取数据库命令接口
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="pars"></param>
        /// <param name="cmdType"></param>
        /// <returns></returns>
        protected IDbCommand GetCommand(string cmdText, IDbDataParameter[] pars, CommandType cmdType = CommandType.Text)
        {
            //创建数据库命令
            IDbCommand cmd = _DbFactory.CreateCommand();
            cmd.CommandType = cmdType;
            //设置数据源运行的文本命令
            cmd.CommandText = cmdText;
            //设置连接
            cmd.Connection = _IConn;
           
            if (pars != null)
            {
                cmd.Parameters.Clear();
                foreach (var p in pars)
                {
                    cmd.Parameters.Add(p);
                }
            }

            return cmd;
        }

        /// <summary>
        /// 获取数据库命令接口
        /// </summary>
        /// <param name="cmdText">查询语句</param>
        /// <returns>返回数据库命令接口</returns>
        protected IDbCommand GetCommand(string cmdText,IDictionary<string,object> pars, CommandType cmdType= CommandType.Text)
        {
            //创建数据库命令
            IDbCommand cmd = _DbFactory.CreateCommand();
            cmd.CommandType = cmdType;
            //设置数据源运行的文本命令
            cmd.CommandText = cmdText;            
            //设置连接
            cmd.Connection = _IConn;
          
            if (pars != null)
            {
                cmd.Parameters.Clear();
                foreach (var p in pars)
                {
                    cmd.Parameters.Add(CreateParam(cmd,p.Key,p.Value));
                }
            }

            return cmd;
        }

        /// <summary>
        /// 获取数据库命令接口
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="paramNames"></param>
        /// <param name="paramValues"></param>
        /// <param name="cmdType"></param>
        /// <returns></returns>
        protected IDbCommand GetCommand(string cmdText, string[] paramNames,object[] paramValues, CommandType cmdType = CommandType.Text)
        {
            var pars = new Dictionary<string, object>();
            if (paramNames != null && paramValues != null)
            {
                for (var i = 0; i < paramNames.Length; i++)
                {
                    pars.Add(paramNames[i], paramValues[i]);
                }
            }
            return GetCommand(cmdText, pars, cmdType);
        }
        /// <summary>
        /// 获取数据库命令接口
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdType"></param>
        /// <returns></returns>
        protected IDbCommand GetCommand(string cmdText, CommandType cmdType = CommandType.Text)
        {
            //创建数据库命令
            IDbCommand cmd = _DbFactory.CreateCommand();
           
            cmd.CommandType = cmdType;
            //设置数据源运行的文本命令
            cmd.CommandText = cmdText;
            //设置连接
            cmd.Connection = _IConn;

            return cmd;
        }

        /// <summary>
        /// 获取所有表名
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllTableName()
        {
            var TableNames = new List<string>();

            OleDbConnection oledb = IConn as OleDbConnection;
            oledb.Open();
            DataTable dt = oledb.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            foreach (DataRow dr in dt.Rows)
            {
                TableNames.Add(dr[2].ToString());
            }

            return TableNames;
        }  
    
        #endregion

        #region 获取数据读取器

        /// <summary>
        /// 获取数据读取器
        /// </summary>
        /// <param name="IDbCommand">dbcommand</param>
        /// <returns>返回数据读取器接口</returns>
        protected IDataReader GetDataReader(IDbCommand cmd)
        {
            Open();
            return cmd.ExecuteReader();
        }

        /// <summary>
        /// 获取数据读取器
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <returns></returns>
        public IDataReader GetDataReader(string sql)
        {
            var cmd = GetCommand(sql);
            return GetDataReader(cmd);
        }

        #endregion

        #region 执行SQL语句返回受影响的行数

        /// <summary>
        /// 执行SQL语句返回受影响的行数
        /// </summary>
        /// <param name="cmdText">查询语句</param>
        /// <returns>返回受影响的行数(-1:执行失败)</returns>
        protected int ExecuteNonQuery(IDbCommand cmd)
        {
            int iRows = 0;
            //打开并执行命令
            using (Open())
            {
                iRows = cmd.ExecuteNonQuery();
            }
            return iRows;
        }

        /// <summary>
        /// 执行SQL语句返回受影响的行数
        /// </summary>
        /// <param name="cmdText">查询语句</param>
        /// <returns>返回受影响的行数(-1:执行失败)</returns>
        public int ExecuteNonQuery(string cmdText)
        {
            if (DBInfo.IsProxy)
            {
                return Proxy.Access.CreateProxy().ExecuteNonQuery(DBInfo, cmdText);
            }
            else
            {
                var cmd = GetCommand(cmdText);
                return ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 执行SQL语句返回受影响的行数
        /// </summary>
        /// <param name="cmdText">查询语句</param>
        /// <returns>返回受影响的行数(-1:执行失败)</returns>
        public int ExecuteNonQuery(string cmdText,params IDbDataParameter[] ps)
        {
             var cmd = GetCommand(cmdText,ps);
            return ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 执行SQL语句返回受影响的行数
        /// </summary>
        /// <param name="cmdText">查询语句</param>
        /// <returns>返回受影响的行数(-1:执行失败)</returns>
        public int ExecuteNonQuery(string cmdText, string[] paraNames, object[] paraValues)
        {
            if (DBInfo.IsProxy)
            {
                return Proxy.Access.CreateProxy().ExecuteNonQueryWithParam(DBInfo, cmdText,paraNames,paraValues);
            }
            else
            {
                var cmd = GetCommand(cmdText, paraNames, paraValues);

                return ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// 执行带参数的SQL语句
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="dbparams"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string cmdText, IDictionary<string,object> dbparams)
        {
            if (DBInfo.IsProxy)
            {
                return Proxy.Access.CreateProxy().ExecuteNonQueryWithParams(DBInfo, cmdText,dbparams);
            }
            else
            {
                var cmd = GetCommand(cmdText, dbparams);

                return ExecuteNonQuery(cmd);
            }
        }  
        #endregion

        #region 执行SQL语句返回数据集

        /// <summary>
        /// 返回数据集
        /// </summary>
        /// <param name="cmd">命令</param>
        /// <param name="pars">参数</param>       
        /// <returns>返回数据集</returns>
        protected DataSet GetDataSet(IDbCommand cmd)
        {
            DataSet ds = new DataSet();

            DoNet.Common.IO.Logger.Write("DBLog", "Start-Query:" + cmd.CommandText);
            using (Open())
            {
                IDbDataAdapter da = _DbFactory.CreateDataAdapter();
                da.SelectCommand = cmd;               
                da.Fill(ds);
            }
            DoNet.Common.IO.Logger.Write("DBLog", "End-Query:" + cmd.CommandText);
            return ds;
        }

        /// <summary>
        /// 执行SQL语句返回数据集
        /// </summary>
        /// <param name="cmdText">查询语句</param>
        /// <returns>返回数据集</returns>
        public DataSet GetDataSet(string cmdText)
        {
            if (DBInfo.IsProxy)
            {
                var ds = Proxy.Access.CreateProxy().GetDataSet(DBInfo, cmdText);
                var nds = ds.ToDataSet();
                return nds;
            }
            else
            {
                var cmd = GetCommand(cmdText);
                return GetDataSet(cmd);
            }
        }

        /// <summary>
        /// 执行SQL语句返回数据集
        /// </summary>
        /// <param name="cmdText">查询语句</param>
        /// <returns>返回数据集</returns>
        public DataSet GetDataSet(string cmdText, string[] paraNames, object[] paraValues)
        {
            if (DBInfo.IsProxy)
            {
                var ds = Proxy.Access.CreateProxy().GetDataSetWithParam(DBInfo, cmdText,paraNames,paraValues);
                var nds = ds.ToDataSet();
                return nds;
            }
            else
            {
                var cmd = GetCommand(cmdText,paraNames,paraValues);
                return GetDataSet(cmd);
            }
        }

        /// <summary>
        /// 执行带参数SQL语句返回数据集
        /// </summary>
        /// <param name="strProcudureName">查询语句</param>
        /// <param name="param">SQL语句参数</param>
        /// <returns>返回数据集</returns>
        public DataSet GetDataSet(string strSql, params IDbDataParameter[] pars)
        {
            var cmd = GetCommand(strSql,pars);
            return GetDataSet(cmd);
        }

        /// <summary>
        /// 执行存储过程返回数据集
        /// </summary>
        /// <param name="cmdType">命令类型</param>
        /// <param name="strProcudureName">存储过程名称</param>
        /// <param name="param">存储过程参数</param>
        /// <returns>返回数据集</returns>
        public DataSet GetDataSetByStore(string strProcudureName, params IDbDataParameter[] pars)
        {
            var cmd = GetCommand(strProcudureName,pars, CommandType.StoredProcedure);            
            return GetDataSet(cmd);
        }

        #endregion

        #region 执行SQL语句返回单值对象

        /// <summary>
        /// 执行SQL语句返回单值对象
        /// </summary>
        /// <param name="cmdText">查询语句</param>
        /// <returns>返回结果集中第一行的第一列数据</returns>
        protected object ExecuteScalar(IDbCommand cmd)
        {
            using (Open())
            {
                return cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// 执行SQL语句返回单值对象
        /// </summary>
        /// <param name="cmdText">查询语句</param>
        /// <returns>返回结果集中第一行的第一列数据</returns>
        public object ExecuteScalar(string cmdText)
        {
            if (DBInfo.IsProxy)
            {
               return Proxy.Access.CreateProxy().ExecuteScalar(DBInfo, cmdText);
            }
            else
            {
                var cmd = GetCommand(cmdText);
                return ExecuteScalar(cmd);
            }
        }

        /// <summary>
        /// 执行SQL语句返回单值对象
        /// </summary>
        /// <param name="cmdText">查询语句</param>
        /// <returns>返回结果集中第一行的第一列数据</returns>
        public object ExecuteScalar(string cmdText, string[] paraNames, object[] paraValues)
        {
            if (DBInfo.IsProxy)
            {
                return Proxy.Access.CreateProxy().ExecuteScalarWithParam(DBInfo, cmdText, paraNames, paraValues);
            }
            else
            {
                var cmd = GetCommand(cmdText, paraNames, paraValues);

                return ExecuteScalar(cmd);
            }
        }

        #endregion

        #region 执行多条SQL语句 
        /// <summary>
        /// 在同一事务中
        /// 执行多个命令
        /// </summary>
        /// <param name="cmds"></param>
        /// <returns></returns>
        protected int ExecuteCommands(ICollection<IDbCommand> cmds)
        {
            var count = 0;

            using (Open())
            {
                var tran = GetTransaction();
                {
                    foreach (var cmd in cmds)
                    {
                        cmd.Transaction = tran;
                        count += cmd.ExecuteNonQuery();
                    }
                    tran.Commit();
                }
            }
            return count;
        }

        /// <summary>
        /// 执行多条SQL语句
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns></returns>
        public int ExecuteSql(ICollection<string> sqls)
        {
            if (DBInfo.IsProxy)
            {
                return Proxy.Access.CreateProxy().ExecuteSql(DBInfo, sqls);
            }
            else
            {
                var cmds = new List<IDbCommand>();
                foreach (var sql in sqls)
                {
                    var cmd = GetCommand(sql);
                    cmds.Add(cmd);
                }
                return ExecuteCommands(cmds);
            }
        }

        /// <summary>
        /// 在同一事务中
        /// 执行多条带参数的SQL语句
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns></returns>
        public int ExecuteSql(List<string> sqls, List<string[]> paraNames, List<object[]> paraValues)
        {
            if (DBInfo.IsProxy)
            {
                return Proxy.Access.CreateProxy().ExecuteSqlWithParam(DBInfo, sqls, paraNames, paraValues);
            }
            else
            {
                var cmds = new List<IDbCommand>();
                for (int i = 0; i < sqls.Count; i++)
                {
                    var cmd = GetCommand(sqls[i], paraNames[i], paraValues[i]);
                    cmds.Add(cmd);
                }
                return ExecuteCommands(cmds);
            }
        }

        /// <summary>
        /// 在同一事务中
        /// 执行多条带参数的SQL语句
        /// </summary>
        /// <param name="sqls"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public int ExecuteSql(List<string> sqls, List<Dictionary<string,object>> pars)
        {
            var cmds = new List<IDbCommand>();
            for (var i = 0; i < sqls.Count;i++ )
            {
                var cmd = GetCommand(sqls[i], pars[i]);
                cmds.Add(cmd);
            }
            return ExecuteCommands(cmds);
        }

        #endregion

        /// <summary>
        /// 生成参数
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="paraName"></param>
        /// <param name="paraValue"></param>
        /// <returns></returns>
        IDbDataParameter CreateParam(IDbCommand cmd, string paraName, object paraValue)
        {
            var p = cmd.CreateParameter();
            var paramchar = GetParamChar();//参数标识字符
            paraName = paraName.Trim();

            if (paraName[0] != paramchar) paraName = paramchar + paraName;//组合参数

            p.ParameterName = paraName;
            p.Value = paraValue;
            return p;
        }

        /// <summary>
        /// 通过数据库类型获取参数标识
        /// </summary>
        /// <param name="dbtype"></param>
        /// <returns></returns>
        public static char GetParamChar(DbType dbtype)
        {
            if (ParamMarkChar.ContainsKey(dbtype))
            {
                return ParamMarkChar[dbtype];
            }
            return '@';
        }

        /// <summary>
        /// 获取参数字符
        /// </summary>
        /// <returns></returns>
        public char GetParamChar()
        {
            var dbtype = GetDbType();
            return GetParamChar(dbtype);
        }

        /// <summary>
        /// 获取参数名
        /// </summary>
        /// <returns></returns>
        public string GetParamChar(string parName)
        {
            var dbtype = GetDbType();
            return GetParamChar(dbtype) + parName;
        }

        /// <summary>
        /// 去除关健的字符，以免被注入
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string SplitSqlChar(string source)
        {
            if (string.IsNullOrEmpty(source)) return source;

            return source.Replace("'", "").Replace("=", "").Replace("delete from", "").Replace(" or ", "").Replace("%", "");
        }

        #region IDisposable 成员

        public void Dispose()
        {
            dispose(true);
        }

        private void dispose(bool disposing)
        {
            if (disposing)
            {
                Close();

                GC.SuppressFinalize(true);
            }
            
        }
        #endregion
    }

}
