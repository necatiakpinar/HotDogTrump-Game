using System;
using Controllers;
using Interfaces;
using Managers;
using Misc;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Abstracts
{
    public abstract class BaseIngredient : MonoBehaviour, IDraggable, ISlotPlacable, IPoolable<BaseIngredient>
    {
        [SerializeField] private IngredientType _ingredientType;
        private Collider2D _collider;
        private IngredientPlacementSlotController _placedSlot;
        protected bool isDragging;
        protected BaseFood food;
        
        public IngredientType IngredientType => _ingredientType;
        public Collider2D Collider => _collider;
        
        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            isDragging = true;
            EventManager.OnDragStarted?.Invoke();
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            EventManager.OnDragEnded?.Invoke();
            isDragging = false;
            
            TryToPlaceInFood();
        }
        protected virtual void TryToPlaceInFood()
        {
            if (food != null)
            {
                food.AddIngredient(this);
            }
            else
            {
                transform.localPosition = Vector3.zero;
            }
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (!isDragging)
                return;

            var mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 0));
            mouseWorldPosition.z = 0;
            transform.position = mouseWorldPosition;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out BaseFood food))
                this.food = food;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out BaseFood food))
                if (this.food == food)
                    this.food = null;
        }

        public void PlaceToSlot(IngredientPlacementSlotController slot)
        {
            _placedSlot = slot;
            transform.SetParent(_placedSlot.transform);
            transform.localPosition = Vector3.zero;
        }
        public virtual void OnSpawn()
        {
            _collider.enabled = true;
        }
        public virtual void ReturnToPool(BaseIngredient poolObject)
        {
            gameObject.SetActive(false);
            if (_placedSlot != null)
            {
                _placedSlot.RemoveIngredient();
                _placedSlot = null;
            }
            EventManager.OnIngredientReturnToPool?.Invoke(poolObject);
        }
    }
}