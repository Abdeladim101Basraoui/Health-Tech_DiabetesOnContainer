using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DiabetesOnContainer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        // GET: api/<ErrorController>
        [HttpGet]
        public IActionResult Get()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var stackTrace = context.Error.StackTrace;
            var message = context.Error.Message;

            return Problem();
        }

    }
}
