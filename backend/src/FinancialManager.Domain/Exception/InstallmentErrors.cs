using FinancialManager.Domain.Abstraction;

namespace FinancialManager.Domain.Exception;
public static class InstallmentErrors
{
    public static readonly Error NotFound = Error.NotFound("Not.Found", "Installment was not found");
}
