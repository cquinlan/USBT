using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using USBTCommon;

namespace itunesTests
{
    class Data
    {
        public static string app = "Apple iTunes";
        public static string path = "C:\\Program Files (x86)\\iTunes\\iTunes.exe";
        public static string hash = Verify.GetMD5HashFromFile(path);
    }
}
