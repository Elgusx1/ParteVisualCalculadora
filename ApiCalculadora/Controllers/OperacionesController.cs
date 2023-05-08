using ApiCalculadora.classes;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiCalculadora.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [SwaggerTag("Operaciones")]
    public class OperacionesController : Controller
    {
        [HttpGet("calculos")]
        public ActionResult<double[]> AgregarYCalculos([FromQuery] string DisplayText)
        {
            Operaciones operaciones = new Operaciones();
            operaciones.AgregarYCalculos(DisplayText);

            double resultado = operaciones.Resultado;
            return Ok(resultado);
        }


        //[HttpPost]
        //public ActionResult<double> AgregarYCalculos([FromBody] string DisplayText)
        //{
        //    Operaciones operaciones = new Operaciones();
        //    operaciones.AgregarYCalculos(DisplayText);
        //    double resultado = operaciones.Resultado;
        //    return Ok(resultado);
        //}
    }


}
