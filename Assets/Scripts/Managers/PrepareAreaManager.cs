using System.Collections;
using System.Collections.Generic;
using Abstracts;
using Foods;
using Managers;
using Misc;
using UnityEngine;

public class PrepareAreaManager : MonoBehaviour
{
    private Dictionary<FoodType, List<BaseFood>> _foods = new();

    private readonly int _maxHotDogAmount = 3;
    private readonly int _maxHamburgerAmount = 3;

    void Start()
    {
        var bread = EventManager.OnSpawnFromBreadPool.Invoke(IngredientType.HamburgerBread, Vector3.zero, Quaternion.identity, null);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TryToCreateFood(BaseIngredient bread)
    {
        if (bread.IngredientType == IngredientType.HotDogBread)
        {
            if (!_foods.TryGetValue(FoodType.HotDog, out var hotdogs))
            {
                hotdogs = new List<BaseFood>();
                _foods[FoodType.HotDog] = hotdogs;
            }

            if (hotdogs.Count < _maxHotDogAmount)
            {
                var hotDog = new HotDogFood();
                hotDog.AddIngredient(bread);
                hotdogs.Add(hotDog);
            }
            else
            {
                Debug.LogWarning("Hot dog limit reached!");
            }
        }
        else if (bread.IngredientType == IngredientType.HamburgerBread)
        {
            if (!_foods.TryGetValue(FoodType.Hamburger, out var hamburgers))
            {
                hamburgers = new List<BaseFood>();
                _foods[FoodType.Hamburger] = hamburgers;
            }

            if (hamburgers.Count < _maxHamburgerAmount)
            {
                var hamburger = new HamburgerFood();
                hamburger.AddIngredient(bread);
                hamburgers.Add(hamburger);
            }
            else
            {
                Debug.LogWarning("Hamburger limit reached!");
            }
        }
    }
}