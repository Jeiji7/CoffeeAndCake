using CoffeeAndCake.BDSHKA;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CoffeeAndCake.Converters
{
    public class ConverterToGray : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Dish dish = value as Dish;
            bool isEnoughIngredients = DishConverter.ReadyForCooking(dish);
            return isEnoughIngredients ? PixelFormats.Pbgra32 : PixelFormats.Gray8;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}