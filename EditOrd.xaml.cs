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
    /// Логика взаимодействия для EditOrd.xaml
    /// </summary>
    public partial class EditOrd : Page
    {
        public Frame frame;

        public static int index;
        public static class EditOrdInfo
        {
            public static int NumberOrders { get; set; }
            public static string DateDelivery { get; set; }
            public static string Login { get; set; }
            public static string Adress { get; set; }
            public static int NumberItems { get; set; }

        }

        public EditOrd()
        {
            InitializeComponent();
        }

        private void Edit_BTN(object sender, RoutedEventArgs e)
        {

            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=BDmed;Integrated Security=True";

            string sqlExpression = "UPDATE Orders SET DateDelivery='" + txdate.Text + "', Login='" + txlog.Text + "',Adress= '" + txadr.Text + "',NumberItems= '" + txitems.Text + "' WHERE NumberOrd=" + EditOrdInfo.NumberOrders;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                MessageBox.Show("Обновлено объектов:" + number);
            }
            Orders main = new Orders { frame = this.frame }; //Передача в presenter чтобы не был пустым

            frame.Content = main;
        }

    }
}
