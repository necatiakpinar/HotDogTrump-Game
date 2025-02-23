using Abstracts;
using Ingredients;
using Misc;
using Pools;
using UnityEngine;

namespace Managers
{
    public class PoolManager : MonoBehaviour
    {
        [SerializeField] private IngredientPool _breadPool;
        [SerializeField] private IngredientPool _meatPool;
        [SerializeField] private FoodPool _foodPool;

        private void OnEnable()
        {
            EventManager.OnSpawnIngredientFromPool += SpawnFromIngredientPool;
            EventManager.OnSpawnFoodFromPool += SpawnFromFoodPool;
            EventManager.OnReturnToPool += ReturnToPool;
        }

        private void OnDisable()
        {
            EventManager.OnSpawnIngredientFromPool -= SpawnFromIngredientPool;
            EventManager.OnSpawnFoodFromPool -= SpawnFromFoodPool;
            EventManager.OnReturnToPool -= ReturnToPool;
        }

        private BaseIngredient SpawnFromIngredientPool(IngredientType ingredientType, Vector3 position, Quaternion rotation, Transform parent)
        {
            if (IngredientTypeHelper.IsBread(ingredientType))
            {
                var bread = _breadPool.SpawnFromPool(ingredientType, position, rotation, parent);
                if (bread == null)
                {
                    Debug.LogError("Bread pool is empty!");
                    return null;
                }

                return bread;
            }
            else if (IngredientTypeHelper.IsMeat(ingredientType))
            {
                var meat = _meatPool.SpawnFromPool(ingredientType, position, rotation, parent);
                if (meat == null)
                {
                    Debug.LogError("Meat pool is empty!");
                    return null;
                }

                return meat;
            }

            return null;
        }
        
        private BaseFood SpawnFromFoodPool(FoodType foodType, Vector3 position, Quaternion rotation, Transform parent)
        {
            var food = _foodPool.SpawnFromPool(foodType, position, rotation, parent);
            if (food == null)
            {
                Debug.LogError("Food pool is empty!");
                return null;
            }

            return food;
        }

        private void ReturnToPool(BaseIngredient ingredient)
        {
            if (ingredient is BreadIngredient bread)
            {
                _breadPool.ReturnToPool(bread.IngredientType, bread);
            }
            else if (ingredient is MeatIngredient meat)
            {
                _meatPool.ReturnToPool(meat.IngredientType, meat);
            }
        }
    }
}