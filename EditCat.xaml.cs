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
    /// Логика взаимодействия для EditCat.xaml
    /// </summary>
    public partial class EditCat : Page
    {
        public Frame frame;

        public static int index;
        public static class EditCatInfo
        {
            public static int NumberCat { get; set; }
            public static string NameCat { get; set; }

        }

        public EditCat()
        {
            InitializeComponent();
        }

        private void Edit_BTN(object sender, RoutedEventArgs e)
        {

            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=BDmed;Integrated Security=True";

            string sqlExpression = "UPDATE Category SET NameCat='" + txname.Text + "' WHERE NumberCat=" + EditCatInfo.NumberCat;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                MessageBox.Show("Обновлено объектов:" + number);
            }
            Category cat = new Category { frame = this.frame };

            frame.Content = cat;
        }

    }
}