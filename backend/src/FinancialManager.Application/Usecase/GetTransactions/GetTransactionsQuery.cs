using FinancialManager.Application.Abstraction;
using FinancialManager.Domain.Entity;

namespace FinancialManager.Application.Usecase.GetAllTransactions
{
    public sealed record GetTransactionsQuery() : IQuery<List<Transaction>>;
}
