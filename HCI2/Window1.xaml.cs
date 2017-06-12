using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HCI2.Model;
using System.Collections.ObjectModel;

namespace HCI2
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private Classroom currentClassroom;
        private AppConfig appConfig;
        private Subject subject;
        private int startTime = 0;
        private int endTime = 0;
        private ScheduleItem newScheduleItem;
        public ObservableCollection<ScheduleItem> Items;
        public int day;

        public Window1(Classroom currentClassroom, AppConfig appConfig,
            Subject subject, ObservableCollection<ScheduleItem> Items, int day)
        {
            InitializeComponent();
            this.currentClassroom = currentClassroom;
            this.appConfig = appConfig;
            this.subject = subject;
            this.Items = Items;
            this.day = day;
            initComboBox();
        }

        public void initComboBox()
        {
            int start = 420;
            int end = start + this.subject.Duration;
            while (end <= 1320)
            {
                bool flag = true;
                Schedule sh = currentClassroom.Schedule;
                foreach (ScheduleItem si in sh.Items)
                {
                    if (si.StartDate <= start && si.EndDate >= start)
                    {
                        flag = false;
                        break;
                    }
                    if (si.StartDate <= end && si.EndDate >= end)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag == true)
                {
                    int hours = start / 60;
                    int minutes = start - hours * 60;
                    comboBox.Items.Add(hours + ":" + minutes);
                }
                start = start + 60;
                end = start + this.subject.Duration;
            }

        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string[] text = comboBox.SelectedItem.ToString().Split(':');
            int time = Int32.Parse(text[0]) * 60 + Int32.Parse(text[1]);
            int hours = (time + this.subject.Duration) / 60;
            int minutes = (time + this.subject.Duration) - hours * 60;
            label2.Content = hours + ":" + minutes;
            this.startTime = time;
            this.endTime = time + this.subject.Duration;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ScheduleItem si = this.appConfig.addScheduleItem(this.currentClassroom.Id, this.subject,
                 this.startTime, this.endTime, this.day);
            Items.Add(si);
            this.subject.CurrentNumber--;
            foreach(Subject s in this.appConfig.Subjects)
            {
                if(s.Id.Equals(this.subject.Id))
                {
                    s.CurrentNumber = this.subject.CurrentNumber;
                }
            }
            this.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

