namespace FinancialManager.Application.Model
{
    public sealed record CreateTransactionModel
    {
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
    }
}
