using System.Collections.Generic;
using Interfaces;
using Misc;
using UnityEngine;

namespace Abstracts
{
    public abstract class BaseFood : MonoBehaviour, IPoolable<BaseFood>
    {
        protected List<BaseIngredient> _ingredients = new List<BaseIngredient>();
        
        public List<BaseIngredient> Ingredients => _ingredients;

        public void AddIngredient(BaseIngredient ingredient)
        {
            _ingredients.Add(ingredient);
        }

        public void RemoveIngredient(BaseIngredient ingredient)
        {
            _ingredients.Remove(ingredient);
        }

        public bool HasIngredients(IngredientType ingredientType)
        {
            return _ingredients.Exists(ingredient => ingredient.IngredientType == ingredientType);
        }
        public void OnSpawn()
        {
        }
        public void ReturnToPool(BaseFood poolObject)
        {
        }
    }
}