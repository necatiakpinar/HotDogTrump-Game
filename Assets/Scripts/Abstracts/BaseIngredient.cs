using Controllers;
using Interfaces;
using Misc;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Abstracts
{
    public abstract class BaseIngredient : MonoBehaviour, IDraggable
    {
        [SerializeField] private IngredientType _ingredientType;
        private bool _isDragging;
        private IngredientPlacementSlotController _placedSlot;
        
        public IngredientType IngredientType => _ingredientType;
        public void OnPointerDown(PointerEventData eventData)
        {
            _isDragging = true;
        }
        public void OnPointerUp(PointerEventData eventData)
        {
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
        
        public void PlaceToSlot(IngredientPlacementSlotController slot)
        {
            _placedSlot = slot;
            transform.SetParent(_placedSlot.transform);
            transform.localPosition = Vector3.zero;
        }
    }
}
