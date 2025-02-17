using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Ingredients.Meats
{
    public class HamburgerMeat : MeatIngredient
    {
        private readonly Color _rawColor = new Color(1f, 0.4f, 0.4f);
        private readonly Color _mediumColor = new Color(0.8f, 0.2f, 0.1f);
        private readonly Color _readyColor = new Color(0.4f, 0.15f, 0.1f);
        private readonly Color _burntColor = new Color(0.2f, 0.15f, 0.1f);
        
        private readonly float _rawCookTime = 3f;
        private readonly float _mediumCookTime = 5f;
        private readonly float _readyCookTime = 7f;
        private readonly float _burntCookTime = 9f;
        
        public async override UniTask Cook()
        {
            _spriteRenderer.color = _rawColor;
            await UniTask.Delay(TimeSpan.FromSeconds(_rawCookTime));
            _spriteRenderer.color = _mediumColor;
            await UniTask.Delay(TimeSpan.FromSeconds(_mediumCookTime));
            _spriteRenderer.color = _readyColor;
            await UniTask.Delay(TimeSpan.FromSeconds(_readyCookTime));
            _spriteRenderer.color = _burntColor;
            await UniTask.Delay(TimeSpan.FromSeconds(_burntCookTime));
        }
    }
}