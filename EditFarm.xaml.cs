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
    /// Логика взаимодействия для EditFarm.xaml
    /// </summary>
    public partial class EditFarm : Page
    {
        public Frame frame;

        public static int index;
        public static class EditFarmInfo
        {
            public static int NumberFarm { get; set; }
            public static string NameFarm { get; set; }
            public static string AdressFarm { get; set; }


        }

        public EditFarm()
        {
            InitializeComponent();
        }

        private void Edit_BTN(object sender, RoutedEventArgs e)
        {

            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=BDmed;Integrated Security=True";

            string sqlExpression = "UPDATE Farmacy SET NameFarm='" + txname.Text + "', AdressFarm='" + txad.Text + "' WHERE NumberFarm=" + EditFarmInfo.NumberFarm;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                MessageBox.Show("Обновлено объектов:" + number);
            }
            Farmacy farm = new Farmacy { frame = this.frame };

            frame.Content = farm;
        }

    }
}
