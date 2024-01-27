using FinancialManager.API.Extension;
using FinancialManager.Application.Model;
using FinancialManager.Application.Usecase.Installment.CreateInstallment;
using FinancialManager.Application.Usecase.Installment.GetInstallmentById;
using FinancialManager.Application.Usecase.Installment.GetInstallments;
using FinancialManager.Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManager.API.Controllers;

[Route("api/installments")]
[ApiController]
public class InstallmentController : ApiController
{
    public InstallmentController(ISender sender) : base(sender) { }

    [HttpGet]
    public async Task<IResult> GetInstallments(CancellationToken cancellationToken)
    {
        var query = new GetInstallmentsQuery();
        var result = await Sender.Send(query);
        return Results.Ok(result.GetValue());
    }
    [HttpGet("{id}")]
    public async Task<IResult> GetInstallmentById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetInstallmentByIdQuery(id);
        var result = await Sender.Send(query);

        return result.IsSuccess ? Results.Ok(result.GetValue()) : result.ToProblemDetail();
    }

    [HttpPost]
    public async Task<IResult> CreateInstallment([FromBody] CreateInstallmentModel request)
    {
        var command = new CreateInstallmentCommand(request);
        var result = await Sender.Send(command);

        return result.IsSuccess ? Results.Ok(result.GetValue()) : result.ToProblemDetail();
    }
}

