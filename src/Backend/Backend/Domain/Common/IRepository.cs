namespace Backend.Domain.Common
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }  
}
