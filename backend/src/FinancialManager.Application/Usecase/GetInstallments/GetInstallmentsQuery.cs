using FinancialManager.Application.Abstraction;
using FinancialManager.Domain.Entity;

namespace FinancialManager.Application.Usecase.GetInstallments
{
    public sealed record GetInstallmentsQuery : IQuery<List<Installment>>;
}
