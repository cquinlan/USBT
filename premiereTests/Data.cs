using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using USBTCommon;

namespace premiereTests
{
    class Data
    {
        public static string app = "Adobe Premiere Pro";
        public static string path = "C:\\Program Files\\Adobe\\Adobe Premiere Pro CS5.5\\Adobe Premiere Pro.exe";
        public static string hash = Verify.GetMD5HashFromFile(path);
    }
}
