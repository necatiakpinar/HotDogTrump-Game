﻿using System;
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

        private float _currentCookTime;

        public override void OnSpawn()
        {
            base.OnSpawn();
            _currentCookTime = 0;
            _spriteRenderer.color = _meatIngredientDataSo.RawColor;
            
        }
        public async virtual UniTask Cook()
        {
            _spriteRenderer.color = _meatIngredientDataSo.RawColor;
        }
        
        protected void Update()
        {
            TryToCookMeat();
        }

        private void TryToCookMeat()
        {
            if (_currentCookTime < _meatIngredientDataSo.BurntCookTime)
            {
                if (isDragging)
                    return;
                
                _currentCookTime += Time.deltaTime;
                
                if (_currentCookTime >= _meatIngredientDataSo.RawCookTime && _currentCookTime < _meatIngredientDataSo.MediumCookTime)
                {
                    _spriteRenderer.color = _meatIngredientDataSo.MediumCookColor;
                }
                else if (_currentCookTime >= _meatIngredientDataSo.MediumCookTime && _currentCookTime < _meatIngredientDataSo.ReadyCookTime)
                {
                    _spriteRenderer.color = _meatIngredientDataSo.ReadyCookColor;
                }
                else if (_currentCookTime >= _meatIngredientDataSo.ReadyCookTime && _currentCookTime < _meatIngredientDataSo.BurntCookTime)
                {
                    _spriteRenderer.color = _meatIngredientDataSo.BurntColor;
                }
            }
        }
    }
}