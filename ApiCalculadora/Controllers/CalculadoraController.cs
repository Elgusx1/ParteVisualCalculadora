using Microsoft.AspNetCore.Mvc;


namespace ApiCalculadora.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculadoraController : ControllerBase
    {
        //[HttpPost]
        //public string GetDisplayText(MainWindowViewModel viewModel)
        //{
        //    return viewModel.DisplayText;
        //}
        //public ActionResult<string> RealizarCalculo([FromBody] CalculoModel model)
        //{
        //    var viewModel = new MainWindowViewModel();
        //    viewModel.DisplayText = model.displayText;

        //    // Llamar a un método que realice los cálculos
        //    // ...
        //    return viewModel.DisplayText;
        //}
    }

    public class CalculoModel
    {
        public string displayText { get; set; }
    }
}
