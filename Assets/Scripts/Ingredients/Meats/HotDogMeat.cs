using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Ingredients.Meats
{
    public class HotDogMeat : MeatIngredient
    {
        public async override UniTask Cook()
        {
            await base.Cook();
            Debug.LogError("HotDog is cooking");
        }
    }
}