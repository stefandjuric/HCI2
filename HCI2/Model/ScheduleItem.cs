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
    public class ScheduleItem
    {
        [DataMember]
        private int startDate;
        [DataMember]
        private int endDate;
        [DataMember]
        private Subject subject;
        [DataMember]
        private int hoursStart;
        [DataMember]
        private int minutesStart;
        [DataMember]
        private int hoursEnd;
        [DataMember]
        private int minutesEnd;
        [DataMember]
        private int day;


        public ScheduleItem() { }

        public ScheduleItem(int startDate, int endDate, Subject subject, int day)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            this.subject = subject;
            this.hoursStart = startDate / 60;
            this.minutesStart = startDate - this.hoursStart * 60;
            this.hoursEnd = endDate / 60;
            this.minutesEnd = endDate - this.hoursEnd * 60;
            this.day = day;
        }

        public int StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public int EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public Subject Subject
        {
            get { return subject; }
            set { subject = value; }
        }

        public int HoursStart
        {
            get { return hoursStart; }
            set { hoursStart = value; }
        }

        public int HoursEnd
        {
            get { return hoursEnd; }
            set { hoursEnd = value; }
        }

        public int MinutesStart
        {
            get { return minutesStart; }
            set { minutesStart = value; }
        }

        public int MinutesEnd
        {
            get { return minutesEnd; }
            set { minutesEnd = value; }
        }

        public int Day
        {
            get { return day; }
            set { day = value; }
        }
    }
}
