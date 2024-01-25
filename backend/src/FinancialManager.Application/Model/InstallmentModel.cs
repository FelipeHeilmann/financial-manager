namespace FinancialManager.Application.Model
{
    public record CreateInstallmentModel
    {
        public double Amount { get; set;}
        public DateTime Date { get; set;}
        public Guid TransactionId { get; set;}
    }
    
}
