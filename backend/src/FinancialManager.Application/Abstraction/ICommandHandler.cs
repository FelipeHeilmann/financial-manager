using FinancialManager.Domain.Abstraction;
using MediatR;

namespace FinancialManager.Application.Abstraction;
public interface ICommandHandler<TCommand> : IRequest<Result> where TCommand : ICommand
{
    Task Handle(TCommand command, CancellationToken cancellationToken);
}

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
{
    Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken);
}

