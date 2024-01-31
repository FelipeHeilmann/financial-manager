using FinancialManager.Domain.Abstraction;

namespace FinancialManager.Domain.Exception;
public static class InstallmentErrors
{
    public static readonly Error NotFound = Error.NotFound("Installment.Not.Found", "Installment was not found");
    public static readonly Error InvalidAmount = Error.Validation("Installment.Amount.Less.Equal.Zero", "The amount of the installment should be greatter than zero");
    public static readonly Error InvalidInstallment = Error.Validation("Installment.invalid.amount", "The installment amount is greatter then transaction value remaining to pay");
}
