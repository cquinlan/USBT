using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using USBTCommon;

namespace wordTests
{
    class Data
    {
        public static string app = "Microsoft Word";
        public static string path = "C:\\Program Files (x86)\\Microsoft Office\\Office12\\WINWORD.EXE";
        public static string hash = Verify.GetMD5HashFromFile(path);
    }
}
