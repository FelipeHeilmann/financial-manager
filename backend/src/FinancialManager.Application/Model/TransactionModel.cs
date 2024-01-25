namespace FinancialManager.Application.Model
{
    public record CreateTransactionModel
    {
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }

        public CreateTransactionModel(double amount, DateTime date, int type, string description, string author)
        {
            Amount = amount;
            Date = date;
            Type = type;
            Description = description;
            Author = author;
        }
    }
}
