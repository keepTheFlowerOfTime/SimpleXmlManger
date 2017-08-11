using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
* Author: gxw
* Time: 2017/7/21 11:02:57
*
*/
namespace MyLib.Xml.Internal
{
    public class ReadContext
    {
        Stack<IXmlNode> m_parentNodes;
        internal ReadContext()
        {
            m_parentNodes = new Stack<IXmlNode>();
        }

        internal void PushParentNode(IXmlNode xmlNode)
        {
            m_parentNodes.Push(xmlNode);
        }

        internal void PopParentNode()
        {
            m_parentNodes.Pop();
        }

        internal IXmlNode PeekParentNode()
        {
            if (m_parentNodes.Count == 0) return null;
            return m_parentNodes.Peek();
        }

        internal int Depth
        {
            get
            {
                if (NodeIsEnd) return m_parentNodes.Count;
                else return m_parentNodes.Count - 1;
            }
        }

        internal int PreviousDepth
        {
            get;
            set;
        }

        public INode RootNode
        {
            get;
            set;
        }

        public bool NodeIsEnd
        {
            get;
            internal set;
        }

        internal INode NowNode
        {
            get;
            set;
        }

        internal System.Xml.XmlNodeType PreviousNodeType
        {
            get;
            set;
        }
    }
}
