using FinancialManager.Application.Abstraction;
using FinancialManager.Application.Model;
using FinancialManager.Domain.Abstraction;

namespace FinancialManager.Application.Usecase.CreateInstallment
{
    public sealed record CreateInstallmentCommand(CreateInstallmentModel request) : ICommand<Result<Guid>>;
}
