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

namespace CoffeeAndCake
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.NavigationService.Navigate(new Pages.DishesPage());
        }

        private void Dishes(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Pages.DishesPage());
        }
        private void Ingredients(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Pages.IngredientsPage());
        }
        private void Orders(object sender, RoutedEventArgs e)
        {
            // Ваш код обработки события здесь
        }

    }
}
