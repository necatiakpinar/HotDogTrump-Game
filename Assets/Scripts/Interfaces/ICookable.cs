using Cysharp.Threading.Tasks;
using Misc;

namespace Interfaces
{
    public interface ICookable
    {
        public CookableIngredientStateType CookingState { get; }
        public UniTask Cook();
    }
}