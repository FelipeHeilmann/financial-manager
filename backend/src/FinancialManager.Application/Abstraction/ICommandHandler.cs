using FinancialManager.Domain.Abstraction;
using MediatR;

namespace FinancialManager.Application.Abstraction
{
    public interface ICommandHandler<in TCommand> : IRequest<Result> where TCommand : ICommand
    {
        Task Handle(TCommand command, CancellationToken cancellationToken);
    }

    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>> where TCommand : ICommand<TResponse>
    {
        
    }
}
