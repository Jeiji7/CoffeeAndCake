using CoffeeAndCake.BDSHKA;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
    /// Логика взаимодействия для Ingredients.xaml
    /// </summary>
    public partial class IngredientsPage : Page
    {
        public static double FinalSum;
        public Ingredient ingredientes = new Ingredient();
        public IngredientsPage()
        {
            InitializeComponent();
            SumCounts();
            Ingridientss.ItemsSource = App.BD.Ingredient.Where(x => x.Visible == true).ToList();
        }

        private void Ingredients_DeleteClick(object sender, RoutedEventArgs e)
        {
            var ingredient = (sender as Hyperlink)?.CommandParameter as Ingredient;
            if (ingredient != null)
            {
                ingredient.Visible = false;
                App.BD.SaveChanges();
                Ingridientss.ItemsSource = App.BD.Ingredient.Where(x => x.Visible == true).ToList();
                SumCounts();
            }
        }
        public void SumCounts()
        {
            SumCentsTB.Text = App.BD.Ingredient.Sum(x => x.CostInCents * x.AvailableCount /100).ToString();
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            var ingredient = (sender as Button)?.CommandParameter as Ingredient;
            if (ingredient != null)
            {
                ingredient.AvailableCount++;
                App.BD.SaveChanges();
                Ingridientss.ItemsSource = App.BD.Ingredient.Where(x => x.Visible == true).ToList();
                SumCounts();
            }
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            var ingredient = (sender as Button)?.CommandParameter as Ingredient;
            if (ingredient != null && ingredient.AvailableCount > 0)
            {
                ingredient.AvailableCount--;
                App.BD.SaveChanges();
                Ingridientss.ItemsSource = App.BD.Ingredient.Where(x => x.Visible == true).ToList();
                SumCounts();
            }
        }

    }
}
