using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для choice.xaml
    /// </summary>
    public partial class choice : Page
    {
        public Frame frame;

        public choice()
        {
            InitializeComponent();
        }

        private void ChoiceList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ChoiceList.SelectedIndex == 0) //первый элемент списка
            {
                Items item = new Items { frame = this.frame }; //Переход на Items
                frame.Content = item;

            }
            if (ChoiceList.SelectedIndex == 1)//второй элемент списка
            {
                Farmacy farm = new Farmacy { frame = this.frame }; //Переход на Farmacy
                frame.Content = farm;
            }
            if (ChoiceList.SelectedIndex == 2)//третий элемент списка
            {
                Category cat = new Category { frame = this.frame }; //Переход на Category
                frame.Content = cat;
            }
            if (ChoiceList.SelectedIndex == 3)//четвёртый элемент списка
            {
                Orders ord = new Orders { frame = this.frame }; //Переход на Orders
                frame.Content = ord;
            }
        }

    }
}
