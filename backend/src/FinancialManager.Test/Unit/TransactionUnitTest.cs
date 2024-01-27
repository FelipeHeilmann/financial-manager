using FinancialManager.Domain.Entity;
using FinancialManager.Domain.Enum;
using Xunit;

namespace FinancialManager.Test.Unit;
public class TransactionUnitTest
{
    [Fact]
    public void Should_Create_A_Depoist_Transaction()
    {
        var today = new DateTime();
        var type = TransactionType.Deposit;
        var amount = 250.00;
        var name = "Felipe";
        var description = "Para comprar o Fone";
        var transaction = Transaction.Create(name, amount, today, type, description);

        Assert.Equal(250.00, transaction.Amount);
        Assert.Equal(TransactionType.Deposit, transaction.Type);
    }

    [Fact]
    public void Should_Create_A_Credit_Transaction()
    {
        var today = new DateTime();
        var type = TransactionType.Credit;
        var amount = 620.00;
        var name = "Felipe";
        var description = "Para comprar teclado Novo";
        var transaction = Transaction.Create(name, amount, today, type, description);

        Assert.Equal(620.00, transaction.Amount);
        Assert.Equal(TransactionType.Credit, transaction.Type);
    }

    [Fact]           
    public void Should_Create_A_Credit_Transaction_And_Add_One_Installment()
    {
        var today = new DateTime();
        var type = TransactionType.Credit;
        var amount = 620.00;
        var name = "Felipe";
        var description = "Teclado Novo";
        var transaction = Transaction.Create(name, amount, today, type, description);
        var transactionId = transaction.Id;

        Assert.Equal(620.00, transaction.Amount);
        Assert.Equal(TransactionType.Credit, transaction.Type);

        var installmentAmount = 200.00;
        var installmentDate = DateTime.Now.AddDays(1);
        var installment = Installment.Create(installmentAmount, installmentDate, transactionId);

        transaction.AddInstallmalent(installment);

        Assert.Equal(420, transaction.GetRemainingAmountToPay());
    }
}
