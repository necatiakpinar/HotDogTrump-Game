using System;
using System.Collections.Generic;
using System.Linq;
using Abstracts;
using Controllers;
using Cysharp.Threading.Tasks;
using Interfaces;
using Misc;
using UnityEngine;

namespace Managers
{
    public class GrillAreaManager : MonoBehaviour
    {
        [SerializeField] private List<IngredientPlacementSlotController> _hotDogMeatPlacementSlots;
        [SerializeField] private List<IngredientPlacementSlotController> _hamburgerMeatPlacementSlots;

        private Dictionary<IngredientType, List<BaseIngredient>> _meats = new();
        private Dictionary<IngredientType, List<IngredientPlacementSlotController>> _ingredientPlacementSlots = new();
        private readonly int _maxFoodAmount = 3;

        private void OnEnable()
        {
            EventManager.OnIngredientResourceCreated += TryToPlaceArea;
        }

        private void OnDestroy()
        {
            EventManager.OnIngredientResourceCreated -= TryToPlaceArea;
        }

        async void Start()
        {
            await Init();
        }

        private async UniTask Init()
        {
            for (int i = 0; i < _maxFoodAmount; i++)
            {
                var hamburger = _hotDogMeatPlacementSlots[i];
                var hotdog = _hamburgerMeatPlacementSlots[i];

                await hamburger.Init(OnIngredientRemoved);
                await hotdog.Init(OnIngredientRemoved);
            }

            _ingredientPlacementSlots[IngredientType.HotDogMeat] = _hotDogMeatPlacementSlots;
            _ingredientPlacementSlots[IngredientType.HamburgerMeat] = _hamburgerMeatPlacementSlots;
            _meats[IngredientType.HotDogMeat] = new List<BaseIngredient>();
            _meats[IngredientType.HamburgerMeat] = new List<BaseIngredient>();
        }

        public void TryToPlaceArea(IngredientType ingredientType)
        {
            if (IngredientTypeHelper.IsMeat(ingredientType))
            {
                var placedMeats = _meats[ingredientType];
                if (placedMeats.Count >= _maxFoodAmount)
                {
                    Debug.LogWarning("Grill area is full!");
                    return;
                }

                var meat = EventManager.OnSpawnIngredientFromPool.Invoke(ingredientType, Vector3.zero, Quaternion.identity, null);
                placedMeats.Add(meat);
                var slot = GetAvailablePlacementSlot(ingredientType);
                if (slot != null)
                {
                    slot.SetIngredient(meat);
                    ICookable meatCookable = meat as ICookable;
                    meatCookable?.Cook();
                }
            }
        }

        private void OnIngredientRemoved(BaseIngredient ingredient)
        {
            _meats[ingredient.IngredientType].Remove(ingredient);
        }

        private IngredientPlacementSlotController GetAvailablePlacementSlot(IngredientType ingredientType)
        {
            return _ingredientPlacementSlots[ingredientType].FirstOrDefault(slot => !slot.IsFull);
        }
    }
}