using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

/*
* Author: gxw
* Time: 2017/7/20 10:17:49
*
*/
namespace MyLib.Xml
{
    /// <summary>
    /// 无法判定节点类型的节点，根据对该节点方法调用，会自动的判断该节点的类型,默认作为GroupNode
    /// </summary>
    public sealed class ChaosNode:Node,IContentNode,IGroupNode
    {
        SimpleNodeType m_type;
        bool m_isGroupNode;
        string m_content;
        NodeCollection m_childNodes;
        internal ChaosNode()
        {
            m_isGroupNode = true;
            m_type = SimpleNodeType.Chaos;
        }

        private void InitLikeContent()
        {
            m_content = "";
        }

        private void InitLikeGroup()
        {
            m_childNodes = new NodeCollection(new List<INode>());
        }

        private XmlNode AutoToXmlNode(XmlDocument document)
        {
            if (IsGroupNode) return new DefaultGroupNode(this).ToXmlNode(document);
            else return new ContentNode(this).ToXmlNode(document);
        }

        private void AutoAdapt(SimpleNodeType type)
        {
            JudgeOperation(type);
            SetNodeType(type);
        }

        private void JudgeOperation(SimpleNodeType type)
        {
            if (Type == SimpleNodeType.Chaos) return;
            if (type == Type) return;
            throw new NotSupportedException();
        }

        private void SetNodeType(SimpleNodeType type)
        {
            m_type = type;
            if (Type == SimpleNodeType.Group)
            {
                m_isGroupNode = true;
                InitLikeGroup();
            }
            else if (Type == SimpleNodeType.Content)
            {
                m_isGroupNode = false;
                InitLikeContent();
            }
        }

        private XmlNode AutoToXmlNodeWithChildNode(XmlDocument document)
        {
            if (IsGroupNode) return new DefaultGroupNode(this).ToXmlNodeWithChildNode(document);
            else return new ContentNode(this).ToXmlNodeWithChildNode(document);
        }

        public override XmlNode ToXmlNode(XmlDocument document)
        {
            return AutoToXmlNode(document);
        }

        public override XmlNode ToXmlNodeWithChildNode(XmlDocument document)
        {
            return AutoToXmlNodeWithChildNode(document);
        }

        public void Add(INode node)
        {
            AutoAdapt(SimpleNodeType.Group);
            m_childNodes.Add(node);
            node.Owner = this;
        }

        public void Remove(INode node)
        {
            AutoAdapt(SimpleNodeType.Group);
            m_childNodes.Remove(node);
            node.Owner = null;
        }

        public void RemoveAll()
        {
            AutoAdapt(SimpleNodeType.Group);
            foreach (var node in ChildNodes) node.Owner = null;
            m_childNodes.Clear();
        }

        public override bool IsGroupNode
        {
            get
            {
                return m_isGroupNode;
            }
        }

        public string Content
        {
            get
            {
                AutoAdapt(SimpleNodeType.Content);
                return Content;
            }

            set
            {
                AutoAdapt(SimpleNodeType.Content);
                m_content = value;
            }
        }

        public SimpleNodeType Type
        {
            get
            {
                return m_type;
            }
        }

        public INode FirstChild
        {
            get
            {
                AutoAdapt(SimpleNodeType.Group);
                return m_childNodes.First();
            }
        }

        public INode LastChild
        {
            get
            {
                AutoAdapt(SimpleNodeType.Group);
                return m_childNodes.Last();
            }
        }

        public NodeCollection ChildNodes
        {
            get
            {
                AutoAdapt(SimpleNodeType.Group);
                return m_childNodes;
            }
        }
    }
}
