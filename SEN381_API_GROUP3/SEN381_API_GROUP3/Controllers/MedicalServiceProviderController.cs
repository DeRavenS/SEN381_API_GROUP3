using Microsoft.AspNetCore.Mvc;
using SEN381_API_GROUP3.Database;
using System.Data;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEN381_API_GROUP3.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class MedicalServiceProviderController : ControllerBase
    {
        // GET: api/<MedicalServiceProviderController>
        [HttpGet]
        public IEnumerable<string> Get()
        {   
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[MedicalServiceProvider]", scon);
            SqlDataReader read = command.ExecuteReader();

            string temp = "";

            while (read.Read())
            {
                Console.WriteLine(read.GetInt32(0));
                temp = read.GetString(1);
            }

            scon.Close();
            return new string[] { temp, "value2" };
        }

        // GET api/<MedicalServiceProviderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MedicalServiceProviderController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MedicalServiceProviderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MedicalServiceProviderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
