using FinancialManager.Application.Abstraction;
using FinancialManager.Domain.Abstraction;
using FinancialManager.Domain.Exception;
using FinancialManager.Domain.Repository;

namespace FinancialManager.Application.Usecase.Transaction.GetTransactionById;
public sealed class GetTransactionByIdQueryHandler : IQueryHandler<GetTransactionByIdQuery, Domain.Entity.Transaction>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionByIdQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<Result<Domain.Entity.Transaction>> Handle(GetTransactionByIdQuery query, CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetByIdAsync(query.id, cancellationToken, "Installments");

        if (transaction == null)
        {
            return Result.Failure<Domain.Entity.Transaction>(TransactionErrors.NotFound);
        }

        return Result.Success(transaction);
    }
}
