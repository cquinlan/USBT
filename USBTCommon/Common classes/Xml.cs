using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace USBTCommon
{
    public static class Xml
    {
        public static XmlDocument fetchXmlObject(string path)
        {
            XmlDocument doc = new XmlDocument();
            if (File.Exists(path))
            {
                doc.Load(path);
                doc.PreserveWhitespace = false;
                return doc;
            }
            else return null;
        }

        public static IOperation[] loadOpsFile(string path, Type[] knownTypes)
        {
            //Open the operation file.
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);

            //Read in the file and store each item to a list. Type is "anyType" but this will be cast to IOperation.
            DataContractSerializer serializer = new DataContractSerializer(typeof(IOperation), knownTypes);
            XmlReader read = XmlReader.Create(fs);
            read.ReadToDescendant("z:anyType");

            List<IOperation> opList = new List<IOperation>();

            //Blurahegle
            while (serializer.IsStartObject(read))
            {
                //Check each type when deserializing. Make sure we can cast it.
                try
                {
                    opList.Add((IOperation)serializer.ReadObject(read));
                }
                catch (Exception e)
                {
                    MessageBox.Show("Invalid operation type encountered. Please ensure all required libraies are installed \n" + e.Message, "An error has occured",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    fs.Close();
                    return null;
                }
            }
            fs.Close();
            return opList.ToArray();
            //Done
        }

        public static void saveOpsFile(string path, IOperation[] opQueue, Type[] knowTypes)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(IOperation),knowTypes);
            XmlWriterSettings set = new XmlWriterSettings();
            set.NewLineHandling = NewLineHandling.Entitize; //Don't mess with newline chars.
            set.Indent = true; //Make it human readable.

            using (XmlWriter writer = XmlWriter.Create(path, set))
            {
                //Because we are reading this to an array later, we need to add a root node with the proper title and namespace.
                writer.WriteStartElement("Operations","http://schemas.microsoft.com/2003/10/Serialization/Arrays");
                foreach (IOperation p in opQueue)
                {
                    serializer.WriteObject(writer, ((IOperation)p));
                    writer.Flush();
                }
                writer.WriteEndElement();
                writer.Close();
            }
        }

        public static bool generateReport(Dictionary<string, Dictionary<string, Stopwatch>> timeDict)
        {
            for (int i = 0; i < timeDict.Count; i++)
            {
                Dictionary<string, Stopwatch> child = timeDict.Values.ElementAt(i);
                Console.WriteLine(timeDict.Keys.ElementAt(i));
                for (int x = 0; x < child.Count; x++)
                {
                    Stopwatch timer = child.Values.ElementAt(x);
                    string name = child.Keys.ElementAt(x);
                    double convert = Convert.ToDouble((timer.ElapsedMilliseconds));
                    Console.WriteLine(name + ": " + convert.ToString());
                }
            }
            return true;
        }
    }
}
