using Xunit;
using Moq;
using FinancialManager.Domain.Entity;
using FinancialManager.Domain.Enum;
using FinancialManager.Application.Data;
using FinancialManager.Domain.Repository;
using FinancialManager.Application.Model;
using FinancialManager.Application.Usecase.CreateTransaction;
using FinancialManager.Domain.Abstraction;
using FinancialManager.Application.Usecase.CreateInstallment;

namespace FinancialManager.Test.Integration
{
    public class InstallmentIntegrationTest
    {
        private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public InstallmentIntegrationTest()
        {
            _transactionRepositoryMock = new();
            _unitOfWorkMock = new ();
        }

        [Fact]
        public async void Should_Create_Installment_For_A_Transaction_With_Success()
        {

            var request = new CreateTransactionModel() { Amount = 250.00, Date = DateTime.Now, Type = 1, Description = "Teclado Novo", Author = "Felipe" };
            var command = new CreateTransactionCommand(request);

            var handler = new CreateTransactionCommandHandler(_transactionRepositoryMock.Object, _unitOfWorkMock.Object);

            Result<Guid> transactinResult = await handler.Handle(command, default);

            var transactionId = transactinResult.GetValue();

            _transactionRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>(), null))
                  .ReturnsAsync(new Transaction(transactionId, request.Amount, request.Author, request.Date, TransactionType.Credit, request.Description, DateTime.Now));

            var installmentRequest = new CreateInstallmentModel() { Amount = 300, Date = DateTime.Now.AddDays(2), TransactionId = transactionId };

            var installmentCommand = new CreateInstallmentCommand(installmentRequest);

            var installmentCommandHandler = new CreateInstallmentCommandHandler(_transactionRepositoryMock.Object, _unitOfWorkMock.Object);

            var installmentResult = await installmentCommandHandler.Handle(installmentCommand, default);

            Assert.True(transactinResult.IsSuccess);
            Assert.True(installmentResult.IsSuccess);
        }

        [Fact]
        public async void Should_Create_Installment_For_A_Transaction_Thow_And_NotFound()
        {

            var request = new CreateTransactionModel() { Amount = 250.00, Date = DateTime.Now, Type = 1, Description = "Teclado Novo", Author = "Felipe" };
            var command = new CreateTransactionCommand(request);

            var handler = new CreateTransactionCommandHandler(_transactionRepositoryMock.Object, _unitOfWorkMock.Object);

            Result<Guid> transactinResult = await handler.Handle(command, default);

            var transactionId = transactinResult.GetValue();

            var installmentRequest = new CreateInstallmentModel() { Amount = 300, Date = DateTime.Now.AddDays(2), TransactionId = transactionId };

            var installmentCommand = new CreateInstallmentCommand(installmentRequest);

            var installmentCommandHandler = new CreateInstallmentCommandHandler(_transactionRepositoryMock.Object, _unitOfWorkMock.Object);

            var installmentResult = await installmentCommandHandler.Handle(installmentCommand, default);

            Assert.True(transactinResult.IsSuccess);
            Assert.True(installmentResult.IsFailure);
        }
        [Fact]
        public async void Should_Not_Create_Installment_For_Debit_Transaction()
        { 
            var transactionId = Guid.NewGuid();

            _transactionRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>(), null))
                  .ReturnsAsync(new Transaction(transactionId, 250.00, "Felipe", DateTime.Now, TransactionType.Deposit, "Teclado novo", DateTime.Now));

            var installmentRequest = new CreateInstallmentModel() { Amount = 300, Date = DateTime.Now.AddDays(2), TransactionId = transactionId };

            var installmentCommand = new CreateInstallmentCommand(installmentRequest);

            var installmentCommandHandler = new CreateInstallmentCommandHandler(_transactionRepositoryMock.Object, _unitOfWorkMock.Object);

            var installmentResult = await installmentCommandHandler.Handle(installmentCommand, default);

            Assert.True(installmentResult.IsFailure);        }
    }
}
