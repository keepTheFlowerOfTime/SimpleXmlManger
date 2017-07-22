using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

/*
* Author: gxw
* Time: 2017/7/10 8:49:02
*
*/
namespace MyLib.Xml.Internal
{
    public class XmlWriter
    {
        public XmlWriter()
        {

        }

        public void Write(IGroupNode rootNode, string filePath)
        {
            XmlDocument document = new XmlDocument();
            AddDeclarationToXmlDocument(document);
            document.AppendChild(rootNode.ToXmlNodeWithChildNode(document));
            document.Save(filePath);
        }
        private void AddDeclarationToXmlDocument(XmlDocument document)
        {
            XmlNode declaration = document.CreateXmlDeclaration("1.0", "utf-8", "no");
            document.AppendChild(declaration);
        }
    }
}
