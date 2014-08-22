/////////////////////////////////////////////////////////
// Author   : jiamao
// Date     : 2011/8/10 11:20:18
// Usage    :分页数据
/////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DoNet.Common.DbUtility
{
    /// <summary>
    /// 分页数据
    /// </summary>
    [Serializable]
    [DataContract]
    public class PageData<T>:IEnumerable<T> where T:class
    {
        /// <summary>
        /// 当前查得的数据
        /// </summary>
        [DataMember]
        public ICollection<T> Data;

        /// <summary>
        /// 当前记录数
        /// </summary>       
        public int Count {
            get {
                if (Data == null) return 0;
                return Data.Count;
            }
        }

        /// <summary>
        /// 当前第几页
        /// </summary>
        [DataMember]
        public int Index;

        /// <summary>
        /// 总共查得多少页
        /// </summary>
        [DataMember]
        public int PageCount;

        /// <summary>
        /// 总共有多少符合条件的记录
        /// </summary>
        [DataMember]
        public int DataCount;

        # region IEnumerator接口

        public IEnumerator<T> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        # endregion
    }
}
