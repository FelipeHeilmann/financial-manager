using FinancialManager.Domain.Entity;
using FinancialManager.Domain.Repository;
using FinancialManager.Infra.Context;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FinancialManager.Infra.Repository
{
    public abstract class Repository<TEntity> : IGenericRepostory<TEntity> where TEntity : class
    {
        protected readonly ApplicationContext _context;

        protected Repository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken, string? includes = null)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (!string.IsNullOrEmpty(includes))
            {
                query = query.Include(includes);
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken, string? includes = null)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (!string.IsNullOrEmpty(includes))
            {
                query = query.Include(includes);
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id, cancellationToken);
        }

        public IQueryable<TEntity> GetQueryable(CancellationToken cancellationToken)
        {
            return _context.Set<TEntity>();
        }

        public void Save(TEntity entity, CancellationToken cancellationToken)
        {
            _context.Add(entity);
        }

        public virtual void Update(TEntity entity, CancellationToken cancellationToken)
        {
            _context.Update(entity);
        }

        public void Delete(TEntity entity, CancellationToken cancellationToken)
        {
            _context.Remove(entity);
        }
    }
}
