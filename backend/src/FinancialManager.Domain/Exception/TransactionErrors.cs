using FinancialManager.Domain.Abstraction;

namespace FinancialManager.Domain.Exception
{
    public static class TransactionErrors
    {
        public static readonly Error InvalidType = new("Invalid.TransactionType", "Invalid Transactio Type");
        public static readonly Error NotFound = new("Not.Found", "Transaction was not found");
        public static readonly Error TransactionDoesNotAcceptInstallment = new("CannotAdd.Installment", "Debit transaction can not add installment");
    }
}
