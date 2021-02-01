using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
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

namespace MedBd
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public Frame frame;

        public static bool admin = false;
        public static string mainlogin = "";
        public MainWindow()
        {
            InitializeComponent();

            LoginBD log = new LoginBD(); // open   login page
            log.frame = MainFrame;
            MainFrame.Content = log;

      

        }


    }
}
