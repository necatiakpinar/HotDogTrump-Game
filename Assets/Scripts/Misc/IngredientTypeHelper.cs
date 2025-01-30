using System;
using System.Collections.Generic;
using System.Linq;

namespace Misc
{
    public static class IngredientTypeHelper
    {
        private static readonly HashSet<IngredientType> BreadIngredients;
        
        static IngredientTypeHelper()
        {
            BreadIngredients = Enum.GetValues(typeof(BreadType))
                .Cast<BreadType>()
                .Select(type => (IngredientType)type)
                .ToHashSet();
        }
        
        public static bool IsBread(IngredientType ingredientType) => BreadIngredients.Contains(ingredientType);
    }
}