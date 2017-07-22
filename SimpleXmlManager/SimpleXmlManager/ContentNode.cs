using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyLib.Xml
{
    public interface IContentNode:INode
    {
        string Content { get; set; }
    }
    public class ContentNode : Node,IContentNode
    {
        public const string DEFAULT_CONTENT = "Empty";
        string m_content;
        public ContentNode()
        {

        }

        public ContentNode(ChaosNode chaosNode):base(chaosNode)
        {
            m_content = chaosNode.Content;
        }

        public override XmlNode ToXmlNode(XmlDocument document)
        {
            XmlNode element = document.CreateElement(Name);
            element.InnerText = Content;
            foreach (string attriName in m_attributes.Keys)
            {
                XmlAttribute attri = document.CreateAttribute(attriName);
                attri.Value = m_attributes[attriName];
                element.Attributes.Append(attri);
            }
            return element;
        }

        public override XmlNode ToXmlNodeWithChildNode(XmlDocument document)
        {
            return ToXmlNode(document);
        }

        #region 公有属性
        public string Content
        {
            get
            {
                if (string.IsNullOrWhiteSpace(m_content)) return DEFAULT_CONTENT;
                return m_content;
            }

            set
            {
                m_content = value;
            }
        }
        #endregion
    }
}
