using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Collections;

namespace MyLib.Xml
{
    public enum SimpleNodeType
    {
        Chaos,Group,Content
    }

    public interface INode
    {
        XmlNode ToXmlNode(XmlDocument document);

        XmlNode ToXmlNodeWithChildNode(XmlDocument document);

        string Name { get; set; }

        IGroupNode Owner { get; set; }

        object Tag { get; set; }

        bool IsGroupNode { get; }

        Dictionary<string, string> Attributes { get; }

        /// <summary>
        /// 深度，根节点的深度为0
        /// </summary>
        int Depth
        {
            get;
        }
    }

    public abstract class Node : INode
    {
        protected string m_name;
        object m_tag;
        protected IGroupNode m_owner;
        protected Dictionary<string, string> m_attributes;

        public const string ATTRI_NODE_KIND = "__nodeKind";
        public const string KIND_GROUP_NODE = "groupNode";
        public const string KIND_CONTENT_NODE = "contentNode";

        protected Node()
        {
            m_attributes = new Dictionary<string, string>();
        }

        protected Node(Node node)
        {
            m_name = node.Name;
            m_tag = node.Tag;
            m_owner = node.Owner;
            m_attributes = node.Attributes;
        }

        //转化string[]为string，并以\\做为分隔符
        internal static string ConvertStringArrayToString(string[] stringArray, string separator = "\\")
        {
            return string.Join(separator, stringArray);
        }
        internal static string ConvertStringArrayToString(object[] itemArray, string separator = "\\")
        {
            string[] temp = new string[itemArray.Length];
            for (int i = 0; i < itemArray.Length; i++)
                temp[i] = itemArray[i].ToString();
            return ConvertStringArrayToString(temp, separator);
        }
        internal static string[] ConvertStringToStringArray(string content, params char[] separator)
        {
            return content.Split(separator);
        }

        public abstract XmlNode ToXmlNode(XmlDocument document);

        public abstract XmlNode ToXmlNodeWithChildNode(XmlDocument document);

        public string Name
        {
            get
            {
                if (string.IsNullOrWhiteSpace(m_name)) return GetType().Name;
                return m_name;
            }
            set
            {
                m_name = value;
            }
        }

        public object Tag
        {
            get
            {
                return m_tag;
            }

            set
            {
                m_tag = value;
            }
        }

        public Dictionary<string, string> Attributes
        {
            get
            {
                return m_attributes;
            }
        }

        public IGroupNode Owner
        {
            get
            {
                return m_owner;
            }

            set
            {
                m_owner = value;
            }
        }

        public virtual bool IsGroupNode
        {
            get
            {
                return this is IGroupNode;
            }
        }

        public int Depth
        {
            get
            {
                int depth = 0;
                INode node = this;
                while(node.Owner!=null)
                {
                    ++depth;
                    node = node.Owner;
                }
                return depth;
            }
        }
    }


}
