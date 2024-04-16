using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CoffeeAndCake.BDSHKA;

namespace CoffeeAndCake
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static NyamNyam_Session2Entities BD = new NyamNyam_Session2Entities();
        public static Dish dishes;
    }
}
