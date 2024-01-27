using FinancialManager.Application.Abstraction;

namespace FinancialManager.Application.Usecase.Installment.GetInstallmentById;
public sealed record GetInstallmentByIdQuery(Guid id) : IQuery<Domain.Entity.Installment>;

