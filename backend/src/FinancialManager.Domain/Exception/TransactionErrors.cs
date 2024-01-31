using FinancialManager.Domain.Abstraction;

namespace FinancialManager.Domain.Exception;
public static class TransactionErrors
{
    public static readonly Error InvalidType = Error.Validation("Invalid.TransactionType", "Invalid Transaction Type");
    public static readonly Error NotFound = Error.NotFound("Not.Found", "Transaction was not found");
    public static readonly Error TransactionDoesNotAcceptInstallment = Error.Validation("CannotAdd.Installment", "Debit transaction can not add installment");
    public static readonly Error InvalidInstallment = Error.Validation("Installment.invalid.amount", "The installment amount is greatter then transaction value remaining to pay");
}

