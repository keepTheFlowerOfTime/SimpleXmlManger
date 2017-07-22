using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
* Author: gxw
* Time: 2017/7/20 10:14:22
*
*/
namespace MyLib.Xml.Document
{
    /// <summary>
    /// 文档的节点只能存在两个，分别是Head节点和Data节点，其中Data节点为GroupNode
    /// </summary>
    public interface IDocumentNode
    {
        void Release();

        void Close();

        void Read(string filePath);

        IGroupNode Items
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
