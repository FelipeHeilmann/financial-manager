namespace FinancialManager.Application.Abstraction
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task Handle(TCommand command, CancellationToken cancellationToken);
    }

    public interface ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken);
    }
}
