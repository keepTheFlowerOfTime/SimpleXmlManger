using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
* Author: gxw
* Time: 2017/7/12 11:31:16
*
*/
namespace MyLib.Xml.Document
{
    public abstract class BaseDocumentHead : IDocumentNode
    {
        public const string DEFAULT_NAME = "Head";
        IGroupNode m_items;
        string m_name;
        SimpleXmlManager m_xmlManager;
        public BaseDocumentHead(INodeFactory nodeFactory)
        {
            m_xmlManager = new SimpleXmlManager(nodeFactory);
        }

        public void Read(string filePath)
        {
            if (filePath == null)
            {
                m_items = m_xmlManager.GroupNodeCreateHandle(new Internal.ReadArgs());
                m_items.Name = DEFAULT_NAME;
            }
            else
                m_items = m_xmlManager.Read(filePath, Name) as IGroupNode;
        }

        public void Release()
        {
            (m_items as IGroupNode)?.ChildNodes.Clear();
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

        public IGroupNode Items
        {
            get
            {
                return m_items;
            }
        }
    }
}
