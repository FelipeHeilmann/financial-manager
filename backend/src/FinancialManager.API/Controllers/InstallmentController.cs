using FinancialManager.Application.Model;
using FinancialManager.Application.Usecase.CreateInstallment;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManager.API.Controllers
{

    [Route("api/installments")]
    [ApiController]
    public class InstallmentController : ApiController
    {
        public InstallmentController(ISender sender) : base(sender) { }

        [HttpPost]
        public async Task<ActionResult> CreateInstallment([FromBody] CreateInstallmentModel request)
        {
            var command = new CreateInstallmentCommand(request);
            var result = await Sender.Send(command);

            return result.IsSuccess ? Ok(result) : BadRequest(result.GetError());
        }
    }
}
