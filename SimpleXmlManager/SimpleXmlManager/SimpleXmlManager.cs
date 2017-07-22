using MyLib.Xml.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib.Xml
{
    public class SimpleXmlManager
    {
        private XmlReader m_xmlReader;
        private XmlWriter m_xmlWriter;
        private INode m_previousReadNode;

        public Func<ReadArgs, IGroupNode> GroupNodeCreateHandle;
        public Func<ReadArgs, IContentNode> ContentNodeCreateHandle;
        public Func<ReadArgs, INode> RootNodeCreateHandle;

        static SimpleXmlManager s_instance;
        public static SimpleXmlManager Default
        {
            get
            {
                if (s_instance == null) s_instance = new SimpleXmlManager(new DefaultNodeFactory());
                return s_instance;
            }
        }

        /// <summary>
        /// 使用NodeFactory的接口初始化ContentNodeCreateHandle、
        /// RootNodeCreateHandle、ReadRootNodeHandle;
        /// </summary>
        /// <param name="factory"></param>
        public SimpleXmlManager(INodeFactory factory)
        {
            m_xmlReader = new XmlReader();
            m_xmlReader.ReadSingleNodeEnd += OnReadNodeEnd;
            m_xmlReader.ReadSingleNodeStart+= OnReadNodeStart;
            m_xmlWriter = new XmlWriter();

            RootNodeCreateHandle = factory.CreateRootNodeFromData;
            GroupNodeCreateHandle = factory.CreateGroupNodeFromData;
            ContentNodeCreateHandle = factory.CreateContentNodeFromData;
        }

        /// <summary>
        /// 使用默认的DefaultNodeFactory初始化该类
        /// </summary>
        public SimpleXmlManager() : this(new DefaultNodeFactory())
        {

        }

        /// <summary>
        /// 从指定的文件路径读入文件流,出现异常返回null。
        /// 当filePath为null时，返回一个由factory创建root空节点
        /// </summary>
        /// <param name="filePath"></param>
        public INode Read(string filePath)
        {
            if (filePath == null) return RootNodeCreateHandle(new ReadArgs());
            try
            {
                return m_xmlReader.Read(filePath).RootNode as IGroupNode;
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                return null;
            }
            catch (System.IO.FileNotFoundException)
            {
                return null;
            }
        }

        /// <summary>
        /// 在指定的文件路径写入文件
        /// </summary>
        /// <param name="filePath"></param>
        public void Write(IGroupNode rootNode, string filePath)
        {
            string directory = System.IO.Path.GetDirectoryName(System.IO.Path.GetFullPath(filePath));
            if (!System.IO.Directory.Exists(directory)) System.IO.Directory.CreateDirectory(directory);
            m_xmlWriter.Write(rootNode, filePath);
        }

        /// <summary>
        /// 打开指定路径的文件，然后读取名为nodeName的节点及其子节点
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public INode Read(string filePath, string nodeName)
        {
            return m_xmlReader.ReadNode(filePath, nodeName).RootNode;
        }


        private void OnReadNodeEnd(ReadContext context, ReadArgs readArgs)
        {
            if (readArgs.Type == XmlNodeType.Content)
                (context.NowNode as IContentNode).Content = readArgs.Value;
            context.NowNode = context.NowNode.Owner;
        }

        private void OnReadNodeStart(ReadContext context, ReadArgs readArgs)
        {
            if (context.Depth == 0 && context.RootNode == null)
            {
                context.RootNode = RootNodeCreateHandle(readArgs);
                m_previousReadNode = context.RootNode;
            }
            else
            {
                if (context.Depth > m_previousReadNode.Depth)
                    (m_previousReadNode as IGroupNode).Add(CreateNormalNode(readArgs));
                else
                {
                    int depthDeviation = m_previousReadNode.Depth - context.Depth;
                    INode parentNode = m_previousReadNode;
                    do
                    {
                        parentNode = parentNode.Owner;
                    }
                    while (depthDeviation-- != 0);
                    (parentNode as IGroupNode).Add(CreateNormalNode(readArgs));
                }
            }
            context.NowNode = m_previousReadNode;
        }

        private INode CreateNormalNode(ReadArgs args)
        {
            if (args.Type == XmlNodeType.Content) return m_previousReadNode = ContentNodeCreateHandle(args);
            else return m_previousReadNode = GroupNodeCreateHandle(args);
        }
    }

}
