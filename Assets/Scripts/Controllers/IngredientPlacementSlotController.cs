using Abstracts;
using UnityEngine;

namespace Controllers
{
    public class IngredientPlacementSlotController : MonoBehaviour
    {
        private BaseIngredient _ingredient;
        
        public bool IsFull => _ingredient != null;
        
        public void SetIngredient(BaseIngredient ingredient)
        {
            _ingredient = ingredient;
            _ingredient.transform.SetParent(transform);
            _ingredient.transform.localPosition = Vector3.zero;
        }
        
        public void RemoveIngredient()
        {
            _ingredient = null;
        }
    }
}