﻿using System;
using Abstracts;
using Cysharp.Threading.Tasks;
using Data.ScriptableObjects;
using Interfaces;
using Misc;
using UnityEngine;

namespace Ingredients
{
    public abstract class MeatIngredient : BaseIngredient, ICookable
    {
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        [SerializeField] protected MeatIngredientDataSo _meatIngredientDataSo;

        private float _currentCookTime;
        protected CookableIngredientStateType cookingState;
        public CookableIngredientStateType CookingState => cookingState;

        public override void OnSpawn()
        {
            base.OnSpawn();
            _currentCookTime = 0;
            _spriteRenderer.color = _meatIngredientDataSo.RawColor;
            cookingState = CookableIngredientStateType.Raw;
        }

        public async virtual UniTask Cook()
        {
            _spriteRenderer.color = _meatIngredientDataSo.RawColor;
        }

        protected override void TryToPlaceInFood()
        {
            base.TryToPlaceInFood();
        }

        protected void Update()
        {
            TryToCookMeat();
        }

        private void TryToCookMeat()
        {
            if (_currentCookTime < _meatIngredientDataSo.BurntCookTime)
            {
                if (isDragging || cookingState == CookableIngredientStateType.Cooked)
                    return;

                _currentCookTime += Time.deltaTime;

                if (_currentCookTime >= _meatIngredientDataSo.RawCookTime && _currentCookTime < _meatIngredientDataSo.MediumCookTime)
                {
                    _spriteRenderer.color = _meatIngredientDataSo.MediumCookColor;
                    CookableIngredientStateType cookingState = CookableIngredientStateType.MediumCooked;
                }
                else if (_currentCookTime >= _meatIngredientDataSo.MediumCookTime && _currentCookTime < _meatIngredientDataSo.ReadyCookTime)
                {
                    _spriteRenderer.color = _meatIngredientDataSo.ReadyCookColor;
                    cookingState = CookableIngredientStateType.Cooked;
                }
                else if (_currentCookTime >= _meatIngredientDataSo.ReadyCookTime && _currentCookTime < _meatIngredientDataSo.BurntCookTime)
                {
                    _spriteRenderer.color = _meatIngredientDataSo.BurntColor;
                    cookingState = CookableIngredientStateType.Burned;
                }
            }
            else
            {
                cookingState = CookableIngredientStateType.Burned;
            }
        }
    }
}