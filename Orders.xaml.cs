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
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : Page
    {
        public Frame frame;

        public class OrdersInfo
        {
            public int NumberOrd { get; set; }
            public string DateDelivery { get; set; }
            public string Login { get; set; }
            public string Adress { get; set; }
            public int NumberItems { get; set; }

        }

        public static ObservableCollection<OrdersInfo> OrdersList = new ObservableCollection<OrdersInfo>();

        public Orders()
        {
            InitializeComponent();

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
                    // выводим названия столбцов
                    //Console.WriteLine("{0}\t{1}\t{2}", reader.GetName(0), reader.GetName(1), reader.GetName(2));

                    while (reader.Read()) // построчно считываем данные
                    {
                        OrdersInfo fileInfo = new OrdersInfo();
                        fileInfo.NumberOrd = Convert.ToInt32(reader.GetValue(0));
                        fileInfo.DateDelivery = Convert.ToString(reader.GetValue(1));
                        fileInfo.Login = Convert.ToString(reader.GetValue(2));
                        fileInfo.Adress = Convert.ToString(reader.GetValue(3));
                        fileInfo.NumberItems = Convert.ToInt32(reader.GetValue(4));
                        FileInfoView.ItemsSource = OrdersList;
                        OrdersList.Add(fileInfo);
                    }
                }

                reader.Close();
            }
        }

        private void Edit(object sender, RoutedEventArgs e) //Кнопка редактора
        {
            try
            {
                EditOrd editord = new EditOrd { frame = this.frame }; //Передача в presenter чтобы не был пустым

                //editor.edita.Content = Convert.ToString(FileInfoView.SelectedIndex);
                editord.txdate.Text = OrdersList[FileInfoView.SelectedIndex].DateDelivery;
                editord.txlog.Text = OrdersList[FileInfoView.SelectedIndex].Login;
                editord.txadr.Text = OrdersList[FileInfoView.SelectedIndex].Adress;
                editord.txitems.Text = Convert.ToString(OrdersList[FileInfoView.SelectedIndex].NumberItems);

                //editor.edita.Content = Convert.ToString(FileInfoView.SelectedIndex);
                EditOrd.index = FileInfoView.SelectedIndex;
                //EditOrd.EditOrdInfo.NumberOrd = OrdersList[FileInfoView.SelectedIndex].NumberOrd;
                EditOrd.EditOrdInfo.DateDelivery = OrdersList[FileInfoView.SelectedIndex].DateDelivery;
                EditOrd.EditOrdInfo.Login = OrdersList[FileInfoView.SelectedIndex].Login;
                EditOrd.EditOrdInfo.Adress = OrdersList[FileInfoView.SelectedIndex].Adress;
                EditOrd.EditOrdInfo.NumberItems = OrdersList[FileInfoView.SelectedIndex].NumberItems;
                frame.Content = editord;
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Элемент не выбран", "ОШИБКА");
            }
        }

        private void FindLike(string execcomm) // Обновление вывода
        {
            OrdersList.Clear(); // очищаем всё, если там что-то будет
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

                    while (reader.Read()) // построчно считываем данные
                    {
                        OrdersInfo fileInfo = new OrdersInfo();
                        fileInfo.NumberOrd = Convert.ToInt32(reader.GetValue(0));
                        fileInfo.DateDelivery = Convert.ToString(reader.GetValue(1));
                        fileInfo.Login = Convert.ToString(reader.GetValue(2));
                        fileInfo.Adress = Convert.ToString(reader.GetValue(3));
                        fileInfo.NumberItems = Convert.ToInt32(reader.GetValue(4));
                        FileInfoView.ItemsSource = OrdersList;
                        OrdersList.Add(fileInfo);
                    }

                }

                reader.Close();

            }
        }

        private void AddNew_Btn(object sender, RoutedEventArgs e)
        {
            AddOrd adding = new AddOrd { frame = this.frame }; //Передача в presenter чтобы не был пустым
            frame.Content = adding;

        }

        private void FindCat(object sender, TextChangedEventArgs e)
        {
            FindLike("SELECT * FROM Orders WHERE NumberOrd LIKE '" + ordsearch.Text + "%'");
        }

        private void RefreshOrb(object sender, RoutedEventArgs e)
        {
            Items item = new Items { frame = this.frame }; //Передача в presenter чтобы не был пустым

            frame.Content = item;
        }

        private void BackToChoice(object sender, RoutedEventArgs e)
        {
            choice ch = new choice { frame = this.frame }; //Возвращаемся на главную страницу

            frame.Content = ch;
        }

        private void Delete(object sender, RoutedEventArgs e)
        {

            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=BDmed;Integrated Security=True";
            string sqlExpression = "DELETE  FROM Orders WHERE NumberOrd=" + OrdersList[FileInfoView.SelectedIndex].NumberOrd;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                if (number == 0)
                {
                    MessageBox.Show("Объект не найден");
                }
                else
                {
                    MessageBox.Show("Удалено объектов: " + number);
                    OrdersList.RemoveAt(FileInfoView.SelectedIndex);
                }
            }
        }
    }
}
