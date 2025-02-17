using System;
using Abstracts;
using Cysharp.Threading.Tasks;
using Managers;
using Misc;
using UnityEngine;

namespace Controllers
{
    public class IngredientPlacementSlotController : MonoBehaviour
    {
        private BaseIngredient _ingredient;
        private Action<BaseIngredient> _onIngredientRemoved;
        private FoodType _foodType;
        
        public bool IsFull => _ingredient != null;

        public async UniTask Init(Action<BaseIngredient> onIngredientRemoved)
        {
            _onIngredientRemoved = onIngredientRemoved;
            await UniTask.CompletedTask;
        }
        
        public void SetIngredient(BaseIngredient ingredient)
        {
            _ingredient = ingredient;
            _ingredient.PlaceToSlot(this);
            
        }
        
        public void RemoveIngredient()
        {
            _onIngredientRemoved?.Invoke(_ingredient);
            _ingredient = null;
        }
    }
}