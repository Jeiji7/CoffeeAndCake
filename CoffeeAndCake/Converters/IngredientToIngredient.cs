using CoffeeAndCake.BDSHKA;
using CoffeeAndCake.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeAndCake.Converters
{
    public static class IngredientToIngredient
    {
        public static IEnumerable<ExtendedIngredients> ConvertIngredients(Dish dish, int servingsCount)
        {
            List<CookingStage> cookingStages = new List<CookingStage>();
            List<IngredientOfStage> ingredientOfStages = new List<IngredientOfStage>();
            List<Ingredient> ingredients = new List<Ingredient>();
            foreach (CookingStage cookingStage in dish.CookingStage)
            {
                cookingStages.Add(cookingStage);
                foreach (IngredientOfStage ingredientOfStage in cookingStage.IngredientOfStage)
                {
                    ingredientOfStages.Add(ingredientOfStage);
                    if (!ingredients.Contains(ingredientOfStage.Ingredient))
                    {
                        ingredients.Add(ingredientOfStage.Ingredient);
                    }
                }
            }
            return (from cs in dish.CookingStage
                    join ios in ingredientOfStages on cs.Id equals ios.CookingStageId
                    join i in ingredients on ios.IngredientId equals i.Id
                    group ios by (
                        ios.Ingredient,
                        ios.Quantity,
                        i.AvailableCount
                    ) into iq
                    select new ExtendedIngredients(
                        iq.Sum(ingredientOfStage => ingredientOfStage.Quantity
                        * servingsCount) <= iq.Key.AvailableCount,
                        iq.Key.Ingredient.Name,
                        (double)(iq.Sum(ingredientOfStage =>
                        {
                            return ingredientOfStage.Quantity * servingsCount;
                        })),
                        iq.Key.Ingredient.Unit.Name,
                        iq.Key.Ingredient.CostInCents)).ToList();
        }
    }
}