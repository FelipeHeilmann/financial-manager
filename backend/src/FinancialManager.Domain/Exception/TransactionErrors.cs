using FinancialManager.Domain.Abstraction;

namespace FinancialManager.Domain.Exception
{
    public static class TransactionErrors
    {
        public static readonly Error InvalidType = new("Invalid.TransactionType", "Invalid Transactio Type");
    }
}
