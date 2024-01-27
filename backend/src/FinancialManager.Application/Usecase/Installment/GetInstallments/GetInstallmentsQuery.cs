using FinancialManager.Application.Abstraction;

namespace FinancialManager.Application.Usecase.Installment.GetInstallments;

public sealed record GetInstallmentsQuery : IQuery<List<Domain.Entity.Installment>>;

