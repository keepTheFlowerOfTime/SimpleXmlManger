using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
* Author: gxw
* Time: 2017/7/9 10:54:26
*
*/
namespace MyLib.Xml.Internal
{
    public class DefaultXmlAttribute:IXmlAttribute
    {
        public DefaultXmlAttribute()
        {

        }

        public string Key
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
