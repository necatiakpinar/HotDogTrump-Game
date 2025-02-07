using System;
using System.Collections.Generic;
using System.Linq;

namespace Misc
{
    public static class IngredientTypeHelper
    {
        private static readonly HashSet<IngredientType> BreadIngredients;
        private static readonly HashSet<IngredientType> MeatIngredients;
        
        static IngredientTypeHelper()
        {
            BreadIngredients = Enum.GetValues(typeof(BreadType))
                .Cast<BreadType>()
                .Select(type => (IngredientType)type)
                .ToHashSet();

            MeatIngredients = Enum.GetValues(typeof(MeatType))
                .Cast<MeatType>()
                .Select(type => (IngredientType)type)
                .ToHashSet();
        }
        
        public static bool IsBread(IngredientType ingredientType) => BreadIngredients.Contains(ingredientType);
        public static bool IsMeat(IngredientType ingredientType) => MeatIngredients.Contains(ingredientType);
    }
}