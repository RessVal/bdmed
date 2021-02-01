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
        public class OrdersInfo
        {
            public int NumberOrd { get; set; }
            public string DateDelivery { get; set; }
            public string Login { get; set; }
            public string Adress { get; set; }
            public int NumberItems { get; set; }

        }

        public Frame frame;

        public static ObservableCollection<OrdersInfo> OrdersList = new ObservableCollection<OrdersInfo>();

        public static bool admin = false;
        public static string mainlogin = "";
        public MainWindow()
        {
            InitializeComponent();

            LoginBD log = new LoginBD(); // open   login page
            log.frame = MainFrame;
            MainFrame.Content = log;

            OrdersList.Clear(); // Очистка окна вывода

            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=BDmed;Integrated Security=True"; // куда подкл

            string sqlExpression = "SELECT * FROM Orders"; //Запрос



            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {


                    while (reader.Read()) // построчно считываем данные
                    {

                        OrdersInfo fileInfo = new OrdersInfo();
                        fileInfo.NumberOrd = Convert.ToInt32(reader.GetValue(0));
                        fileInfo.DateDelivery = Convert.ToString(reader.GetValue(1));
                        fileInfo.Login = Convert.ToString(reader.GetValue(2));
                        fileInfo.Adress = Convert.ToString(reader.GetValue(3));
                        fileInfo.NumberItems = Convert.ToInt32(reader.GetValue(4));

                        OrdersList.Add(fileInfo);
                    }
                }

                reader.Close();
            }

        }

        public static void Metod()
        {


            OrdersList.Clear(); // Очистка окна вывода

            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=BDmed;Integrated Security=True"; // куда подкл

            string sqlExpression = "SELECT * FROM Orders"; //Запрос
                                                           //if (!MainWindow.admin)
                                                           //{
                                                           //sqlExpression = "SELECT * FROM Orders WHERE Login ='" + MainWindow.mainlogin + "'"; //Запрос
                                                           //}

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {


                    while (reader.Read()) // построчно считываем данные
                    {

                        OrdersInfo fileInfo = new OrdersInfo();
                        fileInfo.NumberOrd = Convert.ToInt32(reader.GetValue(0));
                        fileInfo.DateDelivery = Convert.ToString(reader.GetValue(1));
                        fileInfo.Login = Convert.ToString(reader.GetValue(2));
                        fileInfo.Adress = Convert.ToString(reader.GetValue(3));
                        fileInfo.NumberItems = Convert.ToInt32(reader.GetValue(4));

                        OrdersList.Add(fileInfo);
                    }
                }

                reader.Close();
            }
        }


    }
}
