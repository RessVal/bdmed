using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для LoginBD.xaml
    /// </summary>
    public partial class LoginBD : Page
    {
        public Frame frame;

        public LoginBD()
        {
            InitializeComponent();
        }

        bool accept = false;

        private void FindLike(string execcomm) // Обновление вывода
        {
            //MainWindow.studInfo.Clear(); // очищаем всё, если там что-то будет
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=BDmed;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = execcomm;
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                  
                {
                    accept = true;
                    if (accept)
                    {

                        choice ch = new choice { frame = this.frame }; //Передача в presenter чтобы не был пустым
                        frame.Content = ch;
                    }

                    while (reader.Read()) // построчно считываем данные
                    {
                        //MainWindow.StudentInfo fileInfo = new MainWindow.StudentInfo();
                        //fileInfo.Log = Convert.ToInt32(reader.GetValue(0));
                        //fileInfo.Medname = Convert.ToString(reader.GetValue(1));
                        //fileInfo.Discription = Convert.ToString(reader.GetValue(2));
                        //fileInfo.Category = Convert.ToString(reader.GetValue(3));
                        ////FileInfoView.ItemsSource = MainWindow.studInfo;
                        //MainWindow.studInfo.Add(fileInfo);
                    }

                }
                else
                {
                    MessageBox.Show("Не правильно");
                }

                reader.Close();

            }
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            
            FindLike("SELECT * FROM Users WHERE Login = '" + txlog.Text + "'AND Password = '" + txpass.Text + "'");
        }

        private void Registration(object sender, RoutedEventArgs e)
        {
            Registration reg = new Registration { frame = this.frame }; //Передача в presenter чтобы не был пустым
            frame.Content = reg;
        }
    }
}
