using MyLib.Xml.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib.Xml
{
    public interface INodeFactory
    {
        INode CreateRootNodeFromData(ReadArgs args);

        IContentNode CreateContentNodeFromData(ReadArgs args);

        IGroupNode CreateGroupNodeFromData(ReadArgs args);
    }
    /// <summary>
    /// 默认的节点工厂，对于所有节点，会读取其名字及所有的属性，
    /// 特别对于ContentNode，会额外读取其Content值，
    /// 默认返回DefaultGroupNode或DefaultContentNode
    /// </summary>
    public class DefaultNodeFactory : INodeFactory
    {
        public static void ReadAllAttributeInXmlNode(INode node,ReadArgs args)
        {
            if (node.Attributes != null)
            {
                foreach (var attri in args.Attributes)
                   node.Attributes[attri.Key] = attri.Value;
            }
        }

        protected virtual IContentNode CreateEmptyContentNode()
        {
            return new ContentNode();
        }

        protected virtual IGroupNode CreateEmptyGroupNode()
        {
            return new DefaultGroupNode();
        }

        public IContentNode CreateContentNodeFromData(ReadArgs args)
        {
            IContentNode contentNode = CreateEmptyContentNode();
            contentNode.Name = args.Name;
            contentNode.Content = args.Value;
            ReadAllAttributeInXmlNode(contentNode, args);
            return contentNode;
        }

        public IGroupNode CreateGroupNodeFromData(ReadArgs args)
        {
            IGroupNode groupNode = CreateEmptyGroupNode();
            groupNode.Name = args.Name;
            ReadAllAttributeInXmlNode(groupNode, args);
            return groupNode;
        }

        public INode CreateRootNodeFromData(ReadArgs args)
        {
            if (args.Type == XmlNodeType.Content)
                return CreateContentNodeFromData(args);
            else return CreateGroupNodeFromData(args);
        }
    }
}
