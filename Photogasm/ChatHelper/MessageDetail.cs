using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Photogasm.ChatHelper
{
    public class MessageDetail
    {
        public string FromUserID { get; set; }
        public string FromUserName { get; set; }
        public string ToUserID { get; set; }
        public string ToUserName { get; set; }
        public string Message { get; set; }
    }
}