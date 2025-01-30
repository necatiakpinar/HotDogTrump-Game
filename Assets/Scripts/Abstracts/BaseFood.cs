using System.Collections.Generic;
using Misc;

namespace Abstracts
{
    public abstract class BaseFood
    {
        protected List<BaseIngredient> _ingredients = new List<BaseIngredient>();

        public void AddIngredient(BaseIngredient ingredient)
        {
            _ingredients.Add(ingredient);
        }

        public void RemoveIngredient(BaseIngredient ingredient)
        {
            _ingredients.Remove(ingredient);
        }

        public List<BaseIngredient> GetIngredients()
        {
            return _ingredients;
        }

        public bool HasIngredients(IngredientType ingredientType)
        {
            return _ingredients.Exists(ingredient => ingredient.IngredientType == ingredientType);
        }
    }
}