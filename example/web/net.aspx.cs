/////////////////////////////////////////////////////////
// Author   : jiamao
// Date     : 2011/8/5 16:44:14
// Usage    :
/////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DoNet.Common.Examples.web
{
    public partial class net : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("当前服务端IP：" + DoNet.Common.Net.BaseNet.GetLocalIPAddress());
            Response.Write("<br />");
            Response.Write("当前服务端机器名：" + DoNet.Common.Net.BaseNet.GetHostName());
            Response.Write("<br />");
            Response.Write("当前访问者IP:" + DoNet.Common.Net.BaseNet.GetWebIPAddress());

            Button1.Click += new EventHandler(Button1_Click);
        }

        void Button1_Click(object sender, EventArgs e)
        {
            //发送邮件
            var mail = new DoNet.Common.Net.MailHelper();

            //发送
            //如果需要发送附件等请看其它重载
            mail.QueueSend(txtsmtp.Text, txtmail.Text, txtuser.Text, txtpwd.Text, txtreceiver.Text, txtsubject.Text, txtcontent.Text, true);
            mail.Startup();

            Page.ClientScript.RegisterStartupScript(this.GetType(), "ok", "alert('发送完成');", true);
        }
    }
}
