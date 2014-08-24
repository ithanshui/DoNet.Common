using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data;
using System.Net;
using System.Linq.Expressions;

namespace example
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(0.5.ToString("#.##"));
            Console.WriteLine(0.5.ToString("#0.##"));

           

            Console.ReadLine();
            var r = DoNet.Common.Net.WebServiceHelper.InvokeWebService("http://czyourui.w137.mc-test.com/Server/CustomerSearch.asmx", "", "ConnectionState",null);
            Console.WriteLine(r);
            var cs = new List<DoNet.Common.Data.ServiceProxyInvokeItem>();
            cs.Add(new DoNet.Common.Data.ServiceProxyInvokeItem() { ClassName = "class", IPAddress = "*", MethodName = "method", Namespace = "namespace" });
            DoNet.Common.Serialization.FormatterHelper.XMLSerObject(cs,Environment.CurrentDirectory + "/proxy.config");
           /* var dbms = DoNet.Common.DbUtility.DbFactory.CreateDbORM("sqlserver", "");
            var sql = "select * from fasdfas";
            Console.WriteLine(dbms.DerSubOrderSql(sql));

             sql = " select * from fasdfas";
            Console.WriteLine(dbms.DerSubOrderSql(sql));

            sql = "select * from union select  top 10 * from fasdfas";
            Console.WriteLine(dbms.DerSubOrderSql(sql));

             sql = "select top 10 * from fasdfas";
            Console.WriteLine(dbms.DerSubOrderSql(sql));

             sql = "select top(10) * from fasdfas";
            Console.WriteLine(dbms.DerSubOrderSql(sql));

             sql = "select top (10) * from fasdfas";
            Console.WriteLine(dbms.DerSubOrderSql(sql));

             sql = "select * from ttt where a in (select * from fasdfas)";
            Console.WriteLine(dbms.DerSubOrderSql(sql));


            sql = "select * from ttt where a in (select ( top 10) * from fasdfas)";
            Console.WriteLine(dbms.DerSubOrderSql(sql));
            */
           /* var sql = "select * from tbdbdatasource";
            var db123 = DoNet.Common.DbUtility.DbFactory.CreateDbORM("oracle", "server=10.167.139.19;port=3306;database=totaloapv2;user id=root;password=system;charset=utf8;pooling=true;default command timeout=10");
            db123.DBInfo.IsProxy = true;
            db123.DBInfo.ProxyUrl = "http://eprm.ied.com/dbproxy/dbproxy.ashx";

            while (true)
            {
                var prods = db123.GetDataSet("show processlist");
                var stopwatcher = new System.Diagnostics.Stopwatch();
                stopwatcher.Start();
                var ds = db123.GetDataSet(sql);

                if (stopwatcher.ElapsedMilliseconds > 1500)
                {
                    Console.WriteLine("耗时超过2秒");
                    var ms = new System.IO.MemoryStream();
                    ds.WriteXml(ms);
                    var data123 = System.Text.Encoding.UTF8.GetString(ms.ToArray());
                    DoNet.Common.IO.Logger.Debug(data123);

                    ms = new System.IO.MemoryStream();
                    prods.WriteXml(ms);
                    data123 = System.Text.Encoding.UTF8.GetString(ms.ToArray());
                    DoNet.Common.IO.Logger.Debug(data123);
                }
                stopwatcher.Stop();
                System.Threading.Thread.Sleep(1000);
            }
            Console.ReadLine();
            */
           /*
            var sql = "select count(0) from (select date_format(a.dtStatDate,'%Y-%m-%d') as 'dtStatDate',iDayRegNum,iDayActivityNum from dbCfOssResult.tbUserLogin a, dbCfOssResult.tbUserRegister  b where a.dtStatDate=b.dtStatDate and  a.iWorldId=255 and a.dtStatDate between '2013-12-10' -interval 6 day and '2013-12-10'  group by a.dtStatDate order by a.dtStatDate) t";
            var db123 = DoNet.Common.DbUtility.DbFactory.CreateDbORM("MySql.data", "server=172.27.136.16;port=3306;database=dbCfOssResult;user id=oss_epr;password=oss_epr_cf;charset=utf8;pooling=false;default command timeout=10");
            //db123.DBInfo.IsProxy = true;
            //db123.DBInfo.ProxyUrl = "http://eprm.ied.com/dbproxy/dbproxy.ashx";

            while (true)
            {
                var prods = db123.GetDataSet("show processlist");
                var stopwatcher = new System.Diagnostics.Stopwatch();
                stopwatcher.Start();
                var ds = db123.GetDataSet(sql);

                if (stopwatcher.ElapsedMilliseconds > 1500)
                {
                    Console.WriteLine("耗时超过2秒");
                    var ms = new System.IO.MemoryStream();
                    ds.WriteXml(ms);
                    var data123 = System.Text.Encoding.UTF8.GetString(ms.ToArray());
                    DoNet.Common.IO.Logger.Debug(data123);

                    ms = new System.IO.MemoryStream();
                    prods.WriteXml(ms);
                    data123 = System.Text.Encoding.UTF8.GetString(ms.ToArray());
                    DoNet.Common.IO.Logger.Debug(data123);
                }
                stopwatcher.Stop();
                System.Threading.Thread.Sleep(1000);
            }
            Console.ReadLine();*/
            //return;
           

            /*
            var request = DoNet.Common.Net.JMRequest.CreateHttp("http://localhost:8080/test.php");
            request.Request.Method = "post";

            var prr = System.Web.HttpUtility.UrlEncode("bfg呀地棒球");
            request.Write(System.Text.Encoding.ASCII.GetBytes(prr));

            var res = request.GetResponse();
            var r = res.ReadToEnd();
            Console.WriteLine(System.Web.HttpUtility.UrlDecode(r));
            Console.ReadLine();*/
            //var public_key = "<RSAKeyValue><Modulus>3WCQr1IQqCh34WKOCTzpiC0wfAP8cEHojP9LBqklaNheITFFrP1Ec4bXp042YW6bYxHnMDyDTg//4nUcC/RR3qIEzExNmf2V0FMtzdEcKovXxM7yzFJkaM35DM1MsKhJdyNmgbh/ja1EJDpVt+zn8qsFBUtqkVO0ElS7nk9ZBZM=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            //var private_key = "<RSAKeyValue><Modulus>3WCQr1IQqCh34WKOCTzpiC0wfAP8cEHojP9LBqklaNheITFFrP1Ec4bXp042YW6bYxHnMDyDTg//4nUcC/RR3qIEzExNmf2V0FMtzdEcKovXxM7yzFJkaM35DM1MsKhJdyNmgbh/ja1EJDpVt+zn8qsFBUtqkVO0ElS7nk9ZBZM=</Modulus><Exponent>AQAB</Exponent><P>75ViW3Zcn8p11X23Xj5xBFwyhEC7a9xIVv28Ob4lajlig89zBuZ8Zi9JqC3sRxWmO5J3xRtk3GEwnld6lVFdtQ==</P><Q>7IvRUvXoyhRKiPU3tMQVt/z8mjGjQoSYPRhu4T/TnzTdMEcDNLdpHaqB8ELAog7utd7C4TEAye4tUCY+ZhQjJw==</Q><DP>m1whlfHhCnV9h92oBOM04oDu+TgI0V7dQhvz7PXSyVlA+vyROM5JqPHNL9PnvgjZ7RODuzuSYh5cKrHLefxzaQ==</DP><DQ>zMtwe0b0OKDAtzq29AYgV57shAMdueVaeOrCdLnx2hDGv5l7qRRyKYEJ5p2kcapD+anXR2hJqopPKOkzdOVSWQ==</DQ><InverseQ>fMBAzUlLGvcMNfvlPKc/vLSEzO5WVQz6SDvBHZhHJXFzXZbq7Yfvfr5qw7bN5f49ITIPedlz5MiO/G8U6ciX6w==</InverseQ><D>a5zveGpaMoRJkkSIazEzDMF62i5N3nwLgc7wN7KtvsO/Lj93cVpEliwsVOYORVqxKn2fdrFT2vSoHPt0wNLpoHqJ4ryMtsJ/sL/0vyaF7D2HeVitdZ7PIL+S7MfywAv24IG51XvFbiuDyMYaQzp3E+6MS2vsuKq3U4KIABzJOfE=</D></RSAKeyValue>";


            //var source = "abcdefg";
            //var prikey = "";
            //var pubkey = "";
            //DoNet.Common.Text.Security.RSACreateKey(ref prikey, ref pubkey);
            //var s = DoNet.Common.Text.Security.RSAEncrypt(source, pubkey);


            //var s2 = DoNet.Common.Text.Security.Des3Encrypt("111111111111112222222233", "for 语句有三个参数。第一个参数初始化变量，第二个参数保存条件，第三个参数包含执行循环所需的增");

            //var s = DoNet.Common.Text.Security.Des3Decrypt("111111111111112222222233", s2);

            //DateTime dt;
            //DateTime.TryParse("d3", out dt);
           
            ////Console.WriteLine(s);

            //var ths = new System.Threading.Thread[100];
            //Console.WriteLine(DateTime.Now.ToString());
            //for (var i = 0; i < 1;i++ )
            //{
            //    ths[i] = new System.Threading.Thread(new System.Threading.ThreadStart(Log));
            //    ths[i].Start();
            //}

            //var request = DoNet.Common.Net.JMRequest.CreateHttp("http://10.130.91.231/cgi-bin/clmapi_test.py");
            //request.Request.Method = "post";
            //System.Net.ServicePointManager.Expect100Continue = false;
            //var qr = ("{\"header\":{\"user\":\"alexcheng\",\"version\":0.1},\"request\":{\"cmd\":\"getClustersData\", \"parms\":{\"group_id\":\"7x\", \"ModelIDs\":\"10,30\"}}}");
            //var bs = System.Text.Encoding.UTF8.GetBytes(qr);
            //request.Write(bs);
           
            //using (var response = request.GetResponse())
            //{
            //    var content = response.ReadToEnd(System.Text.Encoding.UTF8);
            //    var data = ServerData.FromJSON(content);
            //    Console.WriteLine(content);
            //}

            //var a = new ServerData();
            //a.respons = new ServerResponse();
            //a.respons.result = new ReportData();
            //a.respons.result.Tables = new List<DataTable>();
            //a.respons.result.Tables.Add(new DataTable());
            //a.respons.result.Tables[0].Name = "tb1";

            //var json = DoNet.Common.Serialization.JSon.ModelToJson(a);

            //var b = DoNet.Common.Serialization.JSon.JsonToModel<ServerData>(json);


            //var s = "bvbvnbbnm<fasdfasfas/> dfhdfhdf";
            //var reg = new System.Text.RegularExpressions.Regex("\\<(.*)/\\>");
            //var m = reg.Match(s);
            //var m2 = m;
            //try
            //{
            //    try
            //    {
            //        try
            //        {
            //            throw new Exception("test");
            //        }
            //        catch (Exception ex)
            //        {
            //            //DoNet.Common.IO.Logger.Debug(ex);
            //            throw;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        DoNet.Common.IO.Logger.Debug(ex);

            //        throw ex;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    DoNet.Common.IO.Logger.Debug(ex);
            //}

            //var dt = new System.Data.DataTable();
            //dt.Columns.Add(new System.Data.DataColumn("id",typeof(int)));

            //var r1 = dt.NewRow();
            //r1[0] = 5;
            //dt.Rows.Add(r1);

            //var r2 = dt.NewRow();
            //r2[0] = 1;
            //dt.Rows.Add(r2);
            //var r3 = dt.NewRow();
            //r3[0] = 4;
            //dt.Rows.Add(r3);
            //var r4 = dt.NewRow();
            //r4[0] = 5;
            //dt.Rows.Add(r4);
            //var r5 = dt.NewRow();
            //r5[0] = 8;
            //dt.Rows.Add(r5);
            //var r6 = dt.NewRow();
            //r6[0] = 0;
            //dt.Rows.Add(r6);

            //foreach (System.Data.DataRow dr in dt.Rows)
            //{ 
            
            //}
            ////dt.DefaultView.Sort = "id desc";
            //DateTime? dt=DateTime.Now;
            //int? a = 0;

            //if (dt.GetType().BaseType == typeof(ValueType) && dt.GetType() != typeof(DateTime)) //如果是值类型
            //{
            //   // strColValue = ColValue.ToString();
            //}

            /*
            var db = DoNet.Common.DbUtility.DbFactory.CreateDbORM("mysql.data.dll", "server=127.0.0.1;port=3306;database=jiamao;user id=root;password=123456;charset=utf8;default command timeout=10");
            var ds = db.GetDataSet("select * from article");

            foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
            {
                var path = dr["path"].ToString();
                var newid = dr["id"].ToString();
                var oldid = path.Substring(path.LastIndexOf('/') + 1);
                var date = (DateTime)dr["createon"];



                DoNet.Common.IO.DirectoryHelper.CopyDirectory(@"D:\项目\JiaMaoCode\wwwroot\haofefe\wwwroot\" + path,
                                @"D:\微云网盘\273389528\项目\javascript\nodejs\jm47.com\data\" + date.ToString("yyyy/MM/dd") + "/" + newid);

                var cdsd = db.GetDataSet("select * from jmprojectitem where projectid=" + oldid);
                var content = "";
                foreach (System.Data.DataRow dr2 in cdsd.Tables[0].Rows)
                {
                    content += dr2["itemvalue"];
                }
                if (string.IsNullOrWhiteSpace(content))
                {
                    var p = @"D:\项目\JiaMaoCode\wwwroot\haofefe\wwwroot\" +
                   path + "\\" + oldid + ".dat";
                   
                    if (System.IO.File.Exists(p))
                    {
                        content = DoNet.Common.IO.FileHelper.ReadAllText(p, Encoding.UTF8);
                    }
                }

                
                if (!string.IsNullOrWhiteSpace(content))
                {
                    //content = content.Replace("/WebData/ProCts/", "/Data/").Replace("http://www.jiamaocode.com", "").Replace("http://jiamaocode.com", "").Replace("jiamaocode.com", "");
                    //db.ExecuteNonQuery("update article set content=?ct where id=?id", new string[] { "?ct", "?id" }, new object[] { content, newid });
                    //db.ExecuteNonQuery("insert into comment(articleId,commentby,content,ip,commenton) select " + newid + ",account,recontent,reip,redate from jmrecontent where proid=" + oldid);
                    var p = @"D:\微云网盘\273389528\项目\javascript\nodejs\jm47.com\data\" + date.ToString("yyyy/MM/dd") + "/" + newid;
                    System.IO.Directory.CreateDirectory(p);
                    /*var fn = System.IO.Path.Combine(p, newid + ".htm");
                    if (!System.IO.File.Exists(fn))
                    {
                        System.IO.File.WriteAllText(fn, content, Encoding.UTF8);
                    }

                }
                
                Console.WriteLine(dr["title"].ToString() + " complete");
            }
            Console.WriteLine("total complete");*/

            //var p = new person();
            //DoNet.Common.PowerShell.ScriptRunner.RunScript("$arg.name=\"fefeding\"", new Dictionary<string, object>() {{"arg",p} });

            //Console.WriteLine(p.name);

            //var db = DoNet.Common.DbUtility.DbFactory.CreateDbORM("mysql", "server=172.22.97.228;port=3306;database=totaloapv2;user id=system;password=system;charset=utf8;default command timeout=10");
            //db.DBInfo.IsProxy=true;
            //var dataset = db.GetDataSet("select tbUser.ID as 'tbUser.ID', tbUser.UserName as 'tbUser.UserName', tbUser.UserPwd as 'tbUser.UserPwd', tbUser.UserType as 'tbUser.UserType' from tbUser where 1=1 and  tbUser.UserName = 'hanszhang'  and  tbUser.UserPwd = '123' ");

            //DoNet.Common.Reflection.ClassHelper.FastSetPropertyValue(p, p.GetType().GetProperty("mytime"), DateTime.Now, null);


            //var s = System.Net.WebSockets.WebSocketContext.
            //var test2 = new test2();
            //var test1 = (test1)test2;

            //Console.WriteLine(test2.add(1, 2));
            //Console.WriteLine(test1.add(1, 2));

            //Console.WriteLine("eh");

           // var par = "{\"ClassName\":\"TX.OAPServer.Bll.ProductionApp\",\"Method\":\"GetProductionsByUser\",\"Params\":[\"{\\\"IsFramework\\\":false,\\\"Localication\\\":\\\"zh_CN\\\",\\\"LoginType\\\":0,\\\"ProductionCode\\\":null,\\\"ProductionId\\\":null,\\\"SKey\\\":null,\\\"UserName\\\":\\\"fefeding\\\"}\"]}";

            //var bs = System.Text.Encoding.UTF8.GetBytes(par);

            //var http = DoNet.Common.Net.JMRequest.CreateHttp("http://ws.epr.ied.com/openpartner/serviceproxy.ashx");
            //http.Request.Method = "get";
            //http.Write(bs);

            //var response = http.GetResponse();
            
            ///var r = response.ReadToEnd();
            ///
            //System.Net.ServicePointManager.Expect100Continue = false;
            //var client = new System.Net.WebClient();
            //client.Encoding = System.Text.Encoding.UTF8;
            //var data = client.UploadString("http://ws.epr.ied.com/openpartner/serviceproxy.ashx", par);
            ////var r = System.Text.Encoding.UTF8.GetString(data);
            //Console.WriteLine(data);
            

            //var request = HttpWebRequest.Create("http://ws.epr.ied.com/openpartner/serviceproxy.ashx") as HttpWebRequest;
            //request.Method = "post";
            //request.ContentType = "text/plain; charset=utf-8";
            //request.ContentLength = bs.Length;
            //using (var stream = request.GetRequestStream())
            //{
            //    stream.Write(bs, 0, bs.Length);
            //    //stream.WriteByte(1);
            //}
            
            //using (var res = request.GetResponse() as HttpWebResponse)
            //{
            //    var stream = res.GetResponseStream();
            //    var data = new List<byte>();
            //    if (stream.CanSeek)
            //    {
            //        stream.Seek(0, System.IO.SeekOrigin.Begin);
            //    }
            //    var b = stream.ReadByte();
            //    while (b != -1)
            //    {
            //        data.Add((byte)b);
            //        b = stream.ReadByte();
            //    }
               
            //    var r = System.Text.Encoding.UTF8.GetString(data.ToArray());
            //    Console.WriteLine(r);
            //}
            //if (args != null && args.Length > 0)
            //{
            //    requesturl = args[0];
            //}
            //for (var i = 0; i < 1000; i++)
            //{
            //    var th = new System.Threading.Thread(new System.Threading.ThreadStart(request));
            //    th.IsBackground = true;
            //    th.Start();
            //}

            /*
            var dt = new System.Data.DataTable();
            dt.Columns.Add(new System.Data.DataColumn("ID", typeof(string)));
            dt.Columns.Add(new System.Data.DataColumn("ProductionCode", typeof(string)));
            dt.Columns.Add(new System.Data.DataColumn("ControlSqlID", typeof(bool)));

            var r1 = dt.NewRow();
            r1[0] = "test1";
            r1[1] = "test1";
            r1[2] = true;
            dt.Rows.Add(r1);

            var r2 = dt.NewRow();
            r2[0] = "test2";
            r2[1] = "test1";
            r2[2] = true;
            dt.Rows.Add(r2);
            var r3 = dt.NewRow();
            r3[0] = "test3";
            r3[1] = "test1";
            r3[2] = true;
            dt.Rows.Add(r3);
            var r4 = dt.NewRow();
            r4[0] = "test4";
            r4[1] = "test1";
            r4[2] = true;
            dt.Rows.Add(r4);
            var r5 = dt.NewRow();
            r5[0] = "test5";
            r5[1] = "test1";
            r5[2] = true;
            dt.Rows.Add(r5);
            var r6 = dt.NewRow();
            r6[0] = "test6";
            r6[1] = "test1";
            r6[2] = true;
            dt.Rows.Add(r6);
            var mapping = DoNet.Common.Data.DataConvert.CreateMapping("serverparam.ProductionId|id,serverparam.ProductionCode|ProductionCode,reportdataparam.SqlId|ControlSqlID,tbproductionID|id");
            var result = DoNet.Common.Data.DataConvert.ConvertDataTableToCollection<ServerData>(dt, mapping);
             * 
             * */

            //DoNet.Common.Net.WCFManager.Server.StartServerBind(typeof(wcf.Test));
            //DoNet.Common.Net.WCFManager.Server.StartServerBind(typeof(wcf.Test2), typeof(wcf.ITest), DoNet.Common.Net.WCFManager.WCFBindingType.BasicHttpBinding, "127.0.0.1", 8089, "test2.svc");
            /*
            var server = new DoNet.Common.Net.JMProxy.Server();
            server.AcceptRequestHandler += server_AcceptRequestHandler;
            
            Console.WriteLine("Server Starting...");
            server.Start(8089);
            Console.WriteLine("Server Listen 8089");

            var client = new DoNet.Common.Net.JMProxy.Client("127.0.0.1", 8089);
            client.ConnectColsedHandle += client_ConnectColsedHandle;
            client.ReceiveMessageHandle += client_ReceiveMessageHandle;
            client.Open();
            var s = Console.ReadLine();
            while (s != "exit")
            {
                if (s == "close")
                {
                    client.Close();
                    break;
                }
                client.Send(s);
                s = Console.ReadLine();
            }
            //stream.Close();
            //socket.Close();
            client.Close();*/

            var db = DoNet.Common.DbUtility.DbFactory.CreateDbORM("System.Data.SQLite", "Data Source=data/test;Pooling=true;FailIfMissing=false");

            db.Insert(new person() { id = "125", name = "fefeding" });
            db.Insert(new person[] { new person() { id = "126", name = "fefeding" }, new person() { id = "127", name = "fefeding" } });

            var data = db.Search<person>(1, 10, (p) => p.id != "");

            db.Delete<person>((p) => p.id == "125" || p.id == "127");

            data = db.Search<person>(1, 10, (p) => p.id != "");
            db.Delete(new person() { id="126"});      
            data = db.Search<person>(1, 10, (p) => p.id != "");
            Console.Write(data.ToString());
