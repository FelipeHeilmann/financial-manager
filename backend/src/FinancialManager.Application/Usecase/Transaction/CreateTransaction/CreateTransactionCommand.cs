using FinancialManager.Application.Abstraction;
using FinancialManager.Application.Model;
using FinancialManager.Domain.Abstraction;

namespace FinancialManager.Application.Usecase.Transaction.CreateTransaction
{
    public sealed record CreateTransactionCommand(CreateTransactionModel request) : ICommand<Result<Guid>>;
}
