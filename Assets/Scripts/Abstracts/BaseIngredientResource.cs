using Misc;
using UnityEngine;

namespace Abstracts
{
    public abstract class BaseIngredientResource : MonoBehaviour
    {
        [SerializeField] private IngredientResourceType _ingredientResourceType;
        
        public IngredientResourceType IngredientResourceType => _ingredientResourceType;
    }
}