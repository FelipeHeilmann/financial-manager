using FinancialManager.Application.Model;
using FinancialManager.Application.Usecase.CreateInstallment;
using FinancialManager.Application.Usecase.GetInstallmentById;
using FinancialManager.Application.Usecase.GetInstallments;
using FinancialManager.Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace FinancialManager.API.Controllers
{

    [Route("api/installments")]
    [ApiController]
    public class InstallmentController : ApiController
    {
        public InstallmentController(ISender sender) : base(sender) { }

        [HttpGet]
        public async Task<ActionResult<List<Installment>>> GetInstallments(CancellationToken cancellationToken)
        {
            var query = new GetInstallmentsQuery();
            var result = await Sender.Send(query);
            return Ok(result.GetValue());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Installment>> GetInstallmentById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetInstallmentByIdQuery(id);
            var result = await Sender.Send(query);

            return result.IsSuccess ? Ok(result.GetValue()) : NotFound(result.GetError());
        }

        [HttpPost]
        public async Task<ActionResult> CreateInstallment([FromBody] CreateInstallmentModel request)
        {
            var command = new CreateInstallmentCommand(request);
            var result = await Sender.Send(command);

            return result.IsSuccess ? Ok(result) : BadRequest(result.GetError());
        }
    }
}
