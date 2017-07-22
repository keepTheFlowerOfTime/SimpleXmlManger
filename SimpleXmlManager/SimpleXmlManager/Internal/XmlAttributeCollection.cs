using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
* Author: gxw
* Time: 2017/7/9 10:50:04
*
*/
namespace MyLib.Xml.Internal
{
    public class XmlAttributeCollection:IList<IXmlAttribute>
    {
        List<IXmlAttribute> m_attributes;
        internal XmlAttributeCollection()
        {
            m_attributes = new List<IXmlAttribute>();
        }

        public IXmlAttribute this[int index]
        {
            get
            {
                return ((IList<IXmlAttribute>)m_attributes)[index];
            }

            set
            {
                ((IList<IXmlAttribute>)m_attributes)[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return ((IList<IXmlAttribute>)m_attributes).Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return ((IList<IXmlAttribute>)m_attributes).IsReadOnly;
            }
        }

        public void Add(IXmlAttribute item)
        {
            ((IList<IXmlAttribute>)m_attributes).Add(item);
        }

        public void Clear()
        {
            ((IList<IXmlAttribute>)m_attributes).Clear();
        }

        public bool Contains(IXmlAttribute item)
        {
            return ((IList<IXmlAttribute>)m_attributes).Contains(item);
        }

        public void CopyTo(IXmlAttribute[] array, int arrayIndex)
        {
            ((IList<IXmlAttribute>)m_attributes).CopyTo(array, arrayIndex);
        }

        public IEnumerator<IXmlAttribute> GetEnumerator()
        {
            return ((IList<IXmlAttribute>)m_attributes).GetEnumerator();
        }

        public int IndexOf(IXmlAttribute item)
        {
            return ((IList<IXmlAttribute>)m_attributes).IndexOf(item);
        }

        public void Insert(int index, IXmlAttribute item)
        {
            ((IList<IXmlAttribute>)m_attributes).Insert(index, item);
        }

        public bool Remove(IXmlAttribute item)
        {
            return ((IList<IXmlAttribute>)m_attributes).Remove(item);
        }

        public void RemoveAt(int index)
        {
            ((IList<IXmlAttribute>)m_attributes).RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList<IXmlAttribute>)m_attributes).GetEnumerator();
        }
    }
}
