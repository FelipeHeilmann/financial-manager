using FinancialManager.Application.Abstraction;
using FinancialManager.Domain.Abstraction;

namespace FinancialManager.Application.Usecase.Transaction.DeleteTransaction
{
    public sealed record DeleteTransactionCommand(Guid id) : ICommand<Result>;

}
