using FinancialManager.Application.Abstraction;
using FinancialManager.Domain.Abstraction;
using FinancialManager.Domain.Entity;
using FinancialManager.Domain.Exception;
using FinancialManager.Domain.Repository;

namespace FinancialManager.Application.Usecase.GetTransactionById
{
    public sealed class GetTransactionByIdQueryHandler : IQueryHandler<GetTransactionByIdQuery, Transaction>
    {
        private readonly ITransactionRepository _transactionRepository;

        public GetTransactionByIdQueryHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<Result<Transaction>> Handle(GetTransactionByIdQuery query, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetByIdAsync(query.id, cancellationToken, "Installments");

            if(transaction == null)
            {
                return Result.Failure<Transaction>(TransactionErrors.NotFound);
            }

            return Result.Success<Transaction>(transaction);
        }
    }
}
