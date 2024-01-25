namespace FinancialManager.Application.Abstraction
{
    public interface ICommand : IBaseCommand
    {

    }

    public interface ICommand<TResponse> : IBaseCommand
    {

    }

    public interface IBaseCommand
    {

    }
}
