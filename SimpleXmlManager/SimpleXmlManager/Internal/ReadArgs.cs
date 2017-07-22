using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
* Author: gxw
* Time: 2017/7/21 11:02:24
*
*/
namespace MyLib.Xml.Internal
{
    public class ReadArgs : IXmlNode
    {
        public XmlAttributeCollection Attributes
        {
            get;
            set;
        } = new XmlAttributeCollection();

        public string Name
        {
            get;
            set;
        }

        public IXmlNode NextSibling
        {
            get;
            set;
        }

        public IXmlNode ParentNode
        {
            get;
            set;
        }

        public IXmlNode PreviousSibling
        {
            get;
            set;
        }

        public XmlNodeType Type
        {
            get;
            set;
        }

        public string Value
        {
            get;
            set;
        }
    }
}
