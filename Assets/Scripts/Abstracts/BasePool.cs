using System;
using System.Collections.Generic;
using Interfaces;
using Misc;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

namespace Abstracts
{
    public class BasePool<T,TK> : MonoBehaviour where T : MonoBehaviour, IPoolable<T> where TK: Enum
    {
        [SerializeField] private List<PoolObject<T,TK>> _poolObjects;

        private Dictionary<TK, Queue<T>> _poolDictionary;
        private Dictionary<TK, T> _prefabDictionary;

        private void OnEnable()
        {
            _poolDictionary = new Dictionary<TK, Queue<T>>();
            _prefabDictionary = new Dictionary<TK, T>();

            foreach (var poolObject in _poolObjects)
            {
                _poolDictionary[poolObject.ObjectType] = new Queue<T>();
                _prefabDictionary[poolObject.ObjectType] = poolObject.ObjectPf;
            }
            
            Init();
        }

        private void Init()
        {
            for (int i = 0; i < _poolObjects.Count; i++)
            {
                var poolObject = _poolObjects[i];
                _poolDictionary[poolObject.ObjectType] = new Queue<T>();
                var prefab = poolObject.ObjectPf;
                for (int j = 0; j < poolObject.Size; j++)
                {
                    var createdPoolObject = Instantiate(prefab, transform);
                    _poolDictionary[poolObject.ObjectType].Enqueue(createdPoolObject);
                    createdPoolObject.gameObject.SetActive(false);
                }
            }
        }

        public T SpawnFromPool(TK ingredientType, Vector3 position, Quaternion rotation = default, Transform parent = null)
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

        public void ReturnToPool(TK ingredientType, T poolObject)
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