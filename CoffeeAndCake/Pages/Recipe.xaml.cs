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
using CoffeeAndCake.BDSHKA;
using CoffeeAndCake.Properties;
namespace CoffeeAndCake.Pages
{
    /// <summary>
    /// Логика взаимодействия для Recipe.xaml
    /// </summary>
    public partial class Recipe : Page
    {
        public int servings = App.dishes.BaseServingsQuantity;
        public Recipe(Dish dishh)
        {
            InitializeComponent();
            dishh = App.dishes;
            Listik();
            CountsPortionTB.Text = Convert.ToInt32(servings).ToString(); 
            InstructionsLV.ItemsSource = App.BD.CookingStage.Where(x => x.DishId == dishh.Id).ToList();
            SumCounts();// считает цену за 1 порцию
            CategoryTB.Text = dishh.Category.Name.ToString();
            NamesTB.Text = dishh.Name.ToString();
            SumMin();//считает минуты
        }
        public void Listik()
        {
            IngidientsLV.ItemsSource = App.BD.IngredientOfStage.Where(x => x.CookingStage.DishId == App.dishes.Id).GroupBy(x => x.Ingredient.Name)
            .Select(g => new
            {
                Name = g.Key,
                Visible = g.Sum(x => x.Quantity * servings)  <= g.Select(x => x.Ingredient.AvailableCount).FirstOrDefault(),
                TotalQuantity = g.Sum(x => x.Quantity * servings),
                LastPrise = g.Sum(x => x.Ingredient.CostInCents * x.Quantity * servings),
                UnitName = g.Select(x => x.Ingredient.Unit.Name).FirstOrDefault(),
                Ingredients = g.ToList()
            }).ToList();
        }
        public void SumCounts()
        {
            PriseTB.Text = App.BD.IngredientOfStage.Where(x => x.CookingStage.DishId == App.dishes.Id).Sum(x => x.Quantity * x.Ingredient.CostInCents * servings).ToString();
        }
        public void SumMin()
        {
            CookingTimeTB.Text = App.BD.CookingStage.Where(x => x.DishId == App.dishes.Id).Sum(x => x.TimeInMinutes).ToString();
        }
        private void DecrementServings(object sender, RoutedEventArgs e)
        {
            servings--;
            CountsPortionTB.Text = Convert.ToInt32(servings).ToString();
            Listik();
            SumCounts();
        }

        private void IncrementServings(object sender, RoutedEventArgs e)
        {
            servings++;
            CountsPortionTB.Text = Convert.ToInt32(servings).ToString();
            Listik();
            SumCounts();
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            App.BD.SaveChanges();
            NavigationService.Navigate(new Pages.DishesPage());
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.BD.SaveChanges();
        }
    }
}
