using Microsoft.AspNetCore.Mvc;

namespace Producer.Controllers;

public class ErrorController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}