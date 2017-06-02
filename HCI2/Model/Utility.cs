using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace HCI2.Model
{
    public class Utility
    {
        [STAThread]
        public static void Serialize(AppConfig appConfig)
        {
            DataContractSerializer ser = new DataContractSerializer(typeof(AppConfig));
            var fStream = new FileStream("AppConfig.xml", FileMode.Create);
            ser.WriteObject(fStream, appConfig);
            fStream.Close();
        }


        public static Boolean Deserialize(AppConfig appConfig)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(AppConfig));

            String fileName = "AppConfig.xml";

            if (!File.Exists(fileName)) return false;

            FileStream fs = new FileStream(fileName, FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);

            AppConfig temp = new AppConfig();

            try
            {
                temp = (AppConfig)serializer.ReadObject(reader, true);
                appConfig.Classrooms = temp.Classrooms;
                appConfig.Curses = temp.Curses;
                appConfig.SoftwareItems = temp.SoftwareItems;
                appConfig.Subjects = temp.Subjects;
            }
            catch (Exception)
            {
                return false;
            }

            fs.Close();
            return true;

        }
    }
}
