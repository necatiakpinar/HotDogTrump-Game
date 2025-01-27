using System;
using Interfaces;
using UnityEngine;

namespace Misc
{
    [Serializable]
    public class PoolObject<T> where T: IPoolable<T>
    {
        [SerializeField] private IngredientType _ingredientType;
        [SerializeField] private T _objectPf;
        [SerializeField] private int _size;
        
        public IngredientType IngredientType => _ingredientType;
        public T ObjectPf => _objectPf;
        public int Size => _size;
    }
}