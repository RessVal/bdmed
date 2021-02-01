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
    /// Логика взаимодействия для Items.xaml
    /// </summary>
    public partial class Items : Page
    {
        public Frame frame;

        public class ItemsInfo
        {
            public int NumberItems { get; set; }
            public string NameItems { get; set; }
            public string Discription { get; set; }
            public int NumberCat { get; set; }
            public int NumberFarm { get; set; }

        }

        public static ObservableCollection<ItemsInfo> ItemsList = new ObservableCollection<ItemsInfo>();
        public static int itemid = 0;
        public Items()
        {

            InitializeComponent();

            if (!MainWindow.admin)
            {


                delete.Visibility = Visibility.Hidden;
                add.Visibility = Visibility.Hidden;
                edit.Visibility = Visibility.Hidden;

            }
            else
            {
                b.Visibility = Visibility.Hidden;
            }


            ItemsList.Clear(); // Очистка окна вывода

            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=BDmed;Integrated Security=True"; // куда подкл

            string sqlExpression = "SELECT * FROM Items"; //Запрос
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {


                    while (reader.Read()) // построчно считываем данные
                    {
                        ItemsInfo fileInfo = new ItemsInfo();
                        fileInfo.NumberItems = Convert.ToInt32(reader.GetValue(0));
                        fileInfo.NameItems = Convert.ToString(reader.GetValue(1));
                        fileInfo.Discription = Convert.ToString(reader.GetValue(2));
                        fileInfo.NumberCat = Convert.ToInt32(reader.GetValue(3));
                        fileInfo.NumberFarm = Convert.ToInt32(reader.GetValue(4));
                        FileInfoView.ItemsSource = ItemsList;
                        ItemsList.Add(fileInfo);
                    }
                }

                reader.Close();
            }
        }

        private void Edit(object sender, RoutedEventArgs e) //Кнопка редактора
        {
            try
            {
                Editor editor = new Editor { frame = this.frame }; //Передача в presenter чтобы не был пустым

                //editor.edita.Content = Convert.ToString(FileInfoView.SelectedIndex);
                editor.txname.Text = ItemsList[FileInfoView.SelectedIndex].NameItems;
                editor.txDis.Text = ItemsList[FileInfoView.SelectedIndex].Discription;
                editor.txcategor.Text = Convert.ToString(ItemsList[FileInfoView.SelectedIndex].NumberCat);
                editor.txfarm.Text = Convert.ToString(ItemsList[FileInfoView.SelectedIndex].NumberFarm);

                //editor.edita.Content = Convert.ToString(FileInfoView.SelectedIndex);
                Editor.index = FileInfoView.SelectedIndex;
                Editor.EditorInfo.NumberItems = ItemsList[FileInfoView.SelectedIndex].NumberItems;
                Editor.EditorInfo.NameItems = ItemsList[FileInfoView.SelectedIndex].NameItems;
                Editor.EditorInfo.Discription = ItemsList[FileInfoView.SelectedIndex].Discription;
                Editor.EditorInfo.NumberCat = ItemsList[FileInfoView.SelectedIndex].NumberCat;
                Editor.EditorInfo.NumberFarm = ItemsList[FileInfoView.SelectedIndex].NumberFarm;
                frame.Content = editor;
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Элемент не выбран", "ОШИБКА");
            }
        }

        private void FindLike(string execcomm) // Обновление вывода
        {
            ItemsList.Clear(); // очищаем всё, если там что-то будет
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
                        ItemsInfo fileInfo = new ItemsInfo();
                        fileInfo.NumberItems = Convert.ToInt32(reader.GetValue(0));
                        fileInfo.NameItems = Convert.ToString(reader.GetValue(1));
                        fileInfo.Discription = Convert.ToString(reader.GetValue(2));
                        fileInfo.NumberCat = Convert.ToInt32(reader.GetValue(3));
                        fileInfo.NumberFarm = Convert.ToInt32(reader.GetValue(4));
                        FileInfoView.ItemsSource = ItemsList;
                        ItemsList.Add(fileInfo);
                    }

                }

                reader.Close();

            }
        }

        private void AddNew_Btn(object sender, RoutedEventArgs e)
        {
            AddMed adding = new AddMed { frame = this.frame }; //Передача в presenter чтобы не был пустым
            frame.Content = adding;

        }

        private void FindCat(object sender, TextChangedEventArgs e)
        {
            FindLike("SELECT * FROM Items WHERE NumberItems LIKE '" + catsearch.Text + "%'");
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
            try
            {
                string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=BDmed;Integrated Security=True";
                string sqlExpression = "DELETE  FROM Items WHERE NumberItems=" + ItemsList[FileInfoView.SelectedIndex].NumberItems;
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
                        ItemsList.RemoveAt(FileInfoView.SelectedIndex);
                    }
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Нельзя удалить товар , который заказан");
            }
            





        }

        private void Buy(object sender, RoutedEventArgs e)
        {

            buy b = new buy { frame = this.frame }; 

            frame.Content = b;
            buy.BuyInfo.NumberItems = ItemsList[FileInfoView.SelectedIndex].NumberItems;

        }
    }
}
