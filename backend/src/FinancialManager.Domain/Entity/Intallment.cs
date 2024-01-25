namespace FinancialManager.Domain.Entity
{
    public class Installment
    {
        public Guid Id { get; private set; }
        public double Amount { get; private set; }
        public DateTime Date { get; private set; }
        public Guid TransactionId { get; private set; }

        public Installment(Guid id, double amount, DateTime date, Guid transactionId)
        {
            Id = id;
            Amount = amount;
            Date = date;
            TransactionId = transactionId;
        }

        public static Installment Create(double amount, DateTime date, Guid transactionId)
        {
            return new Installment(Guid.NewGuid(), amount, date, transactionId);
        }
    }
}
