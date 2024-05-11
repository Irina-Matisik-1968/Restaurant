using RestovanWPF.Entities;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для CreateOrderWindow.xaml
    /// </summary>
    public partial class CreateOrderWindow : Window
    {
        Random rand = new Random();
        public double SummaBankCard { get; set; }   //Сумма на карте
        public double SummaOrder { get; set; }      //Сумма заказа

        string pathPhoto = Environment.CurrentDirectory + "\\Images\\";	//Путь к папке с картинками
        
        public List<Classes.ProductInOrder> productInOrders = new List<Classes.ProductInOrder>();


        public CreateOrderWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Конструктор с параметром - сумма на счете
        /// </summary>
        /// <param name="summaBankCard"></param>
        public CreateOrderWindow(double summaBankCard)
        {
            InitializeComponent();
            this.SummaBankCard = summaBankCard;     //Сумма на карте
            tbSumma.Text = "Сумма на карте " + Math.Round(SummaBankCard, 2);
            SummaOrder = 0;
            tbSummaOrder.Text = "Сумма заказа " + Math.Round(SummaOrder, 2);
            //Заполнение списка категорий
            List<Entities.Category> categories = App.DB.Category.ToList();
            //Создать новый объект - категории
            Entities.Category category = new Entities.Category();
            category.CategoryName = "Все категории";
            category.CategoryId = 0;
            categories.Insert(0, category);
            lbCategory.ItemsSource = categories;        //Отображение всех категорий
            lbCategory.DisplayMemberPath = "CategoryName";  //Видит пользователь
            lbCategory.SelectedValuePath = "CategoryId";    //Выбирать для фильтрации
            lbCategory.SelectedIndex = 0;
            ShowDishes();
        }

        /// <summary>
        /// Возврат в окно с главным меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Переход в окно Оформить заказ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butRegister_Click(object sender, RoutedEventArgs e)
        {
            SummaOrder = 1 + rand.NextDouble() * SummaBankCard;     //Сумма заказа
            this.Hide();
            View.RegisterOrderWindow register = new View.RegisterOrderWindow();
            register.Owner = this;          //У этой формы владелец текущая
            register.ShowDialog();
            this.Close();
        }

        //Метод отображения всей информации с учетом фильтрации, поиска и фото
        private void ShowDishes()
        {
            //Получение всех продуктов всегда обновленными из БД
            List<Entities.Product> products = null;
            //using (App.DB = new DBRestaurant())
            //using (Entities.DBRestaurant DBTemp = new DBRestaurant())
            {
                //Получить список блюд из БД
                //List<Entities.Product> products = null;
                products = App.DB.Product.ToList();     //SELECT * FROM Product
                //List<Entities.Product> products = DBTemp.Product.ToList();     //SELECT * FROM Product
                //products = null;
                //products = DBTemp.Product.ToList();     //SELECT * FROM Product
                //Фильтрация по категории
                if (lbCategory.SelectedValue != null && (int)lbCategory.SelectedValue > 0)
                {
                    products = products.Where(p => p.ProductCategory == (int)lbCategory.SelectedValue).ToList();
                }
                //List<Entities.Product> products = App.DB.Product.ToList();
                ////Наложение фильтрации
                //if (lbCategory.SelectedIndex > 0)
                //{
                //    products = products.Where(pr => pr.ProductCategory == lbCategory.SelectedIndex).ToList();
                //}

                //Отображение фото товара по строке
                //Изменение поля фото списка товаров относительно пути только в кэш, но не в БД
                string photo = "";
                //Перебор всех товаров
                foreach (Entities.Product product in products)
                {
                    //Получение фото из БД или заглушка
                    if (String.IsNullOrEmpty(product.ProductPhotoString))	//Пусто поле с фото
                    {
                        photo = "pack://application:,,,/Resources/mid.png";		//Заглушка
                    }
                    else
                    {
                        photo = pathPhoto + product.ProductPhotoString;		//Полный путь к файлу с фото
                        if (!File.Exists(photo))                            //Проверка существования фото
                            photo = "pack://application:,,,/Resources/mid.png"; 	//Заглушка
                    }
                    //Изменение поля фото с учетом пути в списке
                    //if (!product.ProductPhotoString.Contains(pathPhoto) || (String.IsNullOrEmpty(product.ProductPhotoString)))
                    if (!product.ProductPhotoString.Contains(pathPhoto) )
                        product.ProductPhotoString = photo;
                }
                //Отображение списка со всеми изменениями
                lbDish.ItemsSource = products;
            }
        }

        /// <summary>
        /// Выбор категории для отображения блюд
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowDishes();
        }
        /// <summary>
        ///  Выбор блюда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butInOrder_Click(object sender, RoutedEventArgs e)
        {
            //Информация о выбранном блюде
            Entities.Product selectProduct = (sender as Button).DataContext as Entities.Product;
            Classes.ProductInOrder productInOrder;
            //Поиск выбранного блюда в заказе по его названию
            int index = productInOrders.FindIndex(p => p.ProductName == selectProduct.ProductName);
            if (index == -1)        //Такого блюда еще не было в заказе
            {
                //Блюдо заказывается первый раз
                productInOrder = new Classes.ProductInOrder();
                productInOrder.ProductName = selectProduct.ProductName;
                productInOrder.ProductCost = selectProduct.ProductCost;
                productInOrder.ProductCount = 1;
                productInOrder.ProductTotalCost = productInOrder.ProductCost;
                productInOrders.Add(productInOrder);
            }
            else                     
            {
                //Блюдо уже было в заказе
                productInOrder = productInOrders[index];
                productInOrder.ProductCount++;
                productInOrder.ProductTotalCost = productInOrder.ProductCost * productInOrder.ProductCount;
            }
            //Общая сумма заказа
            SummaOrder += productInOrder.ProductCost;
            tbSummaOrder.Text = "Сумма заказа " + SummaOrder;
        }
    }
}
