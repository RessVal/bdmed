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
    /// Логика взаимодействия для buy.xaml
    /// </summary>
    public partial class buy : Page
    {
        public buy()
        {
            InitializeComponent();
        }
        public Frame frame;

        public static class BuyInfo
        {
            public static int NumberItems { get; set; }

        }

        private void Add(object sender, RoutedEventArgs e)
        {
            bool a = true;

            try
            {
                string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=BDmed;Integrated Security=True";
                int uniqueID = 0;
                if (MainWindow.OrdersList.Count > 0)
                {
                   uniqueID = MainWindow.OrdersList[MainWindow.OrdersList.Count - 1].NumberOrd + 1;

                }
                
                string date = txdate.Text;
                string log = MainWindow.mainlogin;
                string adr = txadr.Text;
                int items = BuyInfo.NumberItems;


                string sqlExpression = "INSERT INTO dbo.Orders(NumberOrd, DateDelivery, Login, Adress , NumberItems) VALUES (" + uniqueID + ", '" + date + "', '" + log + "', '" + adr + "', '" + items + "')";
                MessageBox.Show(sqlExpression);



                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    int number = command.ExecuteNonQuery();
                    MessageBox.Show("Добавлено объектов: " + Convert.ToString(number));
                    Orders.OrdersInfo fileInfo = new Orders.OrdersInfo();
                    fileInfo.NumberOrd = uniqueID;
                    fileInfo.DateDelivery = date;
                    fileInfo.Login = log;

                    fileInfo.Adress = adr;
                    fileInfo.NumberItems = Convert.ToInt32(items);

                    Orders.OrdersList.Add(fileInfo);

                    MainWindow.Metod();

                    a = true;
                }



            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный ввод", "ОШИБКА");

                AddOrd back = new AddOrd { frame = this.frame }; //Возвращаем
                frame.Content = back;
                a = false;
            }
            catch (SqlException)
            {
                MessageBox.Show("Такого значения нет!", "ОШИБКА");

                AddOrd back = new AddOrd { frame = this.frame };
                frame.Content = back;
                a = false;
            }

            if (a == true)
            {
                Orders boughtback = new Orders { frame = this.frame }; //Передача в presenter чтобы не был пустым
                frame.Content = boughtback;
            }
        }
    }
}
