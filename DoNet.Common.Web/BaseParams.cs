using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DoNet.Common.Web
{
    /// <summary>
    /// 基础参数
    /// </summary>
    public class BaseParams
    {
        /// <summary>
        /// WEB根路径标识
        /// </summary>
        public const string WebRootMark = "~";

        /// <summary>
        /// 当前绝对的根路径
        /// </summary>
        public static string RootUrl
        {
            get
            {
                var _rootUrl = string.Format("{0}://{1}{2}", HttpContext.Current.Request.Url.Scheme,
                                        HttpContext.Current.Request.Url.Host,
                                        HttpContext.Current.Request.ApplicationPath).TrimEnd('/');

                return _rootUrl;
            }
        }

        static string _rootphysicalpath;
        /// <summary>
        /// WEB的物理根路径
        /// </summary>
        public static string RootPhysicalpath
        {
            get {
                if (string.IsNullOrWhiteSpace(_rootphysicalpath))
                {
                    _rootphysicalpath = HttpContext.Current.Server.MapPath(WebRootMark);
                }
                return _rootphysicalpath;
            }
        }
    }
}
