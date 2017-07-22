using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyLib.Xml
{
    public interface IGroupNode:INode
    {
        INode FirstChild { get; }
        INode LastChild { get; }

        void Add(INode node);

        void Remove(INode node);

        void RemoveAll();

        NodeCollection ChildNodes { get; }
    }

    public abstract class BaseGroupNode : Node, IGroupNode
    {
        public BaseGroupNode(ChaosNode chaosNode):base(chaosNode)
        {

        }

        public BaseGroupNode() { }

        public abstract NodeCollection ChildNodes { get; }
        public abstract INode FirstChild { get; }
        public abstract INode LastChild { get; }

        public abstract void Add(INode node);
        public abstract void Remove(INode node);

        public abstract void RemoveAll();

        public override XmlNode ToXmlNode(XmlDocument document)
        {
            XmlElement element = document.CreateElement(Name);
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
            XmlElement root = (XmlElement)ToXmlNode(document);
            foreach (Node node in ChildNodes) root.AppendChild(node.ToXmlNodeWithChildNode(document));
            return root;
        }
    }

    public class DefaultGroupNode : BaseGroupNode,IGroupNode
    {
        NodeCollection m_childNodes;
        public DefaultGroupNode()
        {
            m_childNodes = new NodeCollection(new List<INode>());
        }

        public DefaultGroupNode(ChaosNode chaosNode):base(chaosNode)
        {
            m_childNodes = chaosNode.ChildNodes;
        }

        public override void Add(INode node)
        {
            node.Owner = this;
            m_childNodes.Add(node);
        }

        public override void RemoveAll()
        {
            foreach (var node in m_childNodes) node.Owner = null;
            m_childNodes.Clear();
        }

        public override void Remove(INode node)
        {
            m_childNodes.Remove(node);
            node.Owner = this;
        }

        public override INode FirstChild
        {
            get
            {
                return m_childNodes.First();
            }
        }

        public override INode LastChild
        {
            get
            {
                return m_childNodes.Last();
            }
        }

        public override NodeCollection ChildNodes
        {
            get
            {
                return m_childNodes;
            }
        }

    }
}
