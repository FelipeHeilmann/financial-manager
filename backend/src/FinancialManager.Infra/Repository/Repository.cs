using FinancialManager.Domain.Repository;
using FinancialManager.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace FinancialManager.Infra.Repository
{
    public abstract class Repository<TEntity> : IGenericRepostory<TEntity> where TEntity : class
    {
        private readonly ApplicationContext _context;

        protected Repository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken, string? includes = null)
        {
            return await _context.Set<TEntity>().Include(includes ?? string.Empty).ToListAsync(cancellationToken);
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken, string? includes = null)
        {
            var entityId = typeof(TEntity).GetProperty("Id");
            return await _context.Set<TEntity>().Include(includes ?? string.Empty).FirstAsync(entity => (Guid)entityId.GetValue(entity) == id);
        }

        public IQueryable<TEntity> GetQueryable(CancellationToken cancellationToken)
        {
            return _context.Set<TEntity>();
        }

        public void Save(TEntity entity, CancellationToken cancellationToken)
        {
            _context.Add(entity);
        }

        public void Update(TEntity entity, CancellationToken cancellationToken)
        {
            _context.Update(entity);
        }

        public void Delete(TEntity entity, CancellationToken cancellationToken)
        {
            _context.Remove(entity);
        }
    }
}
