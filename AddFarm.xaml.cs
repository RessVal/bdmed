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

namespace MedBd                    //Добавления для FARMACY
{
    /// <summary>
    /// Логика взаимодействия для AddMed.xaml
    /// </summary>
    public partial class AddFarm : Page
    {
        public Frame frame;

        public AddFarm()
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
                if (Farmacy.FarmList.Count > 0)
                {
                    uniqueID = Farmacy.FarmList[Farmacy.FarmList.Count - 1].NumberFarm + 1;
                }
                string name = txname.Text;
                string ad = txadress.Text;
                

                string sqlExpression = "INSERT INTO dbo.Farmacy(NumberFarm, NameFarm, AdressFarm) VALUES (" + uniqueID + ", '" + name + "', '" + ad + "')";
                MessageBox.Show(sqlExpression);



                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    int number = command.ExecuteNonQuery();
                    MessageBox.Show("Добавлено объектов: " + Convert.ToString(number));
                    Farmacy.FarmInfo fileInfo = new Farmacy.FarmInfo();
                    fileInfo.NumberFarm = uniqueID;
                    fileInfo.NameFarm = name;
                    fileInfo.AdressFarm = ad;



                    Farmacy.FarmList.Add(fileInfo);

                    a = true;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный ввод", "ОШИБКА");
                AddFarm back = new AddFarm { frame = this.frame }; //Возвращаем
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
                Farmacy boughtback = new Farmacy { frame = this.frame }; //Передача в presenter чтобы не был пустым
                frame.Content = boughtback;
            }

            
        }
    }
}