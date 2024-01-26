using FinancialManager.Application.Abstraction;
using FinancialManager.Application.Usecase.GetAllTransactions;
using FinancialManager.Domain.Abstraction;
using FinancialManager.Domain.Entity;
using FinancialManager.Domain.Repository;

namespace FinancialManager.Application.Usecase.GetTransactions
{
    public sealed class GetTransactionsQueryHandler : IQueryHandler<GetTransactionsQuery, List<Transaction>>
    {
        private readonly ITransactionRepository _transactionRepository;

        public GetTransactionsQueryHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<Result<List<Transaction>>> Handle(GetTransactionsQuery query, CancellationToken cancellationToken)
        {
            var tranasctions = await _transactionRepository.GetAllAsync(cancellationToken, "Installments");

            return Result.Success(tranasctions);
        }
    }
}
