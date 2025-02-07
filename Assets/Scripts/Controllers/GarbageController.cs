using Abstracts;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class GarbageController : MonoBehaviour
    {
        private BaseIngredient _ingredient;
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
        }
    
        void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out BaseIngredient ingredient))
            {
                _ingredient = null;
            }
        }
    
        private void DestroyGarbage()
        {
            if (_ingredient != null)
            {
                _ingredient.ReturnToPool(_ingredient);
            }
        }
    }
}