using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

/*
* Author: gxw
* Time: 2017/7/12 11:18:51
*
*/
namespace MyLib.Xml.Document
{
    /// <summary>
    /// 一个满足某种格式，且符合xml文件语法规则的文件
    /// </summary>
    public class Document
    {
        #region 静态构造方法
        public static Document NewDocument(BaseDocumentHead head, BaseDocumentData data)
        {
            Document doc = new Document(head, data);
            doc.Read(null);
            return doc;
        }

        public static Document NewDocument()
        {
            Document doc = new Document();
            doc.Read(null);
            return doc;
        }

        public static Document ReadHeadFrom(string filePath)
        {
            Document doc = new Document();
            if (File.Exists(filePath)) doc.ReadHead(filePath);
            else doc.Read(null);
            return doc;
        }

        public static Document ReadHeadFrom(BaseDocumentHead head, BaseDocumentData data, string filePath)
        {
            Document doc = new Document(head, data);
            if (File.Exists(filePath)) doc.ReadHead(filePath);
            else doc.Read(null);
            return doc;
        }

        public static Document ReadAllFrom(BaseDocumentHead head, BaseDocumentData data, string filePath)
        {
            Document doc = new Document(head, data);
            if (File.Exists(filePath)) doc.Read(filePath);
            else doc.Read(null);
            return doc;
        }

        public static Document ReadAllFrom(string filePath)
        {
            Document doc = new Document();
            if (File.Exists(filePath))
                doc.Read(filePath);
            else doc.Read(null);
            return doc;
        }
        #endregion

        public const string DEFAULT_ROOT_NODE_NAME = "Root";

        BaseDocumentData m_data;
        BaseDocumentHead m_head;

        protected Document(BaseDocumentHead head, BaseDocumentData data)
        {
            m_head = head;
            m_data = data;
        }

        protected Document() : this(new DefaultDocumentHead()
            , new DefaultDocumentData()
            )
        {

        }

        /// <summary>
        /// 从该路径中读取符合一定格式的xml文件，如对节点名存在要求
        /// </summary>
        /// <param name="filePath"></param>
        public void Read(string filePath)
        {
            ReadHead(filePath);
            ReadData(filePath);
        }

        /// <summary>
        /// 从该路径中读取符合一定格式的xml文件的Data节点
        /// </summary>
        /// <param name="filePath"></param>
        public void ReadData(string filePath)
        {
            Data.Read(filePath);
        }

        /// <summary>
        /// 从该路径中读取符合一定格式的xml文件的Head的节点
        /// </summary>
        /// <param name="filePath"></param>
        public void ReadHead(string filePath)
        {
            Head.Read(filePath);
        }

        /// <summary>
        /// 解除Data节点对内存的占用，不会情况其属性
        /// </summary>
        public void ReleaseData()
        {
            Data.Release();
        }

        /// <summary>
        /// 将Document的信息写到指定的路径，根节点采用默认名
        /// </summary>
        /// <param name="filePath"></param>
        public void Write(string filePath)
        {
            Write(filePath, DEFAULT_ROOT_NODE_NAME);
        }

        /// <summary>
        /// 将Document的信息写到指定的路径，根节点采用指定名
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="rootNodeName"></param>
        public void Write(string filePath, string rootNodeName)
        {
            IGroupNode rootNode = new DefaultGroupNode();
            rootNode.Name = rootNodeName;
            rootNode.Add(Head.Items);
            rootNode.Add(Data.Items);
            SimpleXmlManager.Default.Write(rootNode, filePath);
        }

        /// <summary>
        /// Document的Data节点
        /// </summary>
        public BaseDocumentData Data
        {
            get
            {
                return m_data;
            }
        }

        /// <summary>
        /// Document的Head节点
        /// </summary>
        public BaseDocumentHead Head
        {
            get
            {
                return m_head;
            }
        }
    }
}
