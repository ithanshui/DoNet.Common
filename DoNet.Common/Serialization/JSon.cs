//////////////////////////////////////////////////
// Author   : jiamao
// Date     : 2010/09/15
// Usage    : JSON序列化
//////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization.Json;

namespace DoNet.Common.Serialization
{
    /// <summary>
    /// JSON序列化
    /// </summary>
    public class JSon
    {
        /// <summary>
        /// 把对象序列化成JSON字符
        /// </summary>
        /// <param name="obj">待序列化的对象</param>
        /// <returns></returns>
        public static string ModelToJson(object obj)
        {
            System.Runtime.Serialization.Json.DataContractJsonSerializer ser = new DataContractJsonSerializer(obj.GetType());
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                ser.WriteObject(ms, obj);
                string js = System.Text.Encoding.UTF8.GetString(ms.ToArray());
                return js;
            }            
        }

        /// <summary>
        /// 把JSON反序列化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="js"></param>
        /// <returns></returns>
        public static T JsonToModel<T>(string js)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(js)))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                T obj = (T)ser.ReadObject(ms);
                return obj;
            }
        }
    }
}
