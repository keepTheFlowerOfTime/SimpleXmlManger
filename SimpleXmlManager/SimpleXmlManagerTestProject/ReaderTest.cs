using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using MyLib.Xml;
using MyLib.Xml.Document;

namespace SimpleXmlManagerTestProject
{
    [TestClass]
    public class ReaderTest
    {
        [TestMethod]
        public void TestReadNode()
        {

            SimpleXmlManager manager = new SimpleXmlManager();
            var node = manager.Read("locationReflect.xml", "Data");
            Console.WriteLine(node.Name);
            foreach (var pair in node.Attributes) Console.WriteLine(string.Format("{0}:{1}", pair.Key, pair.Value));
            if (node.IsGroupNode)
            {
                var groupNode = (node as IGroupNode);
                foreach (var subNode in groupNode.ChildNodes) ShowAttri(subNode);
            }
        }

        [TestMethod]
        public void TestReadFile()
        {
            SimpleXmlManager manager = new SimpleXmlManager();
            var node = manager.Read("locationReflect.xml");
            Console.WriteLine(node.Name);
            foreach (var pair in node.Attributes) Console.WriteLine(string.Format("{0}:{1}", pair.Key, pair.Value));
            if (node.IsGroupNode)
            {
                var groupNode = (node as IGroupNode);
                foreach (var subNode in groupNode.ChildNodes) ShowAttri(subNode);
            }
        }

        [TestMethod]
        public void TestCreateAndReadFile()
        {
            string filePath = "CreateAndReadFile.xml";
            string checkPath = "CreateAndReadFile_Test.xml";
            Document document = Document.NewDocument();
            ContentNode contentNode = new ContentNode();
            DefaultGroupNode groupNode = new DefaultGroupNode();
            contentNode.Attributes.Add("attri0", "value");
            for (int i = 0; i < 3; ++i)
            {
                document.Head.Items.Add(new ContentNode()
                {
                    Content = string.Format("Content-{0}", i),
                    Name = string.Format("HeadNode{0}", i),
                });
            }
            document.Head.Items.Add(contentNode);

            for (int i = 0; i < 3; ++i)
            {
                document.Data.Items.Add(new ContentNode()
                {
                    Content = string.Format("Data-{0}", i),
                    Name = string.Format("GroupNode{0}", i),
                });
            }
            document.Data.Items.Add(contentNode);
            document.Data.Items.Add(groupNode);

            document.Write(filePath);

            Document readDocument = Document.ReadAllFrom(filePath);
            readDocument.Write(checkPath);

            string fileText = File.ReadAllText(filePath);
            string outputText = File.ReadAllText(checkPath);
            Assert.AreEqual(fileText, outputText);
            Assert.IsTrue(readDocument.Data.Items.LastChild is IGroupNode);
            Assert.IsTrue(readDocument.Head.Items.LastChild is IContentNode);
        }

        [TestMethod]
        public void TestAddOrDelete()
        {
            string inputPath = "CreateAndReadFile.xml";
            string outputPath = "AddOrDeleteFile.xml";
            Document doc = Document.ReadAllFrom(inputPath);
            IGroupNode groupNode = doc.Data.Items.LastChild as IGroupNode;
            groupNode.Add(new ContentNode()
            {
                Content = "测试值",
            });
            doc.Write(outputPath);
        }

        private void ShowAttri(INode node)
        {
            Console.WriteLine(node.Name);
            foreach (var pair in node.Attributes) Console.WriteLine(string.Format("{0}:{1}", pair.Key, pair.Value));
        }
    }
}
