using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Ingredients.Meats
{
    public class HamburgerMeat : MeatIngredient
    {
        public async override UniTask Cook()
        {
            await base.Cook();
            Debug.LogError("Hamburger is cooking");
        }
    }
}