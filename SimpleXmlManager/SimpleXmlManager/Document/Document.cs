using System;
using System.Collections.Generic;
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
        public static Document NewDocument(BaseDocumentHead head,BaseDocumentData data)
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
            doc.ReadHead(filePath);
            return doc;
        }

        public static Document ReadHeadFrom(BaseDocumentHead head,BaseDocumentData data,string filePath)
        {
            Document doc = new Document(head, data);
            doc.ReadHead(filePath);
            return doc;
        }

        public static Document ReadAllFrom(BaseDocumentHead head,BaseDocumentData data,string filePath)
        {
            Document doc = new Document(head, data);
            doc.Read(filePath);
            return doc;
        }

        public static Document ReadAllFrom(string filePath)
        {
            Document doc = new Document();
            doc.Read(filePath);
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

        protected Document():this(new DefaultDocumentHead()
            ,new DefaultDocumentData()
            )
        {

        }

        public void Read(string filePath)
        {
            ReadHead(filePath);
            ReadData(filePath);
        }

        public void ReadData(string filePath)
        {
            Data.Read(filePath);
        }

        public void ReadHead(string filePath)
        {
            Head.Read(filePath);
        }

        public void ReleaseData()
        {
            Data.Release();
        }

        public void Write(string filePath)
        {
            Write(filePath, DEFAULT_ROOT_NODE_NAME);
        }

        public void Write(string filePath,string rootNodeName)
        {
            IGroupNode rootNode = new DefaultGroupNode();
            rootNode.Name = rootNodeName;
            rootNode.Add(Head.Items);
            rootNode.Add(Data.Items);
            SimpleXmlManager.Default.Write(rootNode, filePath);
        }

        public BaseDocumentData Data
        {
            get
            {
                return m_data;
            }
        }

        public BaseDocumentHead Head
        {
            get
            {
                return m_head;
            }
        }
    }
}
