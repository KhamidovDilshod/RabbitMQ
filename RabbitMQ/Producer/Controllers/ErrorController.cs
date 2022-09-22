using Microsoft.AspNetCore.Mvc;

namespace Sender.Controllers;

public class ErrorController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}