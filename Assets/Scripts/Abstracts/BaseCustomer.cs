using System.Collections.Generic;
using Orders;
using UnityEngine;

namespace Abstracts
{
    public abstract class BaseCustomer : MonoBehaviour
    {
        private List<BaseOrder> _orders;

        private void Start()
        {
            _orders = new List<BaseOrder>();
            var hotDogOrder = new HotDogOrder();
            var hamburgerOrder = new HamburgerOrder();
            _orders.Add(hotDogOrder);
            _orders.Add(hamburgerOrder);
            
        }
    }
}