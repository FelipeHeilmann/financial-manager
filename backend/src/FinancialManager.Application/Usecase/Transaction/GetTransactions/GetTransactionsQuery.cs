using FinancialManager.Application.Abstraction;


namespace FinancialManager.Application.Usecase.Transaction.GetTransactions;
public sealed record GetTransactionsQuery() : IQuery<List<Domain.Entity.Transaction>>;

