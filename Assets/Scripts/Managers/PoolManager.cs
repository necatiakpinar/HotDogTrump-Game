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

        private void OnEnable()
        {
            EventManager.OnSpawnFromPool += SpawnFromPool;
            EventManager.OnReturnToPool += ReturnToPool;
        }

        private void OnDisable()
        {
            EventManager.OnSpawnFromPool -= SpawnFromPool;
            EventManager.OnReturnToPool -= ReturnToPool;
        }

        private BaseIngredient SpawnFromPool(IngredientType ingredientType, Vector3 position, Quaternion rotation, Transform parent)
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