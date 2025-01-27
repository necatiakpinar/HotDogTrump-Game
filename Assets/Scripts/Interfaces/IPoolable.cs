namespace Interfaces
{
    public interface IPoolable <T> where T : IPoolable<T>
    {
        public void OnSpawn();
        public void ReturnToPool(T poolObject);
    }
}