using FinancialManager.Domain.Abstraction;
using MediatR;

namespace FinancialManager.Application.Abstraction;
public interface ICommand : IRequest<Result>
{}

public interface ICommand<TResponse> : IRequest<TResponse>
{}
