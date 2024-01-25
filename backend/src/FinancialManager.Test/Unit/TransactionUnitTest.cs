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
    }
}

