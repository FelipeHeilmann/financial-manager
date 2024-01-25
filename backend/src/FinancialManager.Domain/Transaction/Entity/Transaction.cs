using FinancialManager.Domain.Transaction.Enum;

namespace FinancialManager.Domain.Transaction.Entity
{
    public class Transaction
    {
        public Guid Id { get; private set; }
        public string MadeBy { get; private set; }
        public DateTime Date { get; private set; }
        public double Amount { get; private set; }
        public TransactionType Type { get; private set; }
        public string Description { get; private set; }
        public ICollection<FinancialManager.Domain.Installment.Entity.Installment> Installments { get; private set; }

        public Transaction(Guid id, double amount, string madeBy, DateTime date, TransactionType type, string description)
        {
            Id = id;
            MadeBy = madeBy;
            Date = date;
            Type = type;
            Description = description;
            Amount = amount;
            Installments = new List<FinancialManager.Domain.Installment.Entity.Installment>();
        }

        public static Transaction Create(string madeBy, double amount, DateTime date, TransactionType type, string description)
        {
            return new Transaction(Guid.NewGuid(), amount, madeBy, date, type, description);
        }

        public void AddInstallmalent(FinancialManager.Domain.Installment.Entity.Installment installment)
        {
            Installments.Add(installment);
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
