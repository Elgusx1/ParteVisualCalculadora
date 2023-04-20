using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ApiCalculadora.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetResultController : ControllerBase
    {
        [HttpGet]
        public string GetResult()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=OperationsCollection; Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT Operacion FROM OperacionAlmacenada ORDER BY OperationID DESC";
                SqlCommand command = new SqlCommand(selectQuery, connection);
                string operacion = command.ExecuteScalar().ToString();

                string result = new DataTable().Compute(operacion, null).ToString();
                connection.Close();
                return result;
               
               
            }
        }
    }
}
