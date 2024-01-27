using FinancialManager.Application.Abstraction;
using FinancialManager.Domain.Abstraction;

namespace FinancialManager.Application.Usecase.Installment.DeleteInstallment;

public sealed record DeleteInstallmentCommand(Guid id) : ICommand<Result>;