/*
            var context = new DoNet.Common.Net.HttpListenerController("/", @"E:\website\epr_demo");
            context.AddPrefix("http://127.0.0.1:8089/");
            context.Start();*/
/*
            var id = "abc";
            var name = "ere";

            var pars = new Dictionary<string, object>();
            var where = DoNet.Common.LinqExpression.Helper.DserExpressionToWhere<person>((obj) => obj.id == id && obj.name != name, pars,'@');

            Console.WriteLine(where);*/
            Console.Read();
        }

        static void client_ReceiveMessageHandle(object sender, DoNet.Common.Net.JMProxy.MessageEventArgs e)
        {
            Console.WriteLine("receive :" + System.Text.Encoding.UTF8.GetString(e.Packet.Data.ToArray()));
            
        }

        static void client_ConnectColsedHandle(object sender, EventArgs e)
        {
            Console.WriteLine("server连接被断开");
        }

        static void server_AcceptRequestHandler(object sender, EventArgs e)
        {
            var manager = sender as DoNet.Common.Net.JMProxy.MessageManager;
            manager.ReceiveMessageHandle += manager_ReceiveMessageHandle;
            manager.ConnectColsedHandle += manager_ConnectColsedHandle;
            manager.Send("欢迎加入");
        }

        static void manager_ConnectColsedHandle(object sender, EventArgs e)
        {
            Console.WriteLine("client连接被断开");
        }

        static void manager_ReceiveMessageHandle(object sender, DoNet.Common.Net.JMProxy.MessageEventArgs e)
        {
            Console.WriteLine("receive :" + System.Text.Encoding.UTF8.GetString(e.Packet.Data.ToArray()));
        }

        static string requesturl = "http://localhost:8080/index.js";
        static void request()
        {
            while (true)
            {
                try
                {
                    var client = new System.Net.WebClient();
                    var s = client.DownloadString(requesturl);
                    Console.WriteLine(s);
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                    
                }
                System.Threading.Thread.Sleep(50);
            }
        }

        static void Log()
        {
            var index=0;
            while (index < 1000)
            {
                DoNet.Common.IO.Logger.Write("logger:" + index);
                //DoNet.Common.IO.Logger.Debug("debug-logger:" + index);
                DoNet.Common.IO.Logger.Debug("person:",new person(),2,0.5);
                index++;
            }

            Console.WriteLine("log-end:" +DateTime.Now.ToString());
        }
    }


    public class test1
    {
        public virtual int add(int a,int b) {
            return a + b;
        }
    }

    public class test2 : test1
    {
        public override int add(int a, int b)
        {
            return a * 2 + b;
        }
    }

    [DoNet.Common.Data.Table(Name="tbtest")]
    public class person
    {
        [DoNet.Common.Data.TableField(Name = "id",IsPrimary=true)]
        public string id { get; set; }
        [DoNet.Common.Data.TableField(Name = "name")]
        public string name { get; set; }

        public DateTime? mytime { get; set; }
    }

    /// <summary>
    /// 服务端数据返回格式
    /// </summary>
    [DataContract]
    [Serializable]
    public class ServerData
    {
        public string id { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        [DataMember]
        public ServerResponse respons { get; set; }

        public ServerResponse serverparam { get; set; }

        public ServerResponse reportdataparam { get; set; }

        public string tbproductionID { get; set; }

        /// <summary>
        /// 转json为当前 对象
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ServerData FromJSON(string source)
        {
            var obj = DoNet.Common.Serialization.JSon.JsonToModel<ServerData>(source);
            return obj;
        }
    }

    /// <summary>
    /// 请求返回结果
    /// </summary>
    [DataContract]
    [Serializable]
    public class ServerResponse
    {
        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public string state { get; set; }

        public string Id { get; set; }

        /// <summary>
        /// 结果字符
        /// </summary>
        [DataMember]
        public ReportData result { get; set; }

        public string ProductionCode { get; set; }

        public string ProductionId { get; set; }

        public string SqlId { get; set; }
        ///// <summary>
        ///// 结果数据
        ///// </summary>       
        //public ReportData data { 
        //    get {
        //        if ("1".Equals(state) || "success".Equals(state, StringComparison.OrdinalIgnoreCase))
        //        {
        //            try
        //            {
        //                return ReportData.FromJSON(result);
        //            }
        //            catch (Exception ex)
        //            {
        //                var v = new ReportData();
        //                v.HasError = true;
        //                v.Message = result;
        //                return v;
        //            }
        //        }
        //        else {
        //            var v = new ReportData();
        //            v.HasError = true;
        //            v.Message = result;
        //            return v;
        //        }
        //} }
    }

    /// <summary>
    /// 数据传输的基类
    /// </summary>
    [Serializable]
    [DataContract]
    public class BaseModel
    {
        /// <summary>
        /// 服务端返回的消息
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// 是否有异常
        /// </summary>
        [DataMember]
        public bool HasError { get; set; }
    }
    /// <summary>
    /// 数据列字段映射信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class ColumnDisplay
    {
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// 所属的SQL语句ID
        /// </summary>
        [DataMember]
        public string ControlSqlId { get; set; }

        /// <summary>
        /// 原列名
        /// </summary>
        [DataMember]
        public string ColumnName { get; set; }

        /// <summary>
        /// 替换后的列名
        /// </summary>
        [DataMember]
        public string DisplayName { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [DataMember]
        public string Description { get; set; }
    }
    /// <summary>
    /// 查询结果集
    /// </summary>
    [DataContract]
    [Serializable]
    public class ReportData : BaseModel
    {
        public ReportData()
        {
            Tables = new List<DataTable>();
        }

        public ReportData(System.Data.DataSet ds, IEnumerable<ColumnDisplay> mappings = null)
        {
            Tables = new List<DataTable>();
            foreach (System.Data.DataTable dt in ds.Tables)
            {
                Tables.Add(new DataTable(dt, mappings));
            }
        }

        public ReportData(System.Data.DataSet ds, string encoding, IEnumerable<ColumnDisplay> mappings = null)
        {
            Tables = new List<DataTable>();
            foreach (System.Data.DataTable dt in ds.Tables)
            {
                Tables.Add(new DataTable(dt, mappings, encoding));
            }
        }


        /// <summary>
        /// 所有的表
        /// </summary>
        [DataMember]
        public List<DataTable> Tables { get; set; }

        public bool HasData
        {
            get
            {
                if (Tables != null && Tables.Count > 0)
                {
                    foreach (var tb in Tables)
                    {
                        if (tb.Rows != null && tb.Rows.Count<DataRow>() > 0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 转为json
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return DoNet.Common.Serialization.JSon.ModelToJson(this);
        }

        /// <summary>
        /// 转json为当前 对象
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ReportData FromJSON(string source)
        {
            var obj = DoNet.Common.Serialization.JSon.JsonToModel<ReportData>(source);
            return obj;
        }

        public override string ToString()
        {
            return DoNet.Common.Serialization.FormatterHelper.XMLSerObjectToString(this);
        }

        public static ReportData FromString(string source)
        {
            var obj = DoNet.Common.Serialization.FormatterHelper.XMLDerObjectFromString<ReportData>(source);
            return obj;
        }
    }

    /// <summary>
    /// 数据表
    /// </summary>
    [Serializable]
    [DataContract]
    public class DataTable
    {
        public DataTable()
        {
        }

        /// <summary>
        /// 通过表实例化此对象
        /// </summary>
        /// <param name="dt"></param>
        public DataTable(System.Data.DataTable dt, IEnumerable<ColumnDisplay> mappings = null, string encoding = "utf-8")
        {
            FromTable(dt, mappings, encoding);
        }

        /// <summary>
        /// 表名
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 列
        /// </summary>
        [DataMember]
        public DataColumn[] Columns { get; set; }

        /// <summary>
        /// 列映射配置
        /// </summary>
        [DataMember]
        public ColumnDisplay[] ColumnMapping
        {
            get;
            set;
        }

        /// <summary>
        /// 所有数据行
        /// </summary>
        [DataMember]
        public DataRow[] Rows { get; set; }

        /// <summary>
        /// 从源表转为当前格式
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ColumnMappings"></param>
        public void FromTable(System.Data.DataTable dt, IEnumerable<ColumnDisplay> mappings = null, string encoding = "utf-8")
        {
            if (dt != null)
            {
                this.Name = dt.TableName;
                //初始化列信息
                Columns = new DataColumn[dt.Columns.Count];
                for (var i = 0; i < Columns.Length; i++)
                {
                    Columns[i] = new DataColumn(dt.Columns[i], mappings);
                    Columns[i].Index = i;
                }

                this.Rows = new DataRow[dt.Rows.Count];
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    this.Rows[i] = new DataRow(dt.Rows[i], Columns, encoding);
                }

                if (mappings != null) this.ColumnMapping = mappings.ToArray<ColumnDisplay>();
            }
        }


    }

    /// <summary>
    /// 列信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class DataColumn
    {
        public DataColumn() { }
        public DataColumn(System.Data.DataColumn dc, IEnumerable<ColumnDisplay> mappings = null)
        {
            FromColumn(dc, mappings);
        }

        /// <summary>
        /// 字段名
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 显示的名称
        /// </summary>
        [DataMember]
        public string DisplayName { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        [DataMember]
        public string DataType { get; set; }

        /// <summary>
        /// 在当前列中索引
        /// </summary>
        [DataMember]
        public int Index { get; set; }

        /// <summary>
        /// 当前列说明文字
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// 转换列
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="mappings"></param>
        /// <returns></returns>
        private void FromColumn(System.Data.DataColumn dc, IEnumerable<ColumnDisplay> mappings = null)
        {
            this.DataType = dc.DataType.FullName;
            if (this.DataType.Equals("System.Byte[]", StringComparison.OrdinalIgnoreCase))
            {
                this.DataType = "System.String";
            }

            this.Name = dc.ColumnName.Trim();

            if (mappings != null)
            {
                //多层表头处理
                var cols = this.Name.Split('_');
                var cn = this.Name.Replace("_", "");
                foreach (var col in cols)
                {
                    if (string.IsNullOrWhiteSpace(col)) continue;
                    foreach (var m in mappings)
                    {
                        if (cn.Equals(m.ColumnName.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            this.DisplayName = m.DisplayName;
                            this.Description = m.Description;
                            break;
                        }
                        else if (col.Equals(m.ColumnName.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            this.DisplayName += m.DisplayName + "_";
                            this.Description += m.Description;
                            break;
                        }
                    }
                }
                if (string.IsNullOrWhiteSpace(this.DisplayName))
                {
                    this.DisplayName = this.Name;
                }
                else
                {
                    this.DisplayName = this.DisplayName.Trim('_');
                }
            }
        }
    }

    /// <summary>
    /// 数据项
    /// </summary>
    [DataContract]
    [Serializable]
    public class DataItem
    {

        /// <summary>
        /// 对应的列
        /// </summary>
        [DataMember]
        public string ColumnName { get; set; }

        /// <summary>
        /// 当前值
        /// </summary>
        [DataMember]
        public string Value { get; set; }
    }

    /// <summary>
    /// 表行
    /// </summary>
    [Serializable]
    [DataContract]
    public class DataRow
    {
        public DataRow() { }
        public DataRow(System.Data.DataRow dr, DataColumn[] columns, string encoding)
        {
            FromRow(dr, columns, encoding);
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DataItem this[int index]
        {
            get { return ItemArray[index]; }
            set { ItemArray[index] = value; }
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DataItem this[string columnName]
        {
            get
            {
                foreach (var item in ItemArray)
                {
                    if (columnName.Equals(item.ColumnName, StringComparison.OrdinalIgnoreCase))
                        return item;
                }
                return null;
            }
            set
            {
                var item = this[columnName];
                if (item != null)
                {
                    item.Value = value.Value;
                }
            }
        }


        /// <summary>
        /// 当前行数值
        /// </summary>
        [DataMember]
        public DataItem[] ItemArray { get; set; }

        private void FromRow(System.Data.DataRow dr, DataColumn[] columns, string encoding = "utf-8")
        {
            ItemArray = new DataItem[dr.ItemArray.Length];
            for (var i = 0; i < ItemArray.Length; i++)
            {
                var obj = dr.ItemArray[i];
                ItemArray[i] = new DataItem()
                {
                    ColumnName = columns[i].Name
                };
                if (obj != null && obj != DBNull.Value)
                {
                    if (obj.GetType().FullName.Equals("System.Byte[]", StringComparison.OrdinalIgnoreCase))
                    {
                        ItemArray[i].Value = System.Text.Encoding.GetEncoding(encoding).GetString((byte[])obj);
                    }
                    else
                    {
                        ItemArray[i].Value = obj.ToString();
                    }
                }
            }
        }
    }
}
