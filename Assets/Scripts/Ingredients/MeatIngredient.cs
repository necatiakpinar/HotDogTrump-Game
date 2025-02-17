using Abstracts;
using Cysharp.Threading.Tasks;
using Interfaces;
using UnityEngine;

namespace Ingredients
{
    public abstract class MeatIngredient : BaseIngredient, ICookable
    {
        [SerializeField] protected SpriteRenderer _spriteRenderer;

        public abstract UniTask Cook();
    }
}