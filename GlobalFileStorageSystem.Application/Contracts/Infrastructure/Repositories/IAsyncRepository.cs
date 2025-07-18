namespace GlobalFileStorageSystem.Application.Contracts.Infrastructure.Repositories
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        IQueryable<T> ListAll();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
