using Abstracts;
using Misc;

namespace Orders
{
    public class HamburgerOrder : BaseOrder
    {
        public HamburgerOrder()
        {
            ingredients = new()
            {
                IngredientType.HamburgerBread, IngredientType.HamburgerMeat
            };
        }
    }
}