namespace FinancialManager.Domain.Repository
{
    public interface IGenericRepostory<T>
    {
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task SaveAsync(T entity, CancellationToken cancellationToken);
        Task UpdateAsync(T entity, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
