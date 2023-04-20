using Microsoft.AspNetCore.Mvc;

namespace ApiCalculadora.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasicOperationController : ControllerBase
    {
        [HttpGet("AddTwoNumbers")]
        public ActionResult<double> AddTwoNumbers(double number1, double number2)
        {
            double result = number1 + number2;
            return Ok(result);
        }
    }
    
}
