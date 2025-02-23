using System.Collections.Generic;
using Controllers;
using Interfaces;
using Managers;
using Misc;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Abstracts
{
    public abstract class BaseFood : MonoBehaviour, IDraggable, ISlotPlacable, IPoolable<BaseFood>
    {
        [SerializeField] private FoodType _foodType;
        private bool _isDragging;
        private IngredientPlacementSlotController _placedSlot;
        protected List<BaseIngredient> _ingredients = new List<BaseIngredient>();

        public FoodType FoodType => _foodType;
        public List<BaseIngredient> Ingredients => _ingredients;

        public void AddIngredient(BaseIngredient ingredient)
        {
            ingredient.transform.SetParent(transform);
            ingredient.transform.localPosition = Vector3.zero;
            ingredient.Collider.enabled = false;
            _ingredients.Add(ingredient);
        }

        public void RemoveIngredient(BaseIngredient ingredient)
        {
            _ingredients.Remove(ingredient);
        }

        public void RemoveAllIngredients()
        {
            foreach (var ingredient in _ingredients)
            {
                ingredient.ReturnToPool(ingredient);
            }

            _ingredients.Clear();
        }

        public bool HasIngredients(IngredientType ingredientType)
        {
            return _ingredients.Exists(ingredient => ingredient.IngredientType == ingredientType);
        }
        public void OnSpawn()
        {
        }
        public void ReturnToPool(BaseFood poolObject)
        {
            gameObject.SetActive(false);
            if (_placedSlot != null)
            {
                _placedSlot.RemoveIngredient();
                _placedSlot = null;
            }

            foreach (var ingredient in _ingredients)
            {
                ingredient.ReturnToPool(ingredient);
            }

            EventManager.OnFoodReturnToPool?.Invoke(poolObject);
        }

        public void PlaceToSlot(IngredientPlacementSlotController slot)
        {
            _placedSlot = slot;
            transform.SetParent(slot.transform);
            transform.localPosition = Vector3.zero;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            _isDragging = true;
            EventManager.OnDragStarted?.Invoke();
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            EventManager.OnDragEnded?.Invoke();
            _isDragging = false;
            transform.localPosition = Vector3.zero;
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (!_isDragging)
                return;

            var mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 0));
            mouseWorldPosition.z = 0;
            transform.position = mouseWorldPosition;
        }
    }
}