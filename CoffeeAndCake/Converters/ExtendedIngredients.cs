using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeAndCake.Converters
{
    public class ExtendedIngredients
    {
        public bool IsAvailable { get; }
        public string Name { get; }
        public double Quantity { get; }
        public string Unit { get; }
        public double Cost { get; set; }

        public ExtendedIngredients(bool isAvailable, string name, double quantity, string unit, double cost)
        {
            IsAvailable = isAvailable;
            Name = name;
            Quantity = quantity < 0 ? 0 : quantity;
            Unit = unit;
            Cost = cost;
        }
    }
}