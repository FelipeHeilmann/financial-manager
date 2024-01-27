
namespace FinancialManager.Application.Data;
public interface IUnitOfWork
{
    Task<int> Commit(CancellationToken cancellationToken = default);
}
