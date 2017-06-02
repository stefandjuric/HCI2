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
    public class Subject
    {
        [DataMember]
        private string id;

        [DataMember]
        private string name;

        [DataMember]
        private Course course;

        [DataMember]
        private int numberOfMembers;

        [DataMember]
        private string description;

        [DataMember]
        private int duration;

        [DataMember]
        private int number;

        [DataMember]
        private int currentNumber;

        [DataMember]
        private bool projector;

        [DataMember]
        private bool board;

        [DataMember]
        private bool smartBoard;

        [DataMember]
        private int operatingSystem;

        [DataMember]
        private List<SoftwareItem> softwareList;

        public Subject() { }

        public Subject(string name, Course course, int numberOfMembers, string description,
            int duration, int number, int currentNumber, bool projector, bool board, bool smartBoard, int operatingSystem, List<SoftwareItem> items)
        {
            this.id = DateTime.Now.ToString("MMddyyHmmss");
            this.name = name;
            this.course = course;
            this.numberOfMembers = numberOfMembers;
            this.description = description;
            this.duration = duration;
            this.number = number;
            this.currentNumber = currentNumber;
            this.projector = projector;
            this.board = board;
            this.smartBoard = smartBoard;
            this.operatingSystem = operatingSystem;
            this.softwareList = items;
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

        public Course Course
        {
            get { return course; }
            set { course = value; }
        }

        public int NumberOfMembers
        {
            get { return numberOfMembers; }
            set { numberOfMembers = value; }
        }

        public string Descriptions
        {
            get { return description; }
            set { description = value; }
        }

        public int Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        public int CurrentNumber
        {
            get { return currentNumber; }
            set { currentNumber = value; }
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

        public bool SmartBoard
        {
            get { return smartBoard; }
            set { smartBoard = value; }
        }

        public int OperatingSystem
        {
            get { return operatingSystem; }
            set { operatingSystem = value; }
        }

        public List<SoftwareItem> SoftwareList
        {
            get { return softwareList; }
            set { softwareList = value; }
        }

    }
}
