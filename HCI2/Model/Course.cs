using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace HCI2.Model
{
    [Serializable]
    [DataContract]
    public class Course
    {
        [DataMember]
        private string id;

        [DataMember]
        private string name;

        [DataMember]
        private DateTime dateFoundation;

        [DataMember]
        private string description;

        public Course() { }

        public Course(string name, DateTime date, string description)
        {
            this.id = DateTime.Now.ToString("MMddyyHmmss");
            this.name = name;
            this.dateFoundation = date;
            this.description = description;
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public DateTime DateFoundation
        {
            get { return dateFoundation; }
            set { dateFoundation = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

    }
}
