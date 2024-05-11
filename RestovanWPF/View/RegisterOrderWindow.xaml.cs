using RestovanWPF.Classes;
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
using System.Windows.Shapes;

namespace RestovanWPF.View
{
    /// <summary>
    /// Логика взаимодействия для RegisterOrderWindow.xaml
    /// </summary>
    public partial class RegisterOrderWindow : Window
    {
        CreateOrderWindow createOrder;
        List <Classes.ProductInOrder> productsInOrder;  //Список заказанных блюд

        public RegisterOrderWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Переход к клавному меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowOrder()
        {
            dgOrder.ItemsSource = null;
            dgOrder.ItemsSource = productsInOrder;
            double summaOrder = productsInOrder.Sum(p => p.ProductTotalCost);
            tbOrder.Text = "Сумма заказа " + Math.Round(summaOrder, 2);
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            createOrder = this.Owner as CreateOrderWindow;
            productsInOrder = createOrder.productInOrders;
            ShowOrder();
        }

        private void butAdd_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Classes.ProductInOrder selectDish = button.DataContext as Classes.ProductInOrder;
            switch(button.Content)
            {
                case "+":
                    selectDish.ProductCount++;
                    selectDish.ProductTotalCost += selectDish.ProductCost;
                    break;
                case "-":
                    selectDish.ProductCount--;
                    selectDish.ProductTotalCost -= selectDish.ProductCost;
                    break;

            }
            //selectDish.ProductCount++;
            //selectDish.ProductTotalCost += selectDish.ProductCost;
            ShowOrder();
        }

        private void butDel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
