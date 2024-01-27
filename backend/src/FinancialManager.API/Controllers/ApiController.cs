using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManager.API.Controllers;

[ApiController]
public abstract class ApiController : ControllerBase
{
    protected readonly ISender Sender;

    protected ApiController(ISender sender)
    {
        Sender = sender;
    }
}

