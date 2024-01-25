using FinancialManager.Application.Abstraction;
using FinancialManager.Application.Model;
using FinancialManager.Domain.Abstraction;

namespace FinancialManager.Application.Usecase.CreateTransaction
{
    public sealed record CreateTransactionCommand(CreateTransactionModel request) : ICommand<Result<Guid>>;
}
