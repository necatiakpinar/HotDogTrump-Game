using Managers;
using Misc;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Abstracts
{
    public abstract class BaseIngredientResource : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private IngredientResourceType _ingredientResourceType;
        [SerializeField] private IngredientType _ingredientType;

        public void OnPointerClick(PointerEventData eventData)
        {
            EventManager.OnIngredientResourceCreated.Invoke(_ingredientType);
        }
    }
}