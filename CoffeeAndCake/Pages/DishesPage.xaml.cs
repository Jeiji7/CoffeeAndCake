using CoffeeAndCake.BDSHKA;
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

namespace CoffeeAndCake.Pages
{
    /// <summary>
    /// Логика взаимодействия для DishesPage.xaml
    /// </summary>
    public partial class DishesPage : Page
    {
        public DishesPage()
        {
            InitializeComponent();

            ListServices.ItemsSource = App.BD.Dish.ToList();
            var type = App.BD.Category.ToList();
            type.Insert(0, new BDSHKA.Category() { Id = 0, Name = "Все" });
            SearchCB.ItemsSource = type.ToList();
            SearchCB.DisplayMemberPath = "Name";
            MinTB.Text = (SliderSL.LowerValue).ToString();
            MaxTB.Text = (SliderSL.UpperValue).ToString();

        }

        private void ListServices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.dishes = (Dish)ListServices.SelectedItem;
            Recipe editPage = new Recipe(App.dishes);
            NavigationService.Navigate(editPage);
        }

        private void SliderSL_RangeSelectionChanged(object sender, MahApps.Metro.Controls.RangeSelectionChangedEventArgs<double> e)
        {
            double minPercent = (SliderSL.LowerValue - SliderSL.Minimum) / (SliderSL.Maximum - SliderSL.Minimum) * 100;
            double maxPercent = (SliderSL.UpperValue - SliderSL.Minimum) / (SliderSL.Maximum - SliderSL.Minimum) * 100;

            double minValue = (minPercent * 5000) / 100;
            double maxValue = (maxPercent * 5000) / 100;

            MinTB.Text = Math.Round(minValue).ToString(); // Округляю значение до целого числа
            MaxTB.Text = Math.Round(maxValue).ToString(); // Округляю значение до целого числа

            if (ListServices != null)
            {
                ListServices.ItemsSource = new List<Dish>(App.BD.Dish.Where(p => p.FinalPriceInCents >= SliderSL.LowerValue && p.FinalPriceInCents <= SliderSL.UpperValue).ToList());
            }
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListServices.ItemsSource = new List<Dish>(App.BD.Dish.Where(i => i.Name.StartsWith(SearchTB.Text)));
        }

        private void SearchCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var catygory = SearchCB.SelectedItem as Category;
            ListServices.ItemsSource = new List<Dish>(App.BD.Dish.Where(x => x.CategoryId == catygory.Id).ToList());
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
