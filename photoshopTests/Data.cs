using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using USBTCommon;

namespace photoshopTests
{
    class Data
    {
        public static string app = "Adobe Photoshop CS5.1";
        public static string path = "C:\\Program Files\\Adobe\\Adobe Photoshop CS5.1 (64 Bit)\\Photoshop.exe";
        public static string hash = Verify.GetMD5HashFromFile(path);
    }
}
