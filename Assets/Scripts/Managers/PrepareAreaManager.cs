using System.Collections.Generic;
using System.Linq;
using Abstracts;
using Controllers;
using Cysharp.Threading.Tasks;
using Foods;
using Misc;
using UnityEngine;

namespace Managers
{
    public class PrepareAreaManager : MonoBehaviour
    {
        [SerializeField] private List<IngredientPlacementSlotController> _hotDogPlacementSlots;
        [SerializeField] private List<IngredientPlacementSlotController> _hamburgerPlacementSlots;

        private Dictionary<FoodType, List<BaseFood>> _foods = new();
        private Dictionary<IngredientType, List<IngredientPlacementSlotController>> _ingredientPlacementSlots = new();

        private readonly Dictionary<IngredientType, FoodType> _ingredientToFoodType = new()
        {
            { IngredientType.HotDogBread, FoodType.HotDog },
            { IngredientType.HamburgerBread, FoodType.Hamburger },
        };

        private readonly Dictionary<IngredientType, BaseFood> _ingredientToFoodObject = new()
        {
            { IngredientType.HotDogBread, new HotDogFood() },
            { IngredientType.HamburgerBread, new HamburgerFood() },
        };

        private readonly int _maxFoodAmount = 3;

        private void OnEnable()
        {
            EventManager.OnIngredientResourceCreated += TryToCreateFood;
        }

        private void OnDestroy()
        {
            EventManager.OnIngredientResourceCreated -= TryToCreateFood;
        }

        async void Start()
        {
            await Init();
        }

        private async UniTask Init()
        {
            for (int i = 0; i < _maxFoodAmount; i++)
            {
                var hamburger = _hamburgerPlacementSlots[i];
                var hotdog = _hotDogPlacementSlots[i];

                await hamburger.Init(OnIngredientRemoved);
                await hotdog.Init(OnIngredientRemoved);
            }
            
            _ingredientPlacementSlots[IngredientType.HotDogBread] = _hotDogPlacementSlots;
            _ingredientPlacementSlots[IngredientType.HamburgerBread] = _hamburgerPlacementSlots;
            await UniTask.CompletedTask;
        }

        public void TryToCreateFood(IngredientType ingredientType)
        {
            if (IngredientTypeHelper.IsBread(ingredientType))
            {
                var foodType = _ingredientToFoodType[ingredientType];
                if (!_foods.TryGetValue(foodType, out var foods))
                {
                    foods = new List<BaseFood>();
                    _foods[foodType] = foods;
                }

                if (foods.Count < _maxFoodAmount)
                {
                    var bread = EventManager.OnSpawnFromPool.Invoke(ingredientType, Vector3.zero, Quaternion.identity, null);
                    var food = _ingredientToFoodObject[ingredientType];
                    food.AddIngredient(bread);
                    foods.Add(food);
                    var availableSlot = GetAvailablePlacementSlot(ingredientType);
                    availableSlot.SetIngredient(bread);
                }
                else
                {
                    Debug.LogWarning("Hot dog limit reached!");
                }
            }
        }

        private void OnIngredientRemoved(BaseIngredient ingredient)
        {
            var ingredientType = ingredient.IngredientType;
            var foodType = _ingredientToFoodType[ingredientType];
            var food = _foods[foodType].FirstOrDefault(f => f.Ingredients.Contains(ingredient));
            if (food == null)
            {
                Debug.LogWarning("Food not found!");
                return;
            }

            food.RemoveIngredient(ingredient);
            _foods[foodType].Remove(food);
        }

        private IngredientPlacementSlotController GetAvailablePlacementSlot(IngredientType ingredientType)
        {
            return _ingredientPlacementSlots[ingredientType].FirstOrDefault(slot => !slot.IsFull);
        }
    }
}