using System;
using System.Collections.Generic;
using Interfaces;
using Misc;
using UnityEngine;

namespace Abstracts
{
    public class BasePool<T> : MonoBehaviour where T : MonoBehaviour, IPoolable<T>
    {
        [SerializeField] private List<PoolObject<T>> _poolObjects;
        
        private Dictionary<IngredientType, Queue<T>> _poolDictionary;
        private Dictionary<IngredientType, T> _prefabDictionary;

        private void OnEnable()
        {
            _poolDictionary = new Dictionary<IngredientType, Queue<T>>();
            _prefabDictionary = new Dictionary<IngredientType, T>();

            foreach (var poolObject in _poolObjects)
            {
                _poolDictionary[poolObject.IngredientType] = new Queue<T>();
                _prefabDictionary[poolObject.IngredientType] = poolObject.ObjectPf;
            }
        }
        
        public T SpawnFromPool(IngredientType ingredientType, Vector3 position, Quaternion rotation = default, Transform parent = null)
        {
            if (!_poolDictionary.TryGetValue(ingredientType, out var objectQueue))
            {
                Debug.LogWarning($"No pool with ID {ingredientType} found!");
                return null;
            }

            T objectToSpawn;

            if (objectQueue.Count == 0)
            {
                if (!_prefabDictionary.TryGetValue(ingredientType, out var prefab))
                {
                    Debug.LogWarning($"No prefab found for ingredient type {ingredientType}!");
                    return null;
                }

                objectToSpawn = Instantiate(prefab);
                objectToSpawn.gameObject.SetActive(false);
            }
            else
            {
                objectToSpawn = objectQueue.Dequeue();
            }

            if (parent != null)
                objectToSpawn.transform.SetParent(parent);
            
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
            objectToSpawn.gameObject.SetActive(true);
            
            objectToSpawn.OnSpawn();
            
            return objectToSpawn;
        }

        public void ReturnToPool(IngredientType ingredientType, T poolObject)
        {
            if (!_poolDictionary.ContainsKey(ingredientType))
            {
                Debug.LogWarning($"No pool with ID {ingredientType} found!");
                return;
            }
            
            poolObject.gameObject.SetActive(false);
            _poolDictionary[ingredientType].Enqueue(poolObject);
            
        }

    }
}