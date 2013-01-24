using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using USBTCommon;

namespace chromeTests
{
    class Data
    {
        public static string app = "Google Chrome";
        public static string path = "C:\\Users\\cpquinlx\\AppData\\Local\\Google\\Chrome\\Application\\chrome.exe";
        public static string hash = Verify.GetMD5HashFromFile(path);
    }
}
