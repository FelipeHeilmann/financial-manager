namespace FinancialManager.Application.Model;
public sealed record CreateTransactionModel(double Amount, DateTime Date, int Type, string Description, string Author);

