using System;
using System.Collections.Generic;
using Interfaces;
using Managers;
using Misc;
using Orders;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Abstracts
{
    public abstract class BaseCustomer : MonoBehaviour, IPoolable<BaseCustomer>
    {
        [SerializeField] private CustomerType _customerType;
        [SerializeField] private TMP_Text _orderLabel;
        private List<BaseOrder> _orders;
        private BaseOrder _currentOrder;
        private BaseFood _givenFood;
        private int _currentOrderIndex;
        
        public CustomerType CustomerType => _customerType;

        private void OnEnable()
        {
            EventManager.OnDragEnded += TryToTakeOrder;
        }

        private void OnDestroy()
        {
            EventManager.OnDragEnded -= TryToTakeOrder;
        }

        private void Start()
        {
            _orders = new List<BaseOrder>();
            _currentOrderIndex = 0;
            var hotDogOrder = new HotDogOrder();
            var hamburgerOrder = new HamburgerOrder();
            _orders.Add(hotDogOrder);
            _orders.Add(hamburgerOrder);
            _currentOrder = _orders[_currentOrderIndex];
            UpdateOrderWidget();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out BaseFood food))
                _givenFood = food;
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out BaseFood food))
                _givenFood = null;
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

        private void TryToTakeOrder()
        {
            if (_givenFood == null)
                return;

            var isValid = CheckOrderIsValid();
            if (isValid)
                TakeOrder();

        }

        private void TakeOrder()
        {
            _givenFood.ReturnToPool(_givenFood);
            PassToNextOrder();
        }

        private void PassToNextOrder()
        {
            _currentOrderIndex++;
            if (_currentOrderIndex >= _orders.Count)
            {
                // send to pool
                ReturnToPool(this);
                return;
            }

            _currentOrder = _orders[_currentOrderIndex];
            UpdateOrderWidget();
        }

        public void UpdateOrderWidget()
        {
            _orderLabel.text = _currentOrder.GetType().Name.ToString();
        }
        public void OnSpawn()
        {
        }
        public void ReturnToPool(BaseCustomer poolObject)
        {
            gameObject.SetActive(false);
            EventManager.OnCustomerReturnToPool?.Invoke(poolObject);
        }
    }
}