using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
* Author: gxw
* Time: 2017/7/7 15:59:11
*
*/
namespace MyLib.Xml.Internal
{
    public enum XmlNodeType
    {
        Group,Content,Chaos
    }

    public interface IXmlNode
    {
        IXmlNode NextSibling
        {
            get;
        }

        IXmlNode PreviousSibling
        {
            get;
        }

        IXmlNode ParentNode
        {
            get;
        }

        Internal.XmlAttributeCollection Attributes
        {
            get;
        }

        XmlNodeType Type
        {
            get;
        }

        string Name
        {
            get;
            set;
        }
    }
}
