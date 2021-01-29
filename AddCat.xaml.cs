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

namespace MedBd                    //Добавления для CATEGORY
{
    /// <summary>
    /// Логика взаимодействия для AddCat.xaml
    /// </summary>
    public partial class AddCat : Page
    {
        public Frame frame;

        public AddCat()
        {
            InitializeComponent();
        }

        private void Add(object sender, RoutedEventArgs e)
        {

            bool a = true;

            try
            {
                string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=BDmed;Integrated Security=True";
                int uniqueID = 0;
                if (Category.CatList.Count > 0)
                {
                    uniqueID = Category.CatList[Category.CatList.Count - 1].NumberCat + 1;
                }
                string name = txname.Text;
                

                string sqlExpression = "INSERT INTO dbo.Category(NumberCat, NameCat) VALUES (" + uniqueID + ", '" + name + "')";
                MessageBox.Show(sqlExpression);



                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    int number = command.ExecuteNonQuery();
                    MessageBox.Show("Добавлено объектов: " + Convert.ToString(number));
                    Category.CatInfo fileInfo = new Category.CatInfo();
                    fileInfo.NumberCat = uniqueID;
                    fileInfo.NameCat = name;


                    Category.CatList.Add(fileInfo);

                    a = true;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный ввод", "ОШИБКА");
                AddCat back = new AddCat { frame = this.frame }; //Возвращаем
                frame.Content = back;
                a = false;
            }
            catch (SqlException)
            {
                MessageBox.Show("Такого значения нет!", "ОШИБКА");

                AddMed back = new AddMed { frame = this.frame };
                frame.Content = back;
                a = false;
            }

            if (a == true)
            {
                Category boughtback = new Category { frame = this.frame }; //Передача в presenter чтобы не был пустым
                frame.Content = boughtback;
            }

            
        }
    }
}