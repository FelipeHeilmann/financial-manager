using FinancialManager.Application.Model;
using FinancialManager.Application.Usecase.CreateTransaction;
using FinancialManager.Domain.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManager.API.Controllers
{
    [Route("api/tansactions")]
    [ApiController]
    public class TransactionController : ApiController
    {
        public TransactionController(ISender sender) : base(sender) { }

        [HttpPost]
        public async Task<ActionResult> CreateTransaction([FromBody] CreateTransactionModel request, CancellationToken cancellationToken)
        {
            var command = new CreateTransactionCommand(request);

           var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result) : BadRequest(result.GetError());
        }
    }
}
