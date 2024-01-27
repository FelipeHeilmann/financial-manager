using FinancialManager.Domain.Abstraction;
using MediatR;

namespace FinancialManager.Application.Abstraction;
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> where TQuery : IQuery<TResponse>
{
    Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken);
}

