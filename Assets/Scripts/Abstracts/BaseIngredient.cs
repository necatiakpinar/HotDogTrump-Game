using Misc;
using UnityEngine;

namespace Abstracts
{
    public abstract class BaseIngredient : MonoBehaviour
    {
        [SerializeField] private IngredientType _ingredientType;
        
        public IngredientType IngredientType => _ingredientType;
    }
}
