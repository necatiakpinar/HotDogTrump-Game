using System.Collections.Generic;
using System.Linq;
using Abstracts;
using Controllers;
using Cysharp.Threading.Tasks;
using Foods;
using Managers;
using Misc;
using UnityEngine;

public class PrepareAreaManager : MonoBehaviour
{
    [SerializeField] private List<IngredientPlacementSlotController> _hotDogPlacementSlots;
    [SerializeField] private List<IngredientPlacementSlotController> _hamburgerPlacementSlots;
    
    private Dictionary<FoodType, List<BaseFood>> _foods = new();

    private readonly int _maxHotDogAmount = 3;
    private readonly int _maxHamburgerAmount = 3;

    private void OnEnable()
    {
        EventManager.OnIngredientResourceCreated += TryToCreateFood;
    }

    private void OnDestroy()
    {
        EventManager.OnIngredientResourceCreated -= TryToCreateFood;
    }

    async void  Start()
    {
        await Init();
    }

    private async UniTask Init()
    {
        
    }
    
    public void TryToCreateFood(IngredientType ingredientType)
    {
        //
        if (ingredientType == IngredientType.HotDogBread)
        {
            if (!_foods.TryGetValue(FoodType.HotDog, out var hotdogs))
            {
                hotdogs = new List<BaseFood>();
                _foods[FoodType.HotDog] = hotdogs;
            }

            if (hotdogs.Count < _maxHotDogAmount)
            {
                var bread = EventManager.OnSpawnFromBreadPool.Invoke(IngredientType.HotDogBread, Vector3.zero, Quaternion.identity, null);
                var hotDog = new HotDogFood();
                hotDog.AddIngredient(bread);
                hotdogs.Add(hotDog);
                var availableSlot = GetAvailableHotDogBreadPlacementSlot();
                availableSlot.SetIngredient(bread);
            }
            else
            {
                Debug.LogWarning("Hot dog limit reached!");
            }
        }
        else if (ingredientType == IngredientType.HamburgerBread)
        {
            if (!_foods.TryGetValue(FoodType.Hamburger, out var hamburgers))
            {
                hamburgers = new List<BaseFood>();
                _foods[FoodType.Hamburger] = hamburgers;
            }

            if (hamburgers.Count < _maxHamburgerAmount)
            {
                var bread = EventManager.OnSpawnFromBreadPool.Invoke(IngredientType.HamburgerBread, Vector3.zero, Quaternion.identity, null);
                var hamburger = new HamburgerFood();
                hamburger.AddIngredient(bread);
                hamburgers.Add(hamburger);
                var availableSlot = GetAvailableHamburgerBreadPlacementSlot();
                availableSlot.SetIngredient(bread);
            }
            else
            {
                Debug.LogWarning("Hamburger limit reached!");
            }
        }
    }


    private IngredientPlacementSlotController GetAvailableHotDogBreadPlacementSlot()
    {
        return _hotDogPlacementSlots.FirstOrDefault(slot => !slot.IsFull);
    }
    
    private IngredientPlacementSlotController GetAvailableHamburgerBreadPlacementSlot()
    {
        return _hamburgerPlacementSlots.FirstOrDefault(slot => !slot.IsFull);
    }
}