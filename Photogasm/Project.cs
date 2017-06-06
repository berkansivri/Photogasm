using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Photogasm
{
    public class Project
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string UserID { get; set; }
        public int PhotoCount { get; set; }
        public string SamplePhoto { get; set; }
    }
}