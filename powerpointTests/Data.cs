using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using USBTCommon;

namespace powerpointTests
{
    class Data
    {
        public static string app = "Microsoft PowerPoint";
        public static string path = "C:\\Program Files (x86)\\Microsoft Office\\Office12\\POWERPNT.EXE";
        public static string hash = Verify.GetMD5HashFromFile(path);
    }
}
