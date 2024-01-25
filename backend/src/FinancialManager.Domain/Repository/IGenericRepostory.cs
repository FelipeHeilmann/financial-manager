namespace FinancialManager.Domain.Repository
{
    public interface IGenericRepostory<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task SaveAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}
