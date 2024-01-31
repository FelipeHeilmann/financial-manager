using FinancialManager.Domain.Abstraction;

namespace FinancialManager.Domain.Exception;
public static class TransactionErrors
{
    public static readonly Error InvalidType = Error.Validation("Invalid.Transaction.Type", "Invalid Transaction Type");
    public static readonly Error NotFound = Error.NotFound("Transaction.Not.Found", "Transaction was not found");
    public static readonly Error TransactionDoesNotAcceptInstallment = Error.Validation("Cannot.Add.Installment", "Debit transaction can not add installment");
    
}

