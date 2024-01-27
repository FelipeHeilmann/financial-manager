using Xunit;
using Moq;
using FinancialManager.Domain.Exception;
using FinancialManager.Domain.Repository;
using FinancialManager.Application.Data;
using FinancialManager.Application.Model;
using FinancialManager.Domain.Entity;
using FinancialManager.Application.Usecase.Installment.CreateInstallment;
using FinancialManager.Application.Usecase.Transaction.CreateTransaction;
using FinancialManager.Domain.Abstraction;
using FinancialManager.Domain.Enum;
using FinancialManager.Application.Usecase.Installment.GetInstallments;
using FinancialManager.Application.Usecase.Installment.GetInstallmentById;

namespace FinancialManager.Test.Integration;

public class InstallmentIntegrationTest
{
    private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
    private readonly Mock<IInstallmentRepository> _installmentRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public InstallmentIntegrationTest()
    {
        _transactionRepositoryMock = new();
        _unitOfWorkMock = new ();
        _installmentRepositoryMock = new();
    }

    [Fact]
    public async Task Should_Create_Installment_For_A_Transaction_With_Success()
    {
        var request = new CreateTransactionModel() { Amount = 250.00, Date = DateTime.Now, Type = 1, Description = "Teclado Novo", Author = "Felipe" };
        var transactionId = Guid.NewGuid();

        _transactionRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>(), "Installments"))
                .ReturnsAsync(new Transaction(transactionId, request.Amount, request.Author, request.Date, TransactionType.Credit, request.Description, DateTime.Now));

        var installmentRequest = new CreateInstallmentModel() { Amount = 300, Date = DateTime.Now.AddDays(2), TransactionId = transactionId };
        var installmentCommand = new CreateInstallmentCommand(installmentRequest);
        var installmentCommandHandler = new CreateInstallmentCommandHandler(_transactionRepositoryMock.Object, _unitOfWorkMock.Object);

        var installmentResult = await installmentCommandHandler.Handle(installmentCommand, default);

        Assert.True(installmentResult.IsSuccess);
    }

    [Fact]
    public async Task Should_Not_Create_Installment_For_A_Transaction_And_Throw_NotFound()
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
    public async Task Should_Not_Create_Installment_For_Debit_Transaction()
    { 
        var transactionId = Guid.NewGuid();

        _transactionRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>(), null))
                .ReturnsAsync(new Transaction(transactionId, 250.00, "Felipe", DateTime.Now, TransactionType.Deposit, "Teclado novo", DateTime.Now));

        var installmentRequest = new CreateInstallmentModel() { Amount = 300, Date = DateTime.Now.AddDays(2), TransactionId = transactionId };

        var installmentCommand = new CreateInstallmentCommand(installmentRequest);

        var installmentCommandHandler = new CreateInstallmentCommandHandler(_transactionRepositoryMock.Object, _unitOfWorkMock.Object);

        var installmentResult = await installmentCommandHandler.Handle(installmentCommand, default);

        Assert.True(installmentResult.IsFailure);        
    }

    [Fact]
    public async Task Should_Return_Three_Installments()
    {
        var transactionId = Guid.NewGuid();

        var installment1 = new Installment(Guid.NewGuid(), 250, DateTime.Now, transactionId, DateTime.Now);
        var installment2 = new Installment(Guid.NewGuid(), 250, DateTime.Now, transactionId, DateTime.Now);
        var installment3 = new Installment(Guid.NewGuid(), 250, DateTime.Now, transactionId, DateTime.Now);

        var installments = new List<Installment> { installment1, installment2, installment3 };

        _installmentRepositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>(), null)).ReturnsAsync(installments);

        var query = new GetInstallmentsQuery();

        var queryHandler = new GetInstallmentsQueryHandler(_installmentRepositoryMock.Object);

        var result = await queryHandler.Handle(query, default);

        Assert.True(result.IsSuccess);
        Assert.Equal(3, result.GetValue().Count);
    }

    [Fact]
    public async Task Should_Return_Installment_By_Id()
    {
        var installmentId = Guid.NewGuid();
        var transactionId = Guid.NewGuid();

        var installment = new Installment(installmentId, 250, DateTime.Now, transactionId, DateTime.Now);

        _installmentRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(),It.IsAny<CancellationToken>(), null)).ReturnsAsync(installment);

        var query = new GetInstallmentByIdQuery(transactionId);

        var queryHandler = new GetInstallmentByIdQueryHandler(_installmentRepositoryMock.Object);

        var result = await queryHandler.Handle(query, default);

        Assert.True(result.IsSuccess);
        Assert.Equal(installmentId, result.GetValue().Id);
    }

    [Fact]
    public async Task Should_Return_NotFound_For_Get_Installment_By_Id()
    {
        var installmentId = Guid.NewGuid();

        var query = new GetInstallmentByIdQuery(installmentId);

        var queryHandler = new GetInstallmentByIdQueryHandler(_installmentRepositoryMock.Object);

        var result = await queryHandler.Handle(query, default);

        Assert.True(result.IsFailure);
        Assert.Equal(InstallmentErrors.NotFound, result.GetError());
    }
}

