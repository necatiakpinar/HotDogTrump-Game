﻿using System;
using System.Collections.Generic;
using System.Linq;
using Data.ScriptableObjects.Properties;
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
        [SerializeField] private CustomerPropertiesSo _properties;
        [SerializeField] private TMP_Text _orderLabel;
        private List<BaseOrder> _orders;
        private BaseOrder _currentOrder;
        private BaseFood _givenFood;
        private int _currentOrderIndex;

        public CustomerPropertiesSo Properties => _properties;

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
            if (_givenFood == null)
                return;

            for (int i = 0; i < _givenFood.Ingredients.Count; i++)
            {
                var ingredient = _givenFood.Ingredients[i];
                if (ingredient is ICookable cookable)
                {
                    var coinToPay = _properties.IngredientCurrencyDictionary[cookable.CookingState];
                    Debug.LogError(coinToPay);
                }
            }
            
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