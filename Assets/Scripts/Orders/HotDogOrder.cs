using Abstracts;
using Misc;

namespace Orders
{
    public class HotDogOrder : BaseOrder
    {
        public HotDogOrder()
        {
            ingredients = new()
            {
                IngredientType.HotDogBread, IngredientType.HotDogMeat
            };
        }
    }
}