using Abstracts;
using Interfaces;

namespace Ingredients
{
    public abstract class BreadIngredient : BaseIngredient, IPoolable<BreadIngredient>
    {
        public void OnSpawn()
        {
        }
        public void ReturnToPool(BreadIngredient poolObject)
        {
        }
    }
}