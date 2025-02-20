using System;
using Abstracts;
using Cysharp.Threading.Tasks;
using Data.ScriptableObjects;
using Interfaces;
using UnityEngine;

namespace Ingredients
{
    public abstract class MeatIngredient : BaseIngredient, ICookable
    {
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        [SerializeField] protected MeatIngredientDataSo _meatIngredientDataSo;

        public async virtual UniTask Cook()
        {
            _spriteRenderer.color = _meatIngredientDataSo.RawColor;
            await UniTask.Delay(TimeSpan.FromSeconds(_meatIngredientDataSo.RawCookTime));
            _spriteRenderer.color = _meatIngredientDataSo.MediumCookColor;
            await UniTask.Delay(TimeSpan.FromSeconds(_meatIngredientDataSo.MediumCookTime));
            _spriteRenderer.color = _meatIngredientDataSo.ReadyCookColor;
            await UniTask.Delay(TimeSpan.FromSeconds(_meatIngredientDataSo.ReadyCookTime));
            _spriteRenderer.color = _meatIngredientDataSo.BurntColor;
            await UniTask.Delay(TimeSpan.FromSeconds(_meatIngredientDataSo.BurntCookTime));
        }
    }
}