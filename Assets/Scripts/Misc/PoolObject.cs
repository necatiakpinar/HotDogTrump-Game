using System;
using Interfaces;
using UnityEngine;

namespace Misc
{
    [Serializable]
    public class PoolObject<T,TK> where T: IPoolable<T> where TK: Enum
    {
        [SerializeField] private TK _objectType;
        [SerializeField] private T _objectPf;
        [SerializeField] private int _size;
        
        public TK ObjectType => _objectType;
        public T ObjectPf => _objectPf;
        public int Size => _size;
    }
}