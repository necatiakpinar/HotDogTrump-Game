using System;
using Abstracts;
using Ingredients;
using Misc;
using UnityEngine;

namespace Managers
{
    public static class EventManager
    {
        public static Func<IngredientType, Vector3, Quaternion, Transform, BreadIngredient> OnSpawnFromBreadPool;
        public static Action<IngredientType> OnIngredientResourceCreated;
    }
}