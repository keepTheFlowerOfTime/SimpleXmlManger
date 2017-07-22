using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
* Author: gxw
* Time: 2017/7/7 16:03:16
*
*/
namespace MyLib.Xml.Internal
{
    public interface IXmlAttribute
    {
        string Key
        {
            get;
            set;
        }

        string Value
        {
            get;
            set;
        }
    }
}
