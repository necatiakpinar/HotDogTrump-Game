using System.Collections.Generic;
using Misc;

namespace Abstracts
{
    public abstract class BaseOrder
    {
        protected List<IngredientType> ingredients { get; set; }
        
        public List<IngredientType> Ingredients => ingredients;
    }
}