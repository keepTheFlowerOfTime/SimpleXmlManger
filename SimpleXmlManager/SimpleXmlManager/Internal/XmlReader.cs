using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
* Author: gxw
* Time: 2017/7/7 16:56:28
*
*/
namespace MyLib.Xml.Internal
{
    public class XmlReader
    {
        /// <summary>
        /// 读取某个节点时,当字节点被读取完成时，触发该事件，默认为空
        /// </summary>
        public event Action<ReadContext, ReadArgs> ReadSingleNodeEnd;

        /// <summary>
        /// 读取某个节点时，当读取到能够判定节点类型的时候，触发该事件，默认为空
        /// </summary>
        public event Action<ReadContext, ReadArgs> ReadSingleNodeStart;

        public XmlReader()
        {

        }

        /// <summary>
        /// 读取一个xml文件的节点，读取期间会触发如ReadSingleNodeEnd等之类的事件
        /// 若未绑定动作到事件上，则Read函数本身无意义
        /// </summary>
        /// <param name="filePath"></param>
        public ReadContext Read(string filePath)
        {
            return ReadNodeProcess(filePath, null);
        }

        /// <summary>
        /// 读取一个xml文件，第一个匹配名字为nodeName的节点，读取期间会触发ReadSingleNodeEnd等之类的事件，
        /// 若未绑定动作到事件上，则Read函数本身无意义
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public ReadContext ReadNode(string filePath, string nodeName)
        {
            return ReadNodeProcess(filePath, nodeName);
        }


        protected ReadContext ReadNodeProcess(string filePath, string nodeName)
        {
            FileStream stream = new FileStream(filePath, FileMode.Open);
            System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(stream);
            ReadArgs readArgs = null;
            ReadContext context = new ReadContext();
            bool readIsFinish = false;
            if (nodeName == null) reader.Read();
            else reader.ReadToFollowing(nodeName);
            do
            {
                int nowDepth = reader.Depth;
                switch (reader.NodeType)
                {
                    case System.Xml.XmlNodeType.Element:
                        ConsoleLog.WriteLine(System.Xml.XmlNodeType.Element + "-" + reader.Name);
                        //该条件成立时，判定读取节点为GroupNode
                        if (context.PreviousNodeType == System.Xml.XmlNodeType.Element)
                        {
                            readArgs.Type = XmlNodeType.Group;
                            ReadSingleNodeStart?.Invoke(context, readArgs);
                            if (reader.Depth == context.PreviousDepth && readArgs.Name == nodeName)
                            {
                                readIsFinish = true;
                                context.PopParentNode();
                                break;
                            }

                        }

                        readArgs = new ReadArgs();
                        context.NodeIsEnd = ReadElement(reader, readArgs, context);
                        context.PreviousNodeType = System.Xml.XmlNodeType.Element;
                        context.PreviousDepth = nowDepth;
                        break;
                    case System.Xml.XmlNodeType.Text:
                        ConsoleLog.WriteLine(System.Xml.XmlNodeType.Text + "-" + reader.Name);
                        //判定当前节点为ContentNode
                        readArgs.Type = XmlNodeType.Content;
                        ReadSingleNodeStart?.Invoke(context, readArgs);
                        context.NodeIsEnd = ReadText(reader, readArgs, context);
                        context.PreviousNodeType = System.Xml.XmlNodeType.Text;
                        context.PreviousDepth = nowDepth;
                        break;
                    case System.Xml.XmlNodeType.EndElement:
                        ConsoleLog.WriteLine(System.Xml.XmlNodeType.EndElement + "-" + reader.Name);
                        //当条件成立时，判定节点为GroupNode
                        if (context.PreviousNodeType == System.Xml.XmlNodeType.Element)
                        {
                            readArgs.Type = XmlNodeType.Group;
                            ReadSingleNodeStart?.Invoke(context, readArgs);
                        }

                        context.NodeIsEnd = ReadEndElement(reader, readArgs, context);
                        ReadSingleNodeEnd?.Invoke(context, readArgs);
                        if (reader.Name == nodeName) readIsFinish = true;
                        readArgs = new ReadArgs();
                        context.PreviousNodeType = System.Xml.XmlNodeType.EndElement;
                        context.PreviousDepth = nowDepth;
                        break;
                }
                if (readIsFinish) break;
            }
            while (reader.Read());
            reader.Close();
            return context;
        }


        /// <summary>
        /// 读取xml文件Element节点，返回值恒为false
        /// </summary>
        /// <param name="reader">代表节点读取是否完成</param>
        /// <param name="readArgs">代表读取到的一些参数</param>
        /// <param name="context">代表读取过程中的上下文环境，例如包括父节点</param>
        /// <returns></returns>
        protected bool ReadElement(System.Xml.XmlTextReader reader, ReadArgs readArgs, ReadContext context)
        {
            readArgs.Name = reader.Name;
            readArgs.Attributes = new XmlAttributeCollection();
            for (int i = 0; i < reader.AttributeCount; ++i)
            {
                reader.MoveToNextAttribute();
                readArgs.Attributes.Add(new DefaultXmlAttribute()
                {
                    Key = reader.Name,
                    Value = reader.Value,
                });
            }
            readArgs.ParentNode = context.PeekParentNode();
            context.PushParentNode(readArgs);
            return false;
        }

        /// <summary>
        /// 读取xml文件text节点,返回值恒为false
        /// </summary>
        /// <param name="reader">代表节点读取是否完成</param>
        /// <param name="readArgs">代表读取到的一些参数</param>
        /// <param name="context">代表读取过程中的上下文环境，例如包括父节点</param>
        /// <returns></returns>
        protected bool ReadText(System.Xml.XmlTextReader reader, ReadArgs readArgs, ReadContext context)
        {
            readArgs.Value = reader.Value;
            readArgs.Type = XmlNodeType.Content;
            return false;
        }

        /// <summary>
        /// 读取节点结束标记,返回值恒为false
        /// </summary>
        /// <param name="reader">代表节点读取是否完成</param>
        /// <param name="readArgs">代表读取到的一些参数</param>
        /// <param name="context">代表读取过程中的上下文环境，例如包括父节点</param>
        /// <returns></returns>
        protected bool ReadEndElement(System.Xml.XmlTextReader reader, ReadArgs readArgs, ReadContext context)
        {
            context.PopParentNode();
            return true;
        }
    }




}
