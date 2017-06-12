using System;
using System.Collections.Generic;
using System.IO;
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

namespace HCI2
{
    /// <summary>
    /// Interaction logic for Help.xaml
    /// </summary>
    public partial class Help : Window
    {
        public Help(string nameForm)
        {
            InitializeComponent();
            Console.WriteLine(File.Exists(nameForm) ? "File exists." : "File does not exist.");
            string curDir = Directory.GetCurrentDirectory();
            //this.webBrowser1.Url = new Uri(String.Format("file:///{0}/"+ nameForm, curDir));
            wbSample.Navigate(String.Format("file:///{0}/" + nameForm, curDir));
        }
    }
}
