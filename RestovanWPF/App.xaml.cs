using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RestovanWPF
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        //Данные для авторизации админитсратора
        public static string Login = "admin";
        public static string Password = "admin";
        //Объявление объекта подключения к БД
        public static Entities.DBRestaurant DB;
    }
}
