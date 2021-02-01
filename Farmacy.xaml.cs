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
    /// Логика взаимодействия для Farmacy.xaml
    /// </summary>
    public partial class Farmacy : Page
    {
        public Frame frame;

        public class FarmInfo
        {
            public int NumberFarm { get; set; }
            public string NameFarm { get; set; }
            public string AdressFarm { get; set; }

        }

        public static ObservableCollection<FarmInfo> FarmList = new ObservableCollection<FarmInfo>();

        public Farmacy()
        {
            InitializeComponent();

            if (!MainWindow.admin)
            {


                delete.Visibility = Visibility.Hidden;
                add.Visibility = Visibility.Hidden;
                edit.Visibility = Visibility.Hidden;
            }


            FarmList.Clear(); // Очистка окна вывода

            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=BDmed;Integrated Security=True"; // куда подкл

            string sqlExpression = "SELECT * FROM Farmacy"; //Запрос
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {


                    while (reader.Read()) // построчно считываем данные
                    {
                        FarmInfo fileInfo = new FarmInfo();
                        fileInfo.NumberFarm = Convert.ToInt32(reader.GetValue(0));
                        fileInfo.NameFarm = Convert.ToString(reader.GetValue(1));
                        fileInfo.AdressFarm = Convert.ToString(reader.GetValue(2));

                        FileInfoView.ItemsSource = FarmList;
                        FarmList.Add(fileInfo);
                    }
                }

                reader.Close();
            }
        }

        private void Edit(object sender, RoutedEventArgs e) //Кнопка редактора
        {
            try
            {
                EditFarm editor = new EditFarm { frame = this.frame }; //Передача в presenter чтобы не был пустым


                editor.txname.Text = FarmList[FileInfoView.SelectedIndex].NameFarm;
                editor.txad.Text = FarmList[FileInfoView.SelectedIndex].AdressFarm;



                Editor.index = FileInfoView.SelectedIndex;
                EditFarm.EditFarmInfo.NumberFarm = FarmList[FileInfoView.SelectedIndex].NumberFarm;
                EditFarm.EditFarmInfo.NameFarm = FarmList[FileInfoView.SelectedIndex].NameFarm;
                EditFarm.EditFarmInfo.AdressFarm = FarmList[FileInfoView.SelectedIndex].AdressFarm;

                frame.Content = editor;
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Элемент не выбран", "ОШИБКА");
            }
        }

        private void FindLike(string execcomm) // Обновление вывода
        {
            FarmList.Clear(); // очищаем всё, если там что-то будет
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
                        FarmInfo fileInfo = new FarmInfo();
                        fileInfo.NumberFarm = Convert.ToInt32(reader.GetValue(0));
                        fileInfo.NameFarm = Convert.ToString(reader.GetValue(1));
                        fileInfo.AdressFarm = Convert.ToString(reader.GetValue(2));

                        FileInfoView.ItemsSource = FarmList;
                        FarmList.Add(fileInfo);
                    }

                }

                reader.Close();

            }
        }

        private void AddNew_Btn(object sender, RoutedEventArgs e)
        {
            AddFarm adding = new AddFarm { frame = this.frame }; //Передача в presenter чтобы не был пустым
            frame.Content = adding;

        }

        private void FindCat(object sender, TextChangedEventArgs e)
        {
            FindLike("SELECT * FROM Farmacy WHERE NumberFarm LIKE '" + farmsearch.Text + "%'");
        }

        private void RefreshOrb(object sender, RoutedEventArgs e)
        {
            Farmacy farm = new Farmacy { frame = this.frame }; //Передача в presenter чтобы не был пустым

            frame.Content = farm;
        }

        private void BackToChoice(object sender, RoutedEventArgs e)
        {
            choice ch = new choice { frame = this.frame }; //Возвращаемся на главную страницу

            frame.Content = ch;

        }

        private void Delete(object sender, RoutedEventArgs e)
        {

            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=BDmed;Integrated Security=True";
            string sqlExpression = "DELETE  FROM Farmacy WHERE NumberFarm=" + FarmList[FileInfoView.SelectedIndex].NumberFarm;
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
                    FarmList.RemoveAt(FileInfoView.SelectedIndex);
                }
            }
        }
    }
}
