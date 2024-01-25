using FinancialManager.Domain.Installment.Entity;
using FinancialManager.Domain.Transaction.Entity;
using FinancialManager.Domain.Transaction.Enum;
using Xunit;


namespace FinancialManager.Test.Unit
{

    public class TransactionUnitTest
    {
        [Fact]
        public void Should_Create_A_Depoist_Transaction()
        {
            var today = new DateTime();
            var type = TransactionType.Deposit;
            var amount = 250.00;
            var name = "Felipe";
            var description = "parcela 2 do fone";
            var transaction = Transaction.Create(name, amount, today, type, description);

            Assert.Equal(transaction.Amount, 250.00);
            Assert.Equal(transaction.Type, TransactionType.Deposit);
        }

        [Fact]
        public void Should_Create_A_Credit_Transaction()
        {
            var today = new DateTime();
            var type = TransactionType.Credit;
            var amount = 620.00;
            var name = "Felipe";
            var description = "Teclado Novo";
            var transaction = Transaction.Create(name, amount, today, type, description);

            Assert.Equal(transaction.Amount, 620.00);
            Assert.Equal(transaction.Type, TransactionType.Credit);
        }

        [Fact]           
        public void Should_Create_A_Credit_Transaction_And_Installment()
        {
            var today = new DateTime();
            var type = TransactionType.Credit;
            var amount = 620.00;
            var name = "Felipe";
            var description = "Teclado Novo";
            var transaction = Transaction.Create(name, amount, today, type, description);
            var transactionId = transaction.Id;

            Assert.Equal(transaction.Amount, 620.00);
            Assert.Equal(transaction.Type, TransactionType.Credit);

            var installmentAmount = 200.00;
            var installmentDate = DateTime.Now.AddDays(1);
            var installment = Installment.Create(installmentAmount, installmentDate, transactionId);

            transaction.AddInstallmalent(installment);

            Assert.Equal(transaction.GetRemainingAmountToPay(), 420);
        }
    }
}

