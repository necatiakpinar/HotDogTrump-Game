using System.Collections.Generic;
using Orders;
using TMPro;
using UnityEngine;

namespace Abstracts
{
    public abstract class BaseCustomer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _orderLabel;
        private List<BaseOrder> _orders;
        private BaseOrder _currentOrder;
        private BaseFood _givenFood;

        private void Start()
        {
            _orders = new List<BaseOrder>();
            var hotDogOrder = new HotDogOrder();
            var hamburgerOrder = new HamburgerOrder();
            _orders.Add(hotDogOrder);
            _currentOrder = _orders[0];
            OpenOrderWidget();
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out BaseFood food))
            {
                _givenFood = food;
                var isValid = CheckOrderIsValid();
                Debug.LogError(_givenFood.FoodType + " " + isValid);
            }
        }
        
        private bool CheckOrderIsValid()
        {
            foreach (var ingredient in _givenFood.Ingredients)
            {
                if (!_currentOrder.Ingredients.Contains(ingredient.IngredientType))
                    return false;
            }

            return true;
        }

        public void OpenOrderWidget()
        {
            _orderLabel.text = _currentOrder.GetType().Name.ToString();
        }
    }
}