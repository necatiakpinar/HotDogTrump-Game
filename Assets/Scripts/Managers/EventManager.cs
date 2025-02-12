﻿using System;
using Abstracts;
using Ingredients;
using Misc;
using UnityEngine;

namespace Managers
{
    public static class EventManager
    {
        public static Func<IngredientType, Vector3, Quaternion, Transform, BaseIngredient> OnSpawnFromPool;
        public static Action<BaseIngredient> OnReturnToPool;
        public static Action<IngredientType> OnIngredientResourceCreated;
        public static Action OnDragStarted;
        public static Action OnDragEnded;
    }
}