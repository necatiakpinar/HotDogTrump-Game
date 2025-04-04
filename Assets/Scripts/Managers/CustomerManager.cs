using System;
using System.Collections.Generic;
using Abstracts;
using Misc;
using UnityEngine;

namespace Managers
{
    public class CustomerManager : MonoBehaviour
    {
        [SerializeField] private List<BaseCustomer> _customers;

        private void Start()
        {
            _customers = new List<BaseCustomer>();
            SpawnCustomer();
        }

        private void SpawnCustomer()
        {
            BaseCustomer customer = EventManager.OnSpawnCustomerFromPool.Invoke(CustomerType.NormalCustomer, Vector3.zero, Quaternion.identity, null);
            if (customer != null)
            {
                _customers.Add(customer);
            }
        }
    }
}