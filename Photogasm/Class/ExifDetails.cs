using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Photogasm
{
    public class ExifDetails
    {
        public string PID { get; set; }
        public string Camera { get; set; }
        public string ISO { get; set; }
        public string Focal_Rate { get; set; }
        public string A_Value { get; set; }
        public string S_Value { get; set; }
        public string P_Date { get; set; }
        public string H_Resolution { get; set; }
        public string V_Resolution { get; set; }
        public string Color_Space { get; set; }
        public string Bits_Per_Pixel { get; set; }
        public string Image_Size { get; set; }
    }

}