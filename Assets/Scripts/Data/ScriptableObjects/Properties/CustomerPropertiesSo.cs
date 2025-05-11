using Misc;
using UnityEngine;

namespace Data.ScriptableObjects.Properties
{
    [CreateAssetMenu(fileName = "CustomerProperties", menuName = "ScriptableObjects/Properties/CustomerProperties")]
    public class CustomerPropertiesSo : ScriptableObject
    {
        [SerializeField] private CustomerType _customerType;
        [SerializeField] private SerializableDictionary<CookableIngredientStateType, int> _ingredientCurrencyDictionary;

        public CustomerType CustomerType => _customerType;
        public SerializableDictionary<CookableIngredientStateType, int> IngredientCurrencyDictionary => _ingredientCurrencyDictionary;
    }
}