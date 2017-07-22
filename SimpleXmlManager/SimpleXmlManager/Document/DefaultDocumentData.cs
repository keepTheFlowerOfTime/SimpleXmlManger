using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
* Author: gxw
* Time: 2017/7/13 9:01:00
*
*/
namespace MyLib.Xml.Document
{
    public class DefaultDocumentData:BaseDocumentData
    {
        public DefaultDocumentData(INodeFactory nodeFactory):base(nodeFactory)
        {

        }

        public DefaultDocumentData():this(new DefaultNodeFactory()) { }
    }
}
