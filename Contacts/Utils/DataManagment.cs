using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Contacts.Utils
{
    static public class DataManagment
    {
        public static T LoadData<T>()
        {
            string filePath = GetDataFilePath();
            if (File.Exists(filePath))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(T));
                TextReader textReader = new StreamReader(filePath);
                T retVal = (T)deserializer.Deserialize(textReader);
                textReader.Close();
                return retVal;
            }
            else
            {
                return default(T);
            }
        }

        public static void SaveData<T>(T t)
        {
            XmlSerializer serializer = new XmlSerializer(t.GetType());
            TextWriter textWriter = new StreamWriter(GetDataFilePath());
            serializer.Serialize(textWriter, t);
            textWriter.Close();
        }

        public static string GetDataFilePath()
        {
            string path = ConfigurationManager.AppSettings["dataFilePath"].ToString();
            return System.Environment.ExpandEnvironmentVariables(path);
        }
      
    }
}
