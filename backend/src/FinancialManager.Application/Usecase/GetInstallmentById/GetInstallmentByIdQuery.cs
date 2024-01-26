using FinancialManager.Application.Abstraction;
using FinancialManager.Domain.Entity;

namespace FinancialManager.Application.Usecase.GetInstallmentById
{
    public sealed record GetInstallmentByIdQuery(Guid id) : IQuery<Installment>;
}
