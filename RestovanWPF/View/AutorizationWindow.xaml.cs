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
    /// Логика взаимодействия для AutorizationWindow.xaml
    /// </summary>
    public partial class AutorizationWindow : Window
    {
        int countTry;
        public AutorizationWindow()
        {
            InitializeComponent();
            countTry = 3;
        }
        /// <summary>
        /// Вход как администратор после ввода логина и пароля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butInput_Click(object sender, RoutedEventArgs e)
        {
            string login = tbLogin.Text;
            string password = twPassword.Password;
            //Получить всех пользователей
            //var  users = App.DB.User.ToList();
            List<Entities.User>  users = App.DB.User.ToList();
            List < Entities.User> userFilter = users.Where(u => u.UserLogin == login && u.UserPassword == password).ToList();
            Entities.User user = userFilter.FirstOrDefault();
            if (user!=null)
            //if (login == App.Login && password == App.Password)
            {
                MessageBox.Show("Вы успешно зашли как администратор ресторана");
                new View.WorkCatalogWindow().ShowDialog();
                this.Close();
            }
            else
            {
                countTry--;
                if (countTry == 0)
                {
                    MessageBox.Show("Все попытки исчерпаны");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неудачная попытка входа.\r\n У Вас осталось " + countTry + " попыток.");
                }
            }
        }

        private void butExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
