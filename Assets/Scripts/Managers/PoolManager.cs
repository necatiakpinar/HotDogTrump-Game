using System;
using Ingredients;
using Misc;
using Pools;
using UnityEngine;

namespace Managers
{
    public class PoolManager : MonoBehaviour
    {
        [SerializeField] private BreadPool _breadPool;

        private void OnEnable()
        {
            EventManager.OnSpawnFromBreadPool += SpawnFromBreadPool;
        }
        
        private void OnDisable()
        {
            EventManager.OnSpawnFromBreadPool -= SpawnFromBreadPool;
        }

        private BreadIngredient SpawnFromBreadPool(IngredientType ingredientType, Vector3 position, Quaternion rotation, Transform parent)
        {
            var bread = _breadPool.SpawnFromPool(ingredientType, position, rotation, parent);
            if (bread == null)
            {
                Debug.LogError("Bread pool is empty!");
                return null;
            }

            return bread;
        }
    }
}