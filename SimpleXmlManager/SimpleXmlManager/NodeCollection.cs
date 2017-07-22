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
