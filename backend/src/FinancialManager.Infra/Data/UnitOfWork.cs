using FinancialManager.Application.Data;
using FinancialManager.Infra.Context;

namespace FinancialManager.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<int> Commit(CancellationToken cancellationToken = default)
        {
           return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
