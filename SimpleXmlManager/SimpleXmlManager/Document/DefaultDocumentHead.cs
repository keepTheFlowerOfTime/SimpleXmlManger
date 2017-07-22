using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
* Author: gxw
* Time: 2017/7/13 9:15:49
*
*/
namespace MyLib.Xml.Document
{
    public class DefaultDocumentHead:BaseDocumentHead
    {
        public DefaultDocumentHead(INodeFactory factory):base(factory)
        {

        }

        public DefaultDocumentHead():this(new DefaultNodeFactory()) { }
    }
}
