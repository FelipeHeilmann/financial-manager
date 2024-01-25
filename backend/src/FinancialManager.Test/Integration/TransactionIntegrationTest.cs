using Xunit;
using Moq;
using FinancialManager.Domain.Repository;
using FinancialManager.Application.Data;
using FinancialManager.Application.Usecase.CreateTransaction;
using FinancialManager.Application.Model;

namespace FinancialManager.Test.Integration
{
    public class TransactionIntegrationTest
    {
        private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public TransactionIntegrationTest()
        {
            _transactionRepositoryMock = new();
            _unitOfWorkMock = new();
        }

        [Fact]
        public async void Should_Create_Debit_Transaction_And_Save()
        {
            var request = new CreateTransactionModel(250, DateTime.Now, 1, "Teclado Novo", "Felipe");
            var command = new CreateTransactionCommand(request);

            var handler = new CreateTransactionCommandHandler(_transactionRepositoryMock.Object, _unitOfWorkMock.Object);

            var id = await handler.Handle(command, default);

            Assert.NotNull(id);
        }
    }
}
