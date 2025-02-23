using Abstracts;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class GarbageController : MonoBehaviour
    {
        private BaseIngredient _ingredient;
        private BaseFood _food;
        private void OnEnable()
        {
            EventManager.OnDragEnded += DestroyGarbage;
        }
    
        private void OnDestroy()
        {
            EventManager.OnDragEnded -= DestroyGarbage;
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out BaseIngredient ingredient))
            {
                _ingredient = ingredient;
            }
            else if (other.gameObject.TryGetComponent(out BaseFood food))
            {
                _food = food;
            }
        }
    
        void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out BaseIngredient ingredient))
            {
                _ingredient = null;
            }
            else if (other.gameObject.TryGetComponent(out BaseFood food))
            {
                _food = null;
            }
        }
    
        private void DestroyGarbage()
        {
            if (_ingredient != null)
            {
                _ingredient.ReturnToPool(_ingredient);
            }
            
            if (_food != null)
            {
                _food.ReturnToPool(_food);
            }
        }
    }
}