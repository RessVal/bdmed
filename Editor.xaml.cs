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
    /// Логика взаимодействия для Editor.xaml
    /// </summary>
    public partial class Editor : Page
    {
        public Frame frame;

        public static int index;
        public static class EditorInfo
        {
            public static int NumberItems { get; set; }
            public static string NameItems { get; set; }
            public static string Discription { get; set; }
            public static int NumberCat { get; set; }
            public static int NumberFarm { get; set; }

        }

        public Editor()
        {
            InitializeComponent();
        }

        private void Edit_BTN(object sender, RoutedEventArgs e)
        {

            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=BDmed;Integrated Security=True";
            
            string sqlExpression = "UPDATE Items SET NameItems='" + txname.Text + "', Discription='" + txDis.Text + "',NumberCat= '" + txcategor.Text + "',NumberFarm= '" + txfarm.Text + "' WHERE NumberItems=" + EditorInfo.NumberItems;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                MessageBox.Show("Обновлено объектов:" + number);
            }
            Items items = new Items { frame = this.frame }; //Передача в presenter чтобы не был пустым
            
            frame.Content = items;
        }
        
    }
}
