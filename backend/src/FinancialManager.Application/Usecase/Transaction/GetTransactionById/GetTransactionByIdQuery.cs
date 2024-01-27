using FinancialManager.Application.Abstraction;

namespace FinancialManager.Application.Usecase.Transaction.GetTransactionById;
public sealed record GetTransactionByIdQuery(Guid id) : IQuery<Domain.Entity.Transaction>;

