using MyLib.Xml.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
* Author: gxw
* Time: 2017/7/13 9:03:31
*
*/
namespace MyLib.Xml.Document
{
    public abstract class BaseDocumentNodeFactory:INodeFactory
    {
        BaseDocumentHead m_head;

        public BaseDocumentNodeFactory(BaseDocumentHead head)
        {
            m_head = head;
        }

        protected abstract IContentNode CreateContentNodeFromDataProcess(ReadArgs args, BaseDocumentHead head);

        protected abstract IGroupNode CreateGroupNodeFromDataProcess(ReadArgs args, BaseDocumentHead head);

        protected abstract INode CreateRootNodeFromDataProcess(ReadArgs args, BaseDocumentHead head);

        public IContentNode CreateContentNodeFromData(ReadArgs args)
        {
            return CreateContentNodeFromDataProcess(args, m_head);
        }

        public IGroupNode CreateGroupNodeFromData(ReadArgs args)
        {
            return CreateGroupNodeFromDataProcess(args, m_head);
        }

        public INode CreateRootNodeFromData(ReadArgs args)
        {
            return CreateRootNodeFromDataProcess(args, m_head);
        }
    }
}
