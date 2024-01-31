using FinancialManager.API.Extension;
using FinancialManager.Application.Model;
using FinancialManager.Application.Usecase.Transaction.CreateTransaction;
using FinancialManager.Application.Usecase.Transaction.DeleteTransaction;
using FinancialManager.Application.Usecase.Transaction.GetTransactions;
using FinancialManager.Application.Usecase.Transaction.GetTransactionById;
using FinancialManager.Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManager.API.Controllers;

[Route("api/transactions")]
[ApiController]
public class TransactionController : ApiController
{
    public TransactionController(ISender sender) : base(sender) { }

    [HttpGet]
    public async Task<ActionResult<List<Transaction>>> GetTransactions()
    {
        var query = new GetTransactionsQuery();
        var result = await Sender.Send(query);

        return Ok(result.GetValue());
    }
    [HttpGet("{id}")]
    public async Task<IResult> GetTransactionById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetTransactionByIdQuery(id);
        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Results.Ok(result.GetValue()) : result.ToProblemDetail();
    }


    [HttpPost]
    public async Task<IResult> CreateTransaction([FromBody] CreateTransactionModel request, CancellationToken cancellationToken)
    {
        var command = new CreateTransactionCommand(request);

        var result = await Sender.Send(command, cancellationToken);
            
        return result.IsSuccess ? Results.Created<Guid>($"/transactions/{result.GetValue()}", result.GetValue()) : result.ToProblemDetail();
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteTranaction(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteTransactionCommand(id);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Results.NoContent() : result.ToProblemDetail();
    }
}