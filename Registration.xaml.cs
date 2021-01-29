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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {

        public Frame frame;

        public Registration()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=BDmed;Integrated Security=True";
                
                string log = txlog.Text;
                string password = txpas.Text;
                string secondname = txfam.Text;

                string sqlExpression = "INSERT INTO dbo.Users(Login, Password, SecondName) VALUES ('" + log + "', '" + password + "', '" + secondname + "')";
                MessageBox.Show(sqlExpression);
                


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    int number = command.ExecuteNonQuery();
                    MessageBox.Show("Добавлено объектов: " + Convert.ToString(number));
                    //MainWindow.StudentInfo fileInfo = new MainWindow.StudentInfo();
                    //fileInfo.Medname = log;
                    //fileInfo.Discription = password;
                    //fileInfo.Category = secondname;

                    //MainWindow.studInfo.Add(fileInfo);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный ввод", "ОШИБКА");
            }


            LoginBD boughtback = new LoginBD { frame = this.frame }; //Передача в presenter чтобы не был пустым
            frame.Content = boughtback;
        }


        
    }
}
