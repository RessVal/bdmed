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

namespace MedBd                    //Добавления для ITEMS
{
    /// <summary>
    /// Логика взаимодействия для AddMed.xaml
    /// </summary>
    public partial class AddMed : Page
    {
        public Frame frame;

        public AddMed()
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
                if (Items.ItemsList.Count > 0)
                {
                    uniqueID = Items.ItemsList[Items.ItemsList.Count - 1].NumberItems + 1;
                }
                string name = txnameitems.Text;
                string dis = txDis.Text;
                int cat = Convert.ToInt32(txcat.Text);
                int farm = Convert.ToInt32(txfarm.Text);
                
                string sqlExpression = "INSERT INTO dbo.Items(NumberItems, NameItems, Discription, NumberCat , NumberFarm) VALUES (" + uniqueID + ", '" + name + "', '" + dis + "', '" + cat + "', '" + farm + "')";
                MessageBox.Show(sqlExpression);


                
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                    
                            connection.Open();
                            SqlCommand command = new SqlCommand(sqlExpression, connection);
                            int number = command.ExecuteNonQuery();
                            MessageBox.Show("Добавлено объектов: " + Convert.ToString(number));
                            Items.ItemsInfo fileInfo = new Items.ItemsInfo();
                            fileInfo.NumberItems = uniqueID;
                            fileInfo.NameItems = name;
                            fileInfo.Discription = dis;
                        
                            fileInfo.NumberCat = Convert.ToInt32(cat);
                            fileInfo.NumberFarm = Convert.ToInt32(farm);
                        
                            Items.ItemsList.Add(fileInfo);

                            a = true;
                    }

                

            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный ввод", "ОШИБКА");

                AddMed back = new AddMed { frame = this.frame }; //Возвращаем
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

            if(a == true)
            {
                Items boughtback = new Items { frame = this.frame }; //Передача в presenter чтобы не был пустым
                frame.Content = boughtback;
            }
            
        }

        
    }
}
