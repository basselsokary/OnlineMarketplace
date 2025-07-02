using Application.Common.Interfaces.Messaging.Requests.Dispatchers;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Models;

namespace Web.Controllers;

public abstract class ApiController : Controller
{
    protected readonly IRequestDispatcher Dispatcher;

    protected ApiController(IRequestDispatcher dispatcher) => Dispatcher = dispatcher;

    protected void AddModelErrors(Result result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error);
        }
    }
}
