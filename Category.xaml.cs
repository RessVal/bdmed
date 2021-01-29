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
    public partial class Category : Page
    {
        public Frame frame;

        public class CatInfo
        {
            public int NumberCat { get; set; }
            public string NameCat { get; set; }

        }

        public static ObservableCollection<CatInfo> CatList = new ObservableCollection<CatInfo>();

        public Category()
        {
            InitializeComponent();

            CatList.Clear(); // Очистка окна вывода

            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=BDmed;Integrated Security=True"; // куда подкл

            string sqlExpression = "SELECT * FROM Category"; //Запрос
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
                        CatInfo fileInfo = new CatInfo();
                        fileInfo.NumberCat = Convert.ToInt32(reader.GetValue(0));
                        fileInfo.NameCat = Convert.ToString(reader.GetValue(1));
                        

                        FileInfoView.ItemsSource = CatList;
                        CatList.Add(fileInfo);
                    }
                }

                reader.Close();
            }
        }

        private void Edit(object sender, RoutedEventArgs e) //Кнопка редактора
        {
            try
            {
                EditCat editor = new EditCat { frame = this.frame }; //Передача в presenter чтобы не был пустым

                //editor.edita.Content = Convert.ToString(FileInfoView.SelectedIndex);
                editor.txname.Text = CatList[FileInfoView.SelectedIndex].NameCat;
                


                //editor.edita.Content = Convert.ToString(FileInfoView.SelectedIndex);
                EditCat.index = FileInfoView.SelectedIndex;
                EditCat.EditCatInfo.NumberCat = CatList[FileInfoView.SelectedIndex].NumberCat;
                EditCat.EditCatInfo.NameCat = CatList[FileInfoView.SelectedIndex].NameCat;
                
                frame.Content = editor;
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Элемент не выбран", "ОШИБКА");
            }
        }

        private void FindLike(string execcomm) // Обновление вывода
        {
            CatList.Clear(); // очищаем всё, если там что-то будет
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
                        CatInfo fileInfo = new CatInfo();
                        fileInfo.NumberCat = Convert.ToInt32(reader.GetValue(0));
                        fileInfo.NameCat = Convert.ToString(reader.GetValue(1));
                        

                        FileInfoView.ItemsSource = CatList;
                        CatList.Add(fileInfo);
                    }

                }

                reader.Close();

            }
        }

        private void AddNew_Btn(object sender, RoutedEventArgs e)
        {
            AddCat adding = new AddCat { frame = this.frame }; //Переход на добавление
            frame.Content = adding;

        }

        private void FindCat(object sender, TextChangedEventArgs e)
        {
            FindLike("SELECT * FROM Category WHERE NumberCat LIKE '" + farmsearch.Text + "%'");
        }

        private void RefreshOrb(object sender, RoutedEventArgs e)
        {
           Category cat = new Category { frame = this.frame }; //Передача в presenter чтобы не был пустым

            frame.Content = cat;
        }

        private void BackToChoice(object sender, RoutedEventArgs e)
        {
            choice ch = new choice { frame = this.frame }; //Возвращаемся на главную страницу

            frame.Content = ch;
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            
                string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=BDmed;Integrated Security=True";
            string sqlExpression = "DELETE  FROM Category WHERE NumberCat=" + CatList[FileInfoView.SelectedIndex].NumberCat;
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
                        CatList.RemoveAt(FileInfoView.SelectedIndex);
                    }
                }

               


           
        }
    }
}