using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Photogasm
{
    public class PhotoInfo
    {
        public string UID { get; set; }
        public string userFName { get; set; }
        public string userSName { get; set; }
        public string PID { get; set; }
        public string imgUrl { get; set; }
        public string userImg { get; set; }
        public string rate { get; set; } = "0";
        public int tLike { get; set; } = 0;
        public string publish { get; set; }
        public string projectname { get; set; }
        public string Disc { get; set; }
        public string Url { get; set; }
        public string Liked { get; set; } = "/Images/unlike.png";
        public string imgStar { get; set; }
        public int tComment { get; set; }
        public List<Comment> CommentList { get; set; }
    }
}