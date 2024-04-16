using CoffeeAndCake.BDSHKA;
using CoffeeAndCake.Converters;
using CoffeeAndCake.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var type = App.BD.Category.ToList();
            type.Insert(0, new BDSHKA.Category() { Id = 0, Name = "Все" });
            SearchCB.ItemsSource = type.ToList();
            SearchCB.DisplayMemberPath = "Name";
            SearchCB.SelectedIndex = 0;
            SliderSL.Minimum = 0;
            SliderSL.Maximum = 5000;
            SliderSL.LowerValue = 0;
            SliderSL.UpperValue = 5000;
            MinTB.Text = (SliderSL.LowerValue).ToString();
            MaxTB.Text = (SliderSL.UpperValue).ToString();
        }

        private void ApplyFilters()
        {
            var query = App.BD.Dish.Where(i =>
                i.Name.StartsWith(SearchTB.Text) &&
                i.FinalPriceInCents <= SliderSL.UpperValue &&
                i.FinalPriceInCents >= SliderSL.LowerValue).ToList();
            if ((bool)GalkaCK.IsChecked == true)
            {
                query = query.Where(x =>
                {
                    return DishConverter.ReadyForCooking(x);
                }).ToList();
            }
            else if ((bool)GalkaCK.IsChecked == false)
            {
            }
            if (SearchCB.SelectedIndex != 0)
            {
                query = query.Where(i => i.CategoryId == SearchCB.SelectedIndex).ToList();
            }

            ListServices.ItemsSource = new List<Dish>(query);
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

            MinTB.Text = Math.Round(minValue).ToString();
            MaxTB.Text = Math.Round(maxValue).ToString();

            ApplyFilters();
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void SearchCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }
    }
}
