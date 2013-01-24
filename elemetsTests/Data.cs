using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using USBTCommon;

namespace elementsTests
{
    class Data
    {
        public static string app = "Adobe Photoshop Elements";
        public static string path = "C:\\Program Files (x86)\\Adobe\\Photoshop Elements 6.0\\WINWORD.EXE\\PhotoshopElementsOrganizer.exe";
        public static string hash = Verify.GetMD5HashFromFile(path);
    }
}
