namespace FinancialManager.Domain.Repository
{
    public interface IGenericRepostory<TEntity>
    {
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken, string? includes = null);
        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken, string? includes = null);
        IQueryable<TEntity> GetQueryable (CancellationToken cancellationToken);
        void Save(TEntity entity, CancellationToken cancellationToken);
        void Update(TEntity entity, CancellationToken cancellationToken);
        void Delete(TEntity entity, CancellationToken cancellationToken);
    }
}
