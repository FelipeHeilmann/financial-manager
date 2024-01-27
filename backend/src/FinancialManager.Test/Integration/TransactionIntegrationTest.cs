using Xunit;
using Moq;
using FinancialManager.Domain.Repository;
using FinancialManager.Application.Data;
using FinancialManager.Application.Usecase.CreateTransaction;
using FinancialManager.Application.Model;
using FinancialManager.Domain.Abstraction;
using FinancialManager.Domain.Exception;
using FinancialManager.Application.Usecase.GetAllTransactions;
using FinancialManager.Application.Usecase.GetTransactions;
using FinancialManager.Domain.Entity;
using FinancialManager.Domain.Enum;
using FinancialManager.Application.Usecase.GetTransactionById;

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
        public async Task Should_Create_Debit_Transaction_And_Save()
        {
            var request = new CreateTransactionModel() { Amount = 250.00, Date = DateTime.Now, Type = 1, Description = "Teclado Novo", Author = "Felipe" };
            var command = new CreateTransactionCommand(request);

            var handler = new CreateTransactionCommandHandler(_transactionRepositoryMock.Object, _unitOfWorkMock.Object);

            Result result = await handler.Handle(command, default);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Should_Not_Create_Debit_Transaction_And_Throw_Execption()
        {
            var request = new CreateTransactionModel() { Amount = 250.00, Date = DateTime.Now, Type = 3, Description = "Teclado Novo", Author = "Felipe" };
            var command = new CreateTransactionCommand(request);

            var handler = new CreateTransactionCommandHandler(_transactionRepositoryMock.Object, _unitOfWorkMock.Object);

            Result result = await handler.Handle(command, default);

            Assert.False(result.IsSuccess);
            Assert.Equal(TransactionErrors.InvalidType, result.GetError());
        }

        [Fact]
        public async Task Should_Return_Three_Transactions()
        {
            var transaction = new Transaction(Guid.NewGuid(),250 ,"Felipe", DateTime.Now, TransactionType.Credit,"Teclado novo", DateTime.Now);
            var transaction2 = new Transaction(Guid.NewGuid(), 100, "Rosangela", DateTime.Now, TransactionType.Credit, "Fone de ouvido", DateTime.Now);
            var transaction3 = new Transaction(Guid.NewGuid(), 50, "Ricardo", DateTime.Now, TransactionType.Credit, "Gasolina", DateTime.Now);
            var transactions = new List<Transaction>() { transaction , transaction2, transaction3 };
            
            _transactionRepositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>(), "Installments")).ReturnsAsync(transactions);

            var query = new GetTransactionsQuery();

            var queryHandler = new GetTransactionsQueryHandler(_transactionRepositoryMock.Object);

            var result = await queryHandler.Handle(query, default);

            Assert.True(result.IsSuccess);
            Assert.Equal(3, result.GetValue().Count);
        }

        [Fact]
        public async Task Should_Return_Transaction_By_Id()
        {
            var transaction = new Transaction(Guid.NewGuid(), 250, "Felipe", DateTime.Now, TransactionType.Credit, "Teclado novo", DateTime.Now);

            _transactionRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>(), "Installments")).ReturnsAsync(transaction);

            var query = new GetTransactionByIdQuery(transaction.Id);

            var queryHandler = new GetTransactionByIdQueryHandler(_transactionRepositoryMock.Object);

            var result = await queryHandler.Handle(query, default);

            Assert.True(result.IsSuccess);
            Assert.Equal(transaction.Id, result.GetValue().Id);
        }

        [Fact]
        public async Task Should_Return_NotFound_For_Get_Transaction_By_Id()
        {
            var transaction = new Transaction(Guid.NewGuid(), 250, "Felipe", DateTime.Now, TransactionType.Credit, "Teclado novo", DateTime.Now);

            var query = new GetTransactionByIdQuery(transaction.Id);

            var queryHandler = new GetTransactionByIdQueryHandler(_transactionRepositoryMock.Object);

            var result = await queryHandler.Handle(query, default);

            Assert.True(result.IsFailure);
        }
    }
}
