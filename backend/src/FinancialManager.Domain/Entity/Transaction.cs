using FinancialManager.Domain.Entity;
using FinancialManager.Domain.Enum;
using FinancialManager.Domain.Abstraction;
using FinancialManager.Domain.Exception;

namespace FinancialManager.Domain.Entity
{
    public class Transaction
    {
        public Guid Id { get; private set; }
        public string Author { get; private set; }
        public DateTime Date { get; private set; }
        public double Amount { get; private set; }
        public TransactionType Type { get; private set; }
        public string Description { get; private set; }
        public ICollection<Installment> Installments { get; private set; }

        public Transaction(Guid id, double amount, string author, DateTime date, TransactionType type, string description)
        {
            Id = id;
            Author = author;
            Date = date;
            Type = type;
            Description = description;
            Amount = amount;
            Installments = new List<Installment>();
        }

        public static Transaction Create(string author, double amount, DateTime date, TransactionType type, string description)
        {
            return new Transaction(Guid.NewGuid(), amount, author, date, type, description);
        }

        public Result AddInstallmalent(Installment installment)
        {
            if (Type == TransactionType.Deposit) return Result.Failure(TransactionErrors.TransactionDoesNotAcceptInstallment);
            Installments.Add(installment);
            return Result.Success();
        }
        
        public double GetRemainingAmountToPay()
        {
            var total = Amount;
            foreach (var installment in Installments)
            {
                total -= installment.Amount;
            }

            return total;
        }
    }
}
