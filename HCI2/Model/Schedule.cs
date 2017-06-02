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
    public class Schedule
    {
        [DataMember]
        private List<ScheduleItem> items;

        public Schedule()
        {
            this.items = new List<ScheduleItem>();
        }

        public Schedule(List<ScheduleItem> items)
        {
            this.items = items;
        }

        public List<ScheduleItem> Items
        {
            get { return items; }
            set { items = value; }
        }

        public void addSchedule(ScheduleItem item)
        {
            items.Add(item);
        }
    }
}
