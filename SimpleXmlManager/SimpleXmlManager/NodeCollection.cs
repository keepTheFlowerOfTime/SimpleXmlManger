using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
* Author: gxw
* Time: 2017/7/12 21:27:16
*
*/
namespace MyLib.Xml
{
    public class NodeCollection : IEnumerable<INode>
    {
        ICollection<INode> m_collection;
        internal NodeCollection(ICollection<INode> collection)
        {
            m_collection = collection;
        }

        internal void Add(INode node)
        {
            m_collection.Add(node);
        }

        internal void Remove(INode node)
        {
            m_collection.Remove(node);
        }

        internal void Clear()
        {
            m_collection.Clear();
        }

        /// <summary>
        /// if collection is List,then Sort,else throw NotSupportException
        /// </summary>
        /// <param name="comparision"></param>
        internal void Sort(Comparison<INode> comparision)
        {
            if (m_collection is List<INode>) (m_collection as List<INode>).Sort(comparision);
            else throw new NotSupportedException("Not Support Function");
        }

        /// <summary>
        /// 使用sortHandle方法对Collection进行排序
        /// </summary>
        /// <param name="sortHandle"></param>
        public void Sort(Action<ICollection<INode>> sortHandle)
        {
            sortHandle(m_collection);
        }

        public IEnumerator<INode> GetEnumerator()
        {
            return m_collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_collection.GetEnumerator();
        }
    }
}
