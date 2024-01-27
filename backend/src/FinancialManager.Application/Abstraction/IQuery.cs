using FinancialManager.Domain.Abstraction;
using MediatR;

namespace FinancialManager.Application.Abstraction;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{}

