using FinancialManager.Application.Abstraction;
using FinancialManager.Domain.Entity;

namespace FinancialManager.Application.Usecase.GetTransactionById
{
    public sealed record GetTransactionByIdQuery(Guid id): IQuery<Transaction>;
}
