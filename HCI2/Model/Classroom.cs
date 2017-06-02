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
    public class Classroom
    {
        [DataMember]
        private string id;

        [DataMember]
        private string description;

        [DataMember]
        private int numberOfWorkplace;

        [DataMember]
        private bool projector;

        [DataMember]
        private bool board;

        [DataMember]
        private bool smartBoart;

        [DataMember]
        private bool windows;

        [DataMember]
        private bool linux;

        [DataMember]
        private List<SoftwareItem> softwareList;

        [DataMember]
        private Schedule schedule;

        public Classroom() { }

        public Classroom(string description, int number, bool projector,
            bool board, bool smartBoard, bool windows, bool linux, Schedule schedule)
        {
            //this.id = DateTime.Now.ToString("MM/dd/yy H:mm:ss zzz}");
            this.id = DateTime.Now.ToString("MMddyyHmmss");
            this.description = description;
            this.numberOfWorkplace = number;
            this.projector = projector;
            this.board = board;
            this.smartBoart = smartBoard;
            this.windows = windows;
            this.linux = linux;
            this.schedule = schedule;
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public int NumberOfWorkplace
        {
            get { return numberOfWorkplace; }
            set { numberOfWorkplace = value; }
        }

        public bool Projector
        {
            get { return projector; }
            set { projector = value; }
        }

        public bool Board
        {
            get { return board; }
            set { board = value; }
        }

        public bool SmartBoart
        {
            get { return smartBoart; }
            set { smartBoart = value; }
        }

        public bool Windows
        {
            get { return windows; }
            set { windows = value; }
        }

        public bool Linux
        {
            get { return linux; }
            set { linux = value; }
        }

        public Schedule Schedule
        {
            get { return schedule; }
            set { schedule = value; }
        }

    }
}
