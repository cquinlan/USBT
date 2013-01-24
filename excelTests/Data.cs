using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using USBTCommon;

namespace excelTests
{
    class Data
    {
        public static string app = "Microsoft Excel";
        public static string path = "C:\\Program Files (x86)\\Microsoft Office\\Office12\\EXCEL.EXE";
        public static string hash = Verify.GetMD5HashFromFile(path);
    }
}
