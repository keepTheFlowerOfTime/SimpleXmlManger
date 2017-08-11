using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
* Author: gxw
* Time: 2017/7/12 11:31:54
*
*/
namespace MyLib.Xml.Document
{
    public abstract class BaseDocumentData : IDocumentNode
    {
        public const string DEFAULT_NAME = "Data";
        string m_name;
        IGroupNode m_items;
        INodeFactory m_nodeFactory;
        public BaseDocumentData(INodeFactory nodeFactory)
        {
            m_nodeFactory = nodeFactory;
        }

        public void Read(string filePath)
        {
            if (filePath == null)
            {
                m_items = m_nodeFactory.CreateGroupNodeFromData(new Internal.ReadArgs());
                m_items.Name = DEFAULT_NAME;
            }
            else
            {

                m_items = new SimpleXmlManager(m_nodeFactory).Read(filePath,Name) as IGroupNode;
            }
        }

        public void Release()
        {
            m_items.RemoveAll();
        }

        public void Close()
        {
            Release();
            m_items = null;
        }

        public string Name
        {
            get
            {
                if (string.IsNullOrWhiteSpace(m_name)) return DEFAULT_NAME;
                return m_name;
            }
            set
            {
                m_name = value;
            }
        }

        public void Sort(Comparison<INode> comparision)
        {
            m_items.ChildNodes.Sort(comparision);
        }

        public void Sort(Action<ICollection<INode>> sortHandle)
        {
            m_items.ChildNodes.Sort(sortHandle);
        }

        public IGroupNode Items
        {
            get
            {
                return m_items;
            }
        }
    }
}
