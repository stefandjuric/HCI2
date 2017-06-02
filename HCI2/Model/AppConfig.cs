using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;

namespace HCI2.Model
{
    [Serializable]
    [DataContract]
    public class AppConfig
    {
        [DataMember]
        private List<Classroom> classrooms;

        [DataMember]
        private List<Course> curses;

        [DataMember]
        private List<SoftwareItem> softwareItems;

        [DataMember]
        private List<Subject> subjects;

        public AppConfig()
        {
            this.classrooms = new List<Classroom>();
            this.curses = new List<Course>();
            this.softwareItems = new List<SoftwareItem>();
            this.subjects = new List<Subject>();
        }

        public List<Classroom> Classrooms
        {
            get { return classrooms; }
            set { classrooms = value; }
        }

        public List<Course> Curses
        {
            get { return curses; }
            set { curses = value; }
        }

        public List<SoftwareItem> SoftwareItems
        {
            get { return softwareItems; }
            set { softwareItems = value; }
        }

        public List<Subject> Subjects
        {
            get { return subjects; }
            set { subjects = value; }
        }

        public void Serialize()
        {
            if (!File.Exists("AppConfig.xml"))
            {
                FileStream fs = File.Create("AppConfig.xml");
                fs.Close();
                return;
            }

            XmlSerializer ser = new XmlSerializer(typeof(AppConfig));
            TextWriter writer = new StreamWriter("Model.xml");
            ser.Serialize(writer, this);
            writer.Close();
        }

        public void Deserialize()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AppConfig));

            if (!File.Exists("AppConfig.xml")) return;

            FileStream fs = new FileStream("AppConfig.xml", FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);

            AppConfig temp = new AppConfig();
            temp = (AppConfig)serializer.Deserialize(reader);

            this.classrooms = temp.classrooms;
            this.curses = temp.curses;
            this.softwareItems = temp.softwareItems;
            this.subjects = temp.subjects;
            fs.Close();
        }

        public ScheduleItem addScheduleItem(string classroomId, Subject subject, int start, int end, int day)
        {
            Classroom classroom = getClassroom(classroomId);
            ScheduleItem si = new ScheduleItem(start, end, subject, day);
            classroom.Schedule.addSchedule(si);
            return si;
        }

        private Classroom getClassroom(string id)
        {
            Classroom cr = null;
            foreach (Classroom c in this.Classrooms)
            {
                if (c.Id.Equals(id))
                {
                    cr = c;
                    break;
                }
            }
            return cr;
        }


    }
}
