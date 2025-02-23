using System;
using Abstracts;
using Cysharp.Threading.Tasks;
using Interfaces;
using Managers;
using Misc;
using UnityEngine;

namespace Controllers
{
    public class IngredientPlacementSlotController : MonoBehaviour
    {
        private ISlotPlacable _ingredient;
        private Action<ISlotPlacable> _onIngredientRemoved;
        
        public bool IsFull => _ingredient != null;

        public async UniTask Init(Action<ISlotPlacable> onIngredientRemoved)
        {
            _onIngredientRemoved = onIngredientRemoved;
            await UniTask.CompletedTask;
        }
        
        public void SetIngredient(ISlotPlacable ingredient)
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