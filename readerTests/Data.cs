using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using USBTCommon;

namespace readerTests
{
    class Data
    {
        public static string app = "Adobe Reader";
        public static string path = "C:\\Program Files (x86)\\Adobe\\Reader 10.0\\Reader\\AcroRd32.exe";
        public static string hash = Verify.GetMD5HashFromFile(path);
    }
}
