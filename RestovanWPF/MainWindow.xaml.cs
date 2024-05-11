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
using System.IO;


namespace RestovanWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rand = new Random();
        double summaBankCard;
        string password;
        public MainWindow()
        {
            InitializeComponent();
            App.DB = new Entities.DBRestaurant();   //Связь с БД
        }
        /// <summary>
        /// Вывод прайс-листа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butPriceList_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Прайс-лист в разработке");
            View.CatalogWindow catalogWindow = new View.CatalogWindow();
            this.Hide();
            catalogWindow.ShowDialog();
            this.ShowDialog();
        }
        /// <summary>
        /// Сделать заказ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butOrder_Click(object sender, RoutedEventArgs e)
        {
            summaBankCard = 1000 + rand.NextDouble()*10000;
            MessageBox.Show("Мы заглянули на Вашу карту.\n На ней сумма: " + Math.Round(summaBankCard,2));
            //Создание окна конструктором с параметром - сумма на карте
            View.CreateOrderWindow createOrderWindow = new View.CreateOrderWindow(summaBankCard);
            this.Hide();
            createOrderWindow.ShowDialog();
            this.ShowDialog();
        }
        /// <summary>
        /// Изменения каталога
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butManagment_Click(object sender, RoutedEventArgs e)
        {
            //StreamReader reader = new StreamReader("Key.txt");
            //password = reader.ReadLine();
            //reader.Close();
            //MessageBox.Show("Для редактирования каталога товаров требуется ввод пароля "+ password);
            this.Hide();
            new View.AutorizationWindow().ShowDialog();
            this.ShowDialog();
        }
        /// <summary>
        /// Завершить приложение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
