using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
//using ParteVisualCalculadora.ViewModels;


namespace ApiCalculadora.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperacionesController : ControllerBase
    {
        //[HttpPost("Calculos")]
        //public async Task<ActionResult<string>> Calculos([FromBody] MainWindowViewModel viewModel)
        //{
        //    // Get the value of the DisplayText property from the ViewModel
        //    string displayText = viewModel.DisplayText;

        //    // Perform your calculations here
        //    var result = new DataTable().Compute(displayText, null);
        //    //double result = Math.PI; // Replace this with your actual calculation

        //    // Store the operation in the database
        //    string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=OperationsCollection; Integrated Security=True;";
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        string insertQuery = "INSERT INTO Operations (DisplayText, Result) VALUES (@displayText, @result);";
        //        using (SqlCommand command = new SqlCommand(insertQuery, connection))
        //        {
        //            command.Parameters.AddWithValue("@displayText", displayText);
        //            command.Parameters.AddWithValue("@result", result);
        //            await command.ExecuteNonQueryAsync();
        //        }
        //    }

        //    // Return the result to the client
        //    return Ok(result);
        //}
    }
}
