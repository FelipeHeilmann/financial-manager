using FinancialManager.Domain.Abstraction;

namespace FinancialManager.Domain.Exception
{
    public static class InstallmentErrors
    {
        public static readonly Error NotFound = new("Not.Found", "Installment was not found");
    }
}