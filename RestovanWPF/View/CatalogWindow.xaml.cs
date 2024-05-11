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
    /// Логика взаимодействия для CatalogWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window
    {

        List<Entities.Product> products;

        public CatalogWindow()
        {
            InitializeComponent();
        }

        private void butExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();   
        }

        /// <summary>
        /// П/П получает, фильтрует данные из продуктов
        /// </summary>
        private void ShowProduct()
        {
            //Получить список блюд из БД
            products = App.DB.Product.ToList();     //SELECT * FROM Product
            int countAll = products.Count;          //Кол-во всех

            //Сотировка по цене
            if (cbSort.SelectedIndex == 1)
            {
                products = products.OrderBy(p => p.ProductCost).ToList();
            }
            if (cbSort.SelectedIndex == 2)
            {
                products = products.OrderByDescending(p => p.ProductCost).ToList();
            }
            //Фильтрация по категории
            if (cbCategory.SelectedValue != null && (int)cbCategory.SelectedValue>0)
            {
                products = products.Where(p => p.ProductCategory == (int)cbCategory.SelectedValue).ToList();
            }
            //Поиск по названию
            string search = tbSearch.Text.ToLower();
            //if (search=="")
            if (search.Length>0)
            {
                products = products.Where(p=>p.ProductName.ToLower().Contains(search)).ToList();
            }
            int countFilter = products.Count;          //Кол-во отфильтраванных
            tbCount.Text= countFilter.ToString()+"/"+ countAll.ToString();
            lvProducts.ItemsSource = products;

        }



        /// <summary>
        /// Отобразить в табличной форме каталог блюд
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbSort.SelectedIndex = 0;
            //Заполнение списка категорий
            List<Entities.Category> categories = App.DB.Category.ToList();
            //Создать новый объект - категории
            Entities.Category category = new Entities.Category();
            category.CategoryName = "Все категории";
            category.CategoryId = 0;
            categories.Insert(0,category);

            cbCategory.ItemsSource = categories;        //Отображение всех категорий
            cbCategory.DisplayMemberPath = "CategoryName";  //Видит пользователь
            cbCategory.SelectedValuePath = "CategoryId";    //Выбирать для фильтрации
        }
        /// <summary>
        /// Выбор блюда в списке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show("Название выбранного блюда: "+((Entities.Product)lvProducts.SelectedItem).ProductName);
        }
        /// <summary>
        /// Сортировка по цене
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowProduct();
        }

        /// <summary>
        /// Выбор категории для фильтрации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowProduct();
        }
        /// <summary>
        /// Сброс фильтра по категориям
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butReset_Click(object sender, RoutedEventArgs e)
        {
            ShowProduct();
            cbCategory.SelectedIndex = -1;
        }
        /// <summary>
        /// Строка для поиска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowProduct();
        }
    }
}
