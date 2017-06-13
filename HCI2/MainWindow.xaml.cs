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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using HCI2.Model;
using System.ComponentModel;
using HCI2.Validation;

namespace HCI2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        static public ObservableCollection<Classroom> listaUcionica = new ObservableCollection<Classroom>();
        static public ObservableCollection<Course> listaSmerova = new ObservableCollection<Course>();
        static public ObservableCollection<Subject> listaPredmeta = new ObservableCollection<Subject>();
        static public ObservableCollection<SoftwareItem> listaSoftvera = new ObservableCollection<SoftwareItem>();
        static public List<string> osList = new List<string>(new string[] { "Windows", "Linux", "Cross-platform" });
        public List<SoftwareItem> softver = new List<SoftwareItem>();
        public AppConfig appConfig;
        public string selectedClassroom = "";
        public string selectedCourse = "";
        public string selectedSubject = "";
        public string selectedSoftwareItem = "";
        private Classroom classroom=null;
        Point startPoint = new Point();
        private string currentClassroom = "";

        public ObservableCollection<ScheduleItem> Items
        {
            get;
            set;
        }

        public ObservableCollection<Subject> Predmeti
        {
            get;
            set;
        }

        public ObservableCollection<Subject> Predmeti1
        {
            get;
            set;
        }
        public MainWindow()
        {
            appConfig = new AppConfig();
            readAppConfig();
            InitializeComponent();
            this.DataContext = this;
            Predmeti = new ObservableCollection<Subject>();
            Predmeti1 = new ObservableCollection<Subject>();
            Items = new ObservableCollection<ScheduleItem>();
            mainClassroom_CB_init();
            classroomDataGridInit();
            courseDataGridInit();
            subjectDataGridInit();
            softwareDataGridInit();
            classroomOS_CB_init();
            softwareOS_CB_init();
            subjectOS_CB_init();    
        }

        public void readAppConfig()
        {
            if (!Utility.Deserialize(appConfig)) return;
        }

        public void subjectDataGridInit()
        {
            subjectCourse_CB_init();
            subjectSoftware_TB_init();
            listaPredmeta = new ObservableCollection<Subject>(this.appConfig.Subjects);
            Subject_DG.ItemsSource = null;
            Subject_DG.ItemsSource = listaPredmeta;
        }

        public void subjectCourse_CB_init()
        {
            SubjectCourse_CB.Items.Clear();
            foreach (Course elm in this.appConfig.Curses)
            {
                SubjectCourse_CB.Items.Add(elm.Name);
            }
        }

        public void subjectSoftware_TB_init()
        {
            SubjectSoftware_CB.Items.Clear();
            foreach (SoftwareItem elm in this.appConfig.SoftwareItems)
                SubjectSoftware_CB.Items.Add(elm.Name);
        }

        public void classroomDataGridInit()
        {
            listaUcionica = new ObservableCollection<Classroom>(this.appConfig.Classrooms);
            Classroom_DG.ItemsSource = null;
            Classroom_DG.ItemsSource = listaUcionica;
        }

        public void classroomOS_CB_init()
        {
            ClassroomOS_CB.Items.Clear();
            foreach (string os in osList) ClassroomOS_CB.Items.Add(os);
        }

        public void mainClassroom_CB_init()
        {
            MainClassroom_CB.Items.Clear();
            foreach (Classroom cr in appConfig.Classrooms)
            {
                MainClassroom_CB.Items.Add(cr.Description);
            }
        }

        public void courseDataGridInit()
        {
            listaSmerova = new ObservableCollection<Course>(this.appConfig.Curses);
            Course_DG.ItemsSource = null;
            Course_DG.ItemsSource = listaSmerova;
        }

        public void softwareDataGridInit()
        {
            listaSoftvera = new ObservableCollection<SoftwareItem>(this.appConfig.SoftwareItems);
            Software_DG.ItemsSource = null;
            Software_DG.ItemsSource = listaSoftvera;
        }

        public void softwareOS_CB_init()
        {
            SoftwareOS_CB.Items.Clear();
            foreach (string os in osList) SoftwareOS_CB.Items.Add(os);
        }

        public void subjectOS_CB_init()
        {
            SubjectOS_CB.Items.Clear();
            foreach (string os in osList) SubjectOS_CB.Items.Add(os);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        //Classroom

        private string classromDescription;
        private string classroomSeats;
        public string ClassromDescription
        {
            get;
            set;
        }
        public int ClassroomSeats
        {
            get;
            set;
        }

        private bool classromIsValid()
        {
            if (ClassroomDescription_TB.Text.Equals(""))
            {
                MessageBox.Show("Field for description must not be empty!!");
                return false;
            }
            if (ClassroomSeats_TB.Text.Equals(""))
            {
                MessageBox.Show("Field for seats must not be empty!!");
                return false;
            }
            int brojRadnihMjesta;
            if (!int.TryParse(ClassroomSeats_TB.Text, out brojRadnihMjesta))
            {
                MessageBox.Show("Field for seats must be integer!!");
                return false;
            }
            return true;
        }

        private bool classromFilterIsValid()
        {
            int brojRadnihMjesta;
            if (!ClassroomSeats_TB.Text.Equals(""))
            { 
                if (!int.TryParse(ClassroomSeats_TB.Text, out brojRadnihMjesta))
                {
                    MessageBox.Show("Field for seats must be integer!!");
                    return false;
                }
            }
            return true;
        }

        private void ClassroomFilterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!classromFilterIsValid()) return;
            string opis = ClassroomDescription_TB.Text;
            int brojRadnihMjesta = -1;
            if (!ClassroomSeats_TB.Text.Equals(""))
            {
                brojRadnihMjesta = Int32.Parse(ClassroomSeats_TB.Text);
            }
            bool windows = false;
            bool linux = false;
            if (ClassroomOS_CB.SelectedIndex == 0) windows = true;

            if (ClassroomOS_CB.SelectedIndex == 1) linux = true;

            if (ClassroomOS_CB.SelectedIndex == 2)
            {
                windows = true;
                linux = true;
            }
            bool projektor = ClassroomProjector_CB.IsChecked.Value;
            bool tabla = ClassroomBoard_CB.IsChecked.Value;
            bool pametnaTabla = ClassroomSmartBoard_CB.IsChecked.Value;
            listaUcionica = new ObservableCollection<Classroom>();
            foreach(Classroom c in this.appConfig.Classrooms)
            {
                bool flag = true;
                if(!opis.Equals(""))
                {
                    if (!c.Description.Contains(opis)) flag = false;
                }
                if (brojRadnihMjesta != -1)
                {
                    if (c.NumberOfWorkplace < brojRadnihMjesta) flag = false;
                }
                if (c.Windows != windows) flag = false;
                if (c.Linux != linux) flag = false;
                if (c.Projector != projektor) flag = false;
                if (c.Board != tabla) flag = false;
                if (c.SmartBoart != pametnaTabla) flag = false;
                if (flag) listaUcionica.Add(c);
            }
            Classroom_DG.ItemsSource = listaUcionica;
        }

        private void ClassroomClearBtn_Click(object sender, RoutedEventArgs e)
        {
            classroomDataGridInit();
        }

        private void ClassroomAddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!classromIsValid()) return;
            string opis = ClassroomDescription_TB.Text;
            int brojRadnihMjesta= Int32.Parse(ClassroomSeats_TB.Text);
            bool windows = false;
            bool linux = false;
            if (ClassroomOS_CB.SelectedIndex == 0) windows = true;

            if (ClassroomOS_CB.SelectedIndex == 1) linux = true;

            if (ClassroomOS_CB.SelectedIndex == 2)
            {
                windows = true;
                linux = true;
            }
            bool projektor = ClassroomProjector_CB.IsChecked.Value;
            bool tabla = ClassroomBoard_CB.IsChecked.Value;
            bool pametnaTabla = ClassroomSmartBoard_CB.IsChecked.Value;
            Classroom classroom = new Classroom(opis, brojRadnihMjesta, projektor, tabla, pametnaTabla, windows, linux, new Schedule());
            appConfig.Classrooms.Add(classroom);
            listaUcionica.Add(classroom);
            Classroom_DG.ItemsSource = listaUcionica;
            mainClassroom_CB_init();
        }

        private void Classroom_DG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Classroom_DG.SelectedItem != null)
            {
                var row = (Classroom)Classroom_DG.SelectedItem;
                selectedClassroom = row.Id;
                ClassroomDescription_TB.Text = row.Description;
                ClassroomSeats_TB.Text = row.NumberOfWorkplace.ToString();
                if (row.Windows == true && row.Linux == false) ClassroomOS_CB.SelectedIndex = 0;
                if (row.Windows == false && row.Linux == true) ClassroomOS_CB.SelectedIndex = 1;
                if (row.Windows == true && row.Linux == true) ClassroomOS_CB.SelectedIndex = 2;
                ClassroomProjector_CB.IsChecked = row.Projector;
                ClassroomBoard_CB.IsChecked = row.Board;
                ClassroomSmartBoard_CB.IsChecked = row.SmartBoart;
            }
        }

        private void ClassroomUpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedClassroom != "")
            {
                if (!classromIsValid()) return;
                foreach (Classroom elm in appConfig.Classrooms)
                {
                    if (elm.Id.Equals(selectedClassroom))
                    {
                        elm.Description = ClassroomDescription_TB.Text;
                        elm.NumberOfWorkplace = Int32.Parse(ClassroomSeats_TB.Text);
                        elm.Windows = false;
                        elm.Linux = false;
                        if (ClassroomOS_CB.SelectedIndex == 0) elm.Windows = true;

                        if (ClassroomOS_CB.SelectedIndex == 1) elm.Linux = true;

                        if (ClassroomOS_CB.SelectedIndex == 2)
                        {
                            elm.Windows = true;
                            elm.Linux = true;
                        }
                        elm.Projector = ClassroomProjector_CB.IsChecked.Value;
                        elm.Board = ClassroomBoard_CB.IsChecked.Value;
                        elm.SmartBoart = ClassroomSmartBoard_CB.IsChecked.Value;
                        break;
                    }
                }
                listaUcionica.Clear();
                foreach (Classroom elm in this.appConfig.Classrooms)
                    listaUcionica.Add(elm);
                Classroom_DG.ItemsSource = null;
                Classroom_DG.ItemsSource = listaUcionica;
                mainClassroom_CB_init();
            }
            else
            {
                MessageBox.Show("Select classroom for update!!");
                return;
            }
        }

        private void ClassroomRemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedClassroom != "")
            {
                int i = 0;
                foreach (Classroom elm in appConfig.Classrooms)
                {
                    if (elm.Id.Equals(selectedClassroom)) break;
                    i++;
                }
                appConfig.Classrooms.RemoveAt(i);
                listaUcionica.Clear();
                foreach (Classroom elm in this.appConfig.Classrooms)
                    listaUcionica.Add(elm);
                Classroom_DG.ItemsSource = null;
                Classroom_DG.ItemsSource = listaUcionica;
                mainClassroom_CB_init();
            }
        }

        private void ClassroomSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            listaUcionica.Clear();
            foreach (Classroom elm in appConfig.Classrooms)
            {
                if (elm.Description.Contains(ClassroomSearch_TB.Text) || elm.Id.Contains(ClassroomSearch_TB.Text))
                    listaUcionica.Add(elm);
            }
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            Utility.Serialize(appConfig);
        }

        //Curse

        private string curseName;
        private string curseDescription;
        public string CurseName
        {
            get;
            set;
        }
        public string CurseDescription
        {
            get;
            set;
        }

        private bool curseIsValid()
        {
            if (CourseName_TB.Text.Equals(""))
            {
                MessageBox.Show("Field for name must not be empty!!");
                return false;
            }

            if (CourseDescription_TB.Text.Equals(""))
            {
                MessageBox.Show("Field for description must not be empty!!");
                return false;
            }
            return true;
        }

        private void CourseFilterBtn_Click(object sender, RoutedEventArgs e)
        {
            string naziv = CourseName_TB.Text;
            DateTime datumOsnivanja = DateTime.Parse(CourseFoundationDate_DP.Text);
            string opis = CourseDescription_TB.Text;

            listaSmerova = new ObservableCollection<Course>();
            foreach (Course c in this.appConfig.Curses)
            {
                bool flag = true;
                if (!naziv.Equals(""))
                {
                    if (!c.Name.Contains(naziv)) flag = false;
                }
                if (!opis.Equals(""))
                {
                    if (!c.Description.Contains(opis)) flag = false;
                }
                if (datumOsnivanja == c.DateFoundation) flag = false;
                if (flag) listaSmerova.Add(c);
            }
            Course_DG.ItemsSource = listaSmerova;
        }

        private void CourseClearBtn_Click(object sender, RoutedEventArgs e)
        {
            courseDataGridInit();
        }

        private void CourseAddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!curseIsValid()) return;
            string naziv = CourseName_TB.Text;
            DateTime datumOsnivanja = DateTime.Parse(CourseFoundationDate_DP.Text);
            string opis = CourseDescription_TB.Text;
            Course curs = new Course(naziv, datumOsnivanja, opis);
            appConfig.Curses.Add(curs);
            listaSmerova.Add(curs);
            Course_DG.ItemsSource = listaSmerova;
            subjectCourse_CB_init();
        }

        private void Course_DG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Course_DG.SelectedItem != null)
            {
                var row = (Course)Course_DG.SelectedItem;
                selectedCourse = row.Id;
                CourseName_TB.Text = row.Name;
                CourseFoundationDate_DP.Text = row.DateFoundation.ToString();
                CourseDescription_TB.Text = row.Description;
            }
        }

        private void CourseUpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCourse != "")
            {
                if (!curseIsValid()) return;
                foreach (Course elm in appConfig.Curses)
                {
                    if (elm.Id.Equals(selectedCourse))
                    {
                        elm.Name = CourseName_TB.Text;
                        elm.DateFoundation = DateTime.Parse(CourseFoundationDate_DP.Text);
                        elm.Description = CourseDescription_TB.Text;
                        break;
                    }
                }
                listaSmerova.Clear();
                foreach (Course elm in this.appConfig.Curses)
                    listaSmerova.Add(elm);
                Course_DG.ItemsSource = null;
                Course_DG.ItemsSource = listaSmerova;
                subjectCourse_CB_init();
            }
            else
            {
                MessageBox.Show("Select curse for update!!");
                return;
            }
        }

        private void CourseRemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCourse != "")
            {
                int i = 0;
                foreach (Course elm in appConfig.Curses)
                {
                    if (elm.Id.Equals(selectedCourse)) break;
                    i++;
                }
                appConfig.Curses.RemoveAt(i);
                listaSmerova.Clear();
                foreach (Course elm in this.appConfig.Curses)
                    listaSmerova.Add(elm);
                Course_DG.ItemsSource = null;
                Course_DG.ItemsSource = listaSmerova;
                subjectCourse_CB_init();
            }
        }

        private void CourseSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            listaSmerova.Clear();
            foreach (Course elm in appConfig.Curses)
                if (elm.Name.Contains(CourseSearch_TB.Text) || elm.Id.Contains(CourseSearch_TB.Text)
                    ||elm.Description.Contains(CourseSearch_TB.Text))
                    listaSmerova.Add(elm);
        }

        //Subject

        private string subjectName;
        private string subjectDescription;
        private int subjectDuration;
        private int subjectTerms;
        private int subjectGroupSize;

        public string SubjectName
        {
            get;
            set;
        }

        public string SubjectDescription
        {
            get;
            set;
        }

        public int SubjectDuration
        {
            get;
            set;
        }

        public int SubjectTerms
        {
            get;
            set;
        }

        public int SubjectGroupSize
        {
            get;
            set;
        }

        private bool subjectIsValid()
        {
            if (SubjectName_TB.Text.Equals(""))
            {
                MessageBox.Show("Field for name must not be empty!!");
                return false;
            }
            if (SubjectDescription_TB.Text.Equals(""))
            {
                MessageBox.Show("Field for description must not be empty!!");
                return false;
            }
            int trajanje;
            if (!int.TryParse(SubjectDuration_TB.Text, out trajanje))
            {
                MessageBox.Show("Field for duration must be integer!!");
                return false;
            }
            int brojTermina;
            if (!int.TryParse(SubjectTerms_TB.Text, out brojTermina))
            {
                MessageBox.Show("Field for terms must be integer!!");
                return false;
            }
            int velicinaGrupe;
            if (!int.TryParse(SubjectGroupSize_TB.Text, out velicinaGrupe))
            {
                MessageBox.Show("Field for group size must be integer!!");
                return false;
            }
            return true;
        }

        private bool subjectFilterIsValid()
        {
            int trajanje;
            if (!SubjectDuration_TB.Text.Equals(""))
            {
                if (!int.TryParse(SubjectDuration_TB.Text, out trajanje))
                {
                    MessageBox.Show("Field for duration must be integer!!");
                    return false;
                }
            }
            int brojTermina;
            if (!SubjectTerms_TB.Text.Equals(""))
            {
                if (!int.TryParse(SubjectTerms_TB.Text, out brojTermina))
                {
                    MessageBox.Show("Field for terms must be integer!!");
                    return false;
                }
            }
            int velicinaGrupe;
            if (!SubjectGroupSize_TB.Text.Equals(""))
            {
                if (!int.TryParse(SubjectGroupSize_TB.Text, out velicinaGrupe))
                {
                    MessageBox.Show("Field for group size must be integer!!");
                    return false;
                }
            }
            return true;
        }

        private void SubjectClearBtn_Click(object sender, RoutedEventArgs e)
        {
            subjectDataGridInit();
        }

        private void SubjectFilterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!subjectFilterIsValid()) return;
            string naziv = SubjectName_TB.Text;
            Course curs = appConfig.Curses[SubjectCourse_CB.SelectedIndex];
            string opis = SubjectDescription_TB.Text;
            int trajanje = -1;
            if (!SubjectDuration_TB.Text.Equals(""))
            {
                trajanje = Int32.Parse(SubjectDuration_TB.Text);
            }
            int brojTermina = -1;
            if (!SubjectTerms_TB.Text.Equals(""))
            {
                brojTermina = Int32.Parse(SubjectTerms_TB.Text);
            }
            bool projektor = SubjectProjector_CB.IsChecked.Value;
            bool tabla = SubjectBoard_CB.IsChecked.Value;
            bool pametnaTabla = SubjectSmartBoard_CB.IsChecked.Value;
            int os = SubjectOS_CB.SelectedIndex;
            int velicinaGrupe = -1;
            if (!SubjectGroupSize_TB.Text.Equals(""))
            {
                velicinaGrupe = Int32.Parse(SubjectGroupSize_TB.Text);
            }
            List<SoftwareItem> software = softver;

            listaPredmeta = new ObservableCollection<Subject>();
            foreach (Subject c in this.appConfig.Subjects)
            {
                bool flag = true;
                if (!naziv.Equals(""))
                {
                    if (!c.Name.Contains(opis)) flag = false;
                }
                if (!opis.Equals(""))
                {
                    if (!c.Descriptions.Contains(opis)) flag = false;
                }
                if (!curs.Id.Equals(c.Course.Id)) flag = false;
                if (trajanje != -1)
                {
                    if (c.Duration < trajanje) flag = false;
                }
                if (brojTermina != -1)
                {
                    if (c.Number < brojTermina) flag = false;
                }
                if (c.Projector != projektor) flag = false;
                if (c.Board != tabla) flag = false;
                if (c.SmartBoard != pametnaTabla) flag = false;
                if (os != c.OperatingSystem) flag = false;
                if (velicinaGrupe != -1)
                {
                    if (c.NumberOfMembers < velicinaGrupe) flag = false;
                }

                if (flag) listaPredmeta.Add(c);

            }
            Subject_DG.ItemsSource = listaPredmeta;
        }

        private void SubjectAddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!subjectIsValid()) return;
            string naziv = SubjectName_TB.Text;
            Course curs = appConfig.Curses[SubjectCourse_CB.SelectedIndex];
            string opis = SubjectDescription_TB.Text;
            int trajanje = Int32.Parse(SubjectDuration_TB.Text);
            int brojTermina = Int32.Parse(SubjectTerms_TB.Text);
            bool projektor = SubjectProjector_CB.IsChecked.Value;
            bool tabla = SubjectBoard_CB.IsChecked.Value;
            bool pametnaTabla = SubjectSmartBoard_CB.IsChecked.Value;
            int os = SubjectOS_CB.SelectedIndex;
            int velicinaGrupe = Int32.Parse(SubjectGroupSize_TB.Text);
            Console.WriteLine(softver.Count);
            List<SoftwareItem> software = softver;
            Subject subject = new Subject(naziv, curs, velicinaGrupe, opis, trajanje,
                brojTermina, brojTermina, projektor, tabla, pametnaTabla, os, softver);
            appConfig.Subjects.Add(subject);
            listaPredmeta.Add(subject);
            Subject_DG.ItemsSource = listaPredmeta;
            softver = new List<SoftwareItem>();
            freeSubjects_listView_init();
            distributedSubjects_listView_init();
        }

        private void Subject_DG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Subject_DG.SelectedItem != null)
            {
                var row = (Subject)Subject_DG.SelectedItem;
                selectedSubject = row.Id;
                SubjectName_TB.Text = row.Name;
                int index = appConfig.Curses.FindIndex(a => a.Id == row.Course.Id);
                SubjectCourse_CB.SelectedIndex = index;
                SubjectDescription_TB.Text = row.Descriptions;
                SubjectDuration_TB.Text = row.Duration.ToString();
                SubjectTerms_TB.Text = row.Number.ToString();
                SubjectProjector_CB.IsChecked = row.Projector;
                SubjectBoard_CB.IsChecked = row.Board;
                SubjectSmartBoard_CB.IsChecked = row.SmartBoard;
                SubjectOS_CB.SelectedIndex = row.OperatingSystem;
                SubjectGroupSize_TB.Text = row.NumberOfMembers.ToString();
                SubjectCurrentSoftware_LB.Items.Clear();
                softver.Clear();
                foreach (SoftwareItem elm in row.SoftwareList)
                {
                    SubjectCurrentSoftware_LB.Items.Add(elm.Name);
                    softver.Add(elm);
                }
            }
        }

        private void SubjectUpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedSubject != "")
            {
                foreach (Subject elm in appConfig.Subjects)
                {
                    if (!subjectIsValid()) return;
                    if (elm.Id.Equals(selectedSubject))
                    {
                        elm.Name = SubjectName_TB.Text;
                        elm.Course = appConfig.Curses[SubjectCourse_CB.SelectedIndex];
                        elm.Descriptions = SubjectDescription_TB.Text;
                        elm.Duration = Int32.Parse(SubjectDuration_TB.Text);
                        elm.Number = Int32.Parse(SubjectTerms_TB.Text);
                        elm.Projector = SubjectProjector_CB.IsChecked.Value;
                        elm.Board = SubjectBoard_CB.IsChecked.Value;
                        elm.SmartBoard = SubjectSmartBoard_CB.IsChecked.Value;
                        elm.OperatingSystem = SubjectOS_CB.SelectedIndex;
                        elm.NumberOfMembers = Int32.Parse(SubjectGroupSize_TB.Text);
                        elm.SoftwareList = softver;
                        softver = new List<SoftwareItem>();
                        break;
                    }
                }
                listaPredmeta.Clear();
                foreach (Subject elm in this.appConfig.Subjects)
                    listaPredmeta.Add(elm);
                Subject_DG.ItemsSource = null;
                Subject_DG.ItemsSource = listaPredmeta;
                freeSubjects_listView_init();
            }
            else
            {
                MessageBox.Show("Select subject for update!!");
                return;
            }
        }

        private void SubjectRemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedSubject != "")
            {
                int i = 0;
                foreach (Subject elm in appConfig.Subjects)
                {
                    if (elm.Id.Equals(selectedSubject)) break;
                    i++;
                }
                appConfig.Subjects.RemoveAt(i);
                listaPredmeta.Clear();
                foreach (Subject elm in this.appConfig.Subjects)
                    listaPredmeta.Add(elm);
                foreach(Classroom cr in this.appConfig.Classrooms)
                {
                    if (this.classroom != null)
                    {
                        if (cr.Id.Equals(this.classroom.Id)) this.classroom = cr;
                    }
                    List<ScheduleItem> temp = new List<ScheduleItem>();
                    foreach(ScheduleItem si in cr.Schedule.Items)
                    {
                        Console.WriteLine("lowenbrau");
                        if (si.Subject.Id.Equals(selectedSubject)) temp.Add(si);
                    }
                    foreach (ScheduleItem si in temp)
                    {
                        Console.WriteLine("usaoo    ------------------");
                        cr.Schedule.Items.Remove(si);
                    }
                }
                Subject_DG.ItemsSource = null;
                Subject_DG.ItemsSource = listaPredmeta;
                freeSubjects_listView_init();
                distributedSubjects_listView_init();
            }
        }

        private void SubjectSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            listaPredmeta.Clear();
            foreach (Subject elm in appConfig.Subjects)
                if (elm.Name.Contains(SubjectSearch_TB.Text) || elm.Id.Contains(SubjectSearch_TB.Text)
                    ||elm.Descriptions.Contains(SubjectSearch_TB.Text))
                    listaPredmeta.Add(elm);
        }

        private void ClassroomDay_CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int day = ClassroomDay_CB.SelectedIndex;
            if (this.classroom != null)
            {
                Items.Clear();
                foreach (ScheduleItem c in this.classroom.Schedule.Items)
                {
                    if (c.Day == day)
                    {
                        Items.Add(c);
                    }
                }
            }
            else
            {
                MessageBox.Show("You must select classroom!!");
                return;
            }
        }

        //Software

        private string softwareName;
        private string softwareDescription;
        private string softwareWebSite;
        private string softwarePublisher;
        private int softwarePrice;

        public string SoftwareName
        {
            get;
            set;
        }

        public string SoftwareDescription
        {
            get;
            set;
        }

        public string SoftwareWebSite
        {
            get;
            set;
        }

        public string SoftwarePublisher
        {
            get;
            set;
        }

        public int SoftwarePrice
        {
            get;
            set;
        }

        private bool softwareIsValid()
        {
            if (SoftwareName_TB.Text.Equals(""))
            {
                MessageBox.Show("Field for name must not be empty!!");
                return false;
            }
            if (SoftwarePublisher_TB.Text.Equals(""))
            {
                MessageBox.Show("Field for publisher must not be empty!!");
                return false;
            }
            if (SoftwareWebSite_TB.Text.Equals(""))
            {
                MessageBox.Show("Field for web site must not be empty!!");
                return false;
            }
            double cijena;
            if (!double.TryParse(SoftwarePrice_TB.Text, out cijena))
            {
                MessageBox.Show("Field for price must be double!!");
                return false;
            }
            if (SoftwareDescription_TB.Text.Equals(""))
            {
                MessageBox.Show("Field for description must not be empty!!");
                return false;
            }
            return true;
        }

        private bool softwareFilterIsValid()
        {
            double cijena;
            if (!SoftwarePrice_TB.Text.Equals(""))
            {
                if (!double.TryParse(SoftwarePrice_TB.Text, out cijena))
                {
                    MessageBox.Show("Field for price must be double!!");
                    return false;
                }
            }
           
            return true;
        }
        private void Software_DG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Software_DG.SelectedItem != null)
            {
                var row = (SoftwareItem)Software_DG.SelectedItem;
                selectedSoftwareItem = row.Id;
                SoftwareName_TB.Text = row.Name;
                SoftwareOS_CB.SelectedIndex = row.OperatingSystem;
                SoftwarePublisher_TB.Text = row.Producer;
                SoftwareWebSite_TB.Text = row.Site;
                SoftwarePublishingDate_DP.Text = row.ProductionYear.ToString();
                SoftwarePrice_TB.Text = row.Price.ToString();
            }
        }

        private void SoftwareFilterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!softwareFilterIsValid()) return;
            string naziv = SoftwareName_TB.Text;
            int os = SoftwareOS_CB.SelectedIndex;
            string proizvodjac = SoftwarePublisher_TB.Text;
            string sajt = SoftwareWebSite_TB.Text;
            DateTime godinaIzdavanja = DateTime.Parse(SoftwarePublishingDate_DP.Text);

            double cijena = -1;
            if (!SoftwarePrice_TB.Text.Equals(""))
            {
                cijena = Double.Parse(SoftwarePrice_TB.Text);
            }
            string opis = SoftwareDescription_TB.Text;
            listaSoftvera = new ObservableCollection<SoftwareItem>();
            foreach (SoftwareItem c in this.appConfig.SoftwareItems)
            {
                bool flag = true;
                if (!naziv.Equals(""))
                {
                    if (!c.Name.Contains(opis)) flag = false;
                }
                if (os != c.OperatingSystem) flag = false;
                if (!proizvodjac.Equals(""))
                {
                    if (!c.Producer.Contains(proizvodjac)) flag = false;
                }
                if (!sajt.Equals(""))
                {
                    if (!c.Site.Contains(sajt)) flag = false;
                }
                if (!opis.Equals(""))
                {
                    if (!c.Description.Contains(opis)) flag = false;
                }
                
                if (cijena != -1)
                {
                    if (c.Price < cijena) flag = false;
                }

                if (godinaIzdavanja != c.ProductionYear) flag = false;
                if (flag) listaSoftvera.Add(c);
                
            }
            Software_DG.ItemsSource = listaSoftvera;
        }

        private void SoftwareClearBtn_Click(object sender, RoutedEventArgs e)
        {
            softwareDataGridInit();
        }

        private void SoftwareAddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!softwareIsValid()) return;
            string naziv = SoftwareName_TB.Text;
            int os = SoftwareOS_CB.SelectedIndex;
            string proizvodjac = SoftwarePublisher_TB.Text;
            string sajt = SoftwareWebSite_TB.Text;
            DateTime godinaIzdavanja = DateTime.Parse(SoftwarePublishingDate_DP.Text);
            double cijena = Double.Parse(SoftwarePrice_TB.Text);
            string opis = SoftwareDescription_TB.Text;
            SoftwareItem item = new SoftwareItem(naziv, os, proizvodjac, sajt, godinaIzdavanja, cijena, opis);
            appConfig.SoftwareItems.Add(item);
            listaSoftvera.Add(item);
            Software_DG.ItemsSource = listaSoftvera;
            subjectSoftware_TB_init();
        }

        private void SoftwareUpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedSoftwareItem != "")
            {
                foreach (SoftwareItem elm in appConfig.SoftwareItems)
                {
                    if (!softwareIsValid()) return;
                    if (elm.Id.Equals(selectedSoftwareItem))
                    {
                        elm.Name = SoftwareName_TB.Text;
                        elm.OperatingSystem = SoftwareOS_CB.SelectedIndex;
                        elm.Producer = SoftwarePublisher_TB.Text;
                        elm.Site = SoftwareWebSite_TB.Text;
                        elm.ProductionYear = DateTime.Parse(SoftwarePublishingDate_DP.Text);
                        elm.Price = Double.Parse(SoftwarePrice_TB.Text);
                        elm.Description = SoftwarePrice_TB.Text;
                        break;
                    }
                }
                listaSoftvera.Clear();
                foreach (SoftwareItem elm in this.appConfig.SoftwareItems)
                    listaSoftvera.Add(elm);
                Software_DG.ItemsSource = null;
                Software_DG.ItemsSource = listaSoftvera;
                subjectSoftware_TB_init();
            }
            else
            {
                MessageBox.Show("Select software for update!!");
            }
        }

        private void SoftwareRemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedSoftwareItem != "")
            {
                int i = 0;
                foreach (SoftwareItem elm in appConfig.SoftwareItems)
                {
                    if (elm.Id.Equals(selectedSoftwareItem))
                    {
                        break;
                    }
                    i++;
                }
                appConfig.SoftwareItems.RemoveAt(i);
                listaSoftvera.Clear();
                foreach (SoftwareItem elm in this.appConfig.SoftwareItems)
                    listaSoftvera.Add(elm);
                Software_DG.ItemsSource = null;
                Software_DG.ItemsSource = listaSoftvera;
                subjectSoftware_TB_init();
            }
        }

        private void SoftwareSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            listaSoftvera.Clear();
            foreach (SoftwareItem elm in appConfig.SoftwareItems)
            {
                if (elm.Name.Contains(SoftwareSearch_TB.Text)||elm.Description.Contains(SoftwareSearch_TB.Text)
                    ||elm.Id.Contains(SoftwareSearch_TB.Text)||elm.Producer.Contains(SoftwareSearch_TB.Text)
                    ||elm.Site.Contains(SoftwareSearch_TB.Text))
                {
                    listaSoftvera.Add(elm);
                }
            }
        }

        private void SubjectAddSoftwareBtn_Click(object sender, RoutedEventArgs e)
        {
            int i = SubjectSoftware_CB.SelectedIndex;
            softver.Add(appConfig.SoftwareItems[i]);
            SubjectCurrentSoftware_LB.Items.Add(appConfig.SoftwareItems[i].Name);
        }

        private void mainClassroom_CB_Changed(object sender, SelectionChangedEventArgs e)
        {
            int index = MainClassroom_CB.SelectedIndex;
            if (index != -1)
            {
                Classroom classroom = appConfig.Classrooms[index];
                this.classroom = classroom;
                this.currentClassroom = classroom.Id;
                MainClassroomName_TB.Text = classroom.Description;
                MainClassRoomSeats_TB.Text = classroom.NumberOfWorkplace.ToString();
                if (classroom.Windows == true && classroom.Linux == false) MainClassRoomOS_TB.Text = "Windows";
                if (classroom.Windows == false && classroom.Linux == true) MainClassRoomOS_TB.Text = "Linux";
                if (classroom.Windows == true && classroom.Linux == true) MainClassRoomOS_TB.Text = "Cross-platform";
                Console.WriteLine("Windows: " + classroom.Windows);
                Console.WriteLine("Linux: " + classroom.Linux);
                MainClassroomProjector_CB.IsChecked = classroom.Projector;
                MainClassroomBoard_CB.IsChecked = classroom.Board;
                MainClassroomSmartBoard_CB.IsChecked = classroom.SmartBoart;
                freeSubjects_listView_init();
                distributedSubjects_listView_init();
            }
        }

        public void freeSubjects_listView_init()
        {
            if (this.classroom != null)
            {
                Console.WriteLine("Broj ucionica: " + this.appConfig.Subjects.Count);
                foreach (Subject s in this.appConfig.Subjects)
                {
                    bool flag = true;
                    if (this.classroom.NumberOfWorkplace < s.NumberOfMembers) { flag = false; Console.WriteLine("usaoo1"); }
                    if (!this.classroom.Windows && this.classroom.Linux && s.OperatingSystem == 2) { flag = false; Console.WriteLine("usaoo2"); }
                    if (this.classroom.Windows && !this.classroom.Linux && s.OperatingSystem == 2) { flag = false; Console.WriteLine("usaoo9"); }
                    if (!this.classroom.Linux && s.OperatingSystem == 1) { flag = false; Console.WriteLine("usaoo3"); }
                    if (!this.classroom.Windows && s.OperatingSystem == 0) { flag = false; Console.WriteLine("usaoo4"); }
                    if (!this.classroom.Projector && s.Projector) { flag = false; Console.WriteLine("usaoo5"); }
                    if (!this.classroom.Board && s.Board) { flag = false; Console.WriteLine("usaoo6"); }
                    if (!this.classroom.SmartBoart && s.SmartBoard) { flag = false; Console.WriteLine("usaoo7"); }
                    if (s.CurrentNumber == 0) { flag = false; Console.WriteLine("usaoo8"); }
                    //if (flag) Predmeti.Add(s);
                    if (Predmeti.Contains(s) && !flag) { Predmeti.Remove(s); Console.WriteLine("USAO1"); Console.WriteLine(flag); }
                    if (!Predmeti.Contains(s) && flag) Predmeti.Add(s);
                }
            }
        }

        public void distributedSubjects_listView_init()
        {
            if (this.classroom != null)
            {
                int day = ClassroomDay_CB.SelectedIndex;
                Items.Clear();
                foreach (ScheduleItem c in this.classroom.Schedule.Items)
                {
                    if (c.Day == day)
                    {
                        Console.WriteLine(c.Subject.Name+"   ++++++++++++");
                        Items.Add(c);
                    }
                }
            }
        }

        private void freeSubjects_listView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(MainClassroom_CB.SelectedIndex==-1)
            {
                MessageBox.Show("You must select classroom!!");
                return;
            }
            if (ClassroomDay_CB.SelectedIndex == -1)
            {
                MessageBox.Show("You must select day in week!!");
                return;
            }
            startPoint = e.GetPosition(null);
        }

        private void freeSubjects_listView_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                // Get the dragged ListViewItem
                ListView listView = sender as ListView;
                ListViewItem listViewItem =
                    FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

                // Find the data behind the ListViewItem
                Subject subject = (Subject)listView.ItemContainerGenerator.
                    ItemFromContainer(listViewItem);

                // Initialize the drag & drop operation
                DataObject dragData = new DataObject("myFormat", subject);
                DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
            }
        }

        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void distributedSubjects_listView_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void distributedSubjects_listView_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                Subject subject = e.Data.GetData("myFormat") as Subject;
                Classroom current = getClassroom(this.currentClassroom);
                Window1 aw = new Window1(current, this.appConfig, subject, Items, ClassroomDay_CB.SelectedIndex);
                aw.ShowDialog();
                Predmeti.Remove(subject);
                Predmeti1.Add(subject);
            }
        }

        private Classroom getClassroom(string id)
        {
            Classroom cr = null;
            foreach (Classroom c in this.appConfig.Classrooms)
            {
                if (c.Id.Equals(id))
                {
                    cr = c;
                    break;
                }
            }
            return cr;
        }


        //Help

        public void doThings(string param)
        {
            ClassroomAddBtn.Background = new SolidColorBrush(Color.FromRgb(32, 64, 128));
            Title = param;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                Console.WriteLine(str);
                HelpProvider.ShowHelp(str, this);
            }
        }

        //second list viw delete item

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            int idx = distributedSubjects_listView.SelectedIndex;
            string subjectId="";
            foreach(Classroom c in this.appConfig.Classrooms)
            {
               if(c.Id.Equals(this.classroom.Id))
                {
                    this.classroom = c;
                    subjectId = c.Schedule.Items[idx].Subject.Id;
                    c.Schedule.Items.RemoveAt(idx);
                    break;
                }
            }
            foreach(Subject s in this.appConfig.Subjects)
            {
                if(s.Id.Equals(subjectId))
                {
                    s.CurrentNumber++;
                    break;
                }
            }
            freeSubjects_listView_init();
            distributedSubjects_listView_init();
        }

        private void helpButton_Click(object sender, RoutedEventArgs e)
        {
            Help help = new Help("index.htm");
            help.ShowDialog();
        }

        private void tutorialButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("bt.html");
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            int idx = SubjectCurrentSoftware_LB.SelectedIndex;
            Console.WriteLine(idx);
            if (selectedSubject != "")
            {
                Subject subject = null;
                foreach (Subject s in this.appConfig.Subjects)
                {
                    if (s.Id.Equals(selectedSubject))
                    {
                        subject = s;
                        break;
                    }
                }

                subject.SoftwareList.RemoveAt(idx);
            }
            SubjectCurrentSoftware_LB.Items.RemoveAt(idx);
        }

        private void DataGrid_OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Schedule")
            {
                e.Column = null;
            }
            if (e.PropertyName == "Id")
            {
                e.Column.Header = "Classroom ID";
            }
            if (e.PropertyName == "NumberOfWorkplace")
            {
                e.Column.Header = "Seats";
            }
            if (e.PropertyName == "SmartBoart")
            {
                e.Column.Header = "Smart Board";
            }
        }

        private void DataGrid1_OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Course")
            {
                e.Column = null;
            }
            if (e.PropertyName == "SoftwareList")
            {
                e.Column = null;
            }
            if (e.PropertyName == "OperatingSystem")
            {
                e.Column = null;
            }
            if (e.PropertyName == "NumberOfMembers")
            {
                e.Column.Header = "Number Of Members";
            }
            if (e.PropertyName == "CurrentNumber")
            {
                e.Column.Header = "Scheduling";
            }
            if (e.PropertyName == "SmartBoard")
            {
                e.Column.Header = "Smart Board";
            }
        }

        private void DataGrid2_OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "OperatingSystem")
            {
                e.Column = null;
            }
            if (e.PropertyName == "ProductionYear")
            {
                e.Column.Header = "Production Year";
            }
        }

        private void DataGrid3_OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "DateFoundation")
            {
                e.Column.Header = "Date of Foundation";
            }
        }
    }
}
