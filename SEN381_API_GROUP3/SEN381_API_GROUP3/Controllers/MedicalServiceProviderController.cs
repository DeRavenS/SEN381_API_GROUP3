using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using SEN381_API_GROUP3.Database;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEN381_API_GROUP3.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class MedicalServiceProviderController : ControllerBase
    {
        // GET: api/<MedicalServiceProviderController>
        [HttpGet]
        public IEnumerable<MedicalServiceProvider> Get(int page, int size)
        {
            int offset = page * size;
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[MedicalServiceProvider]" +
                                                "ORDER BY [MedicalServiceProvidorID]" +
                                                "OFFSET @offset ROWS " +
                                                "FETCH NEXT @size ROWS ONLY;", scon);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@size", size);
            SqlDataReader read = command.ExecuteReader();

            List<MedicalServiceProvider> providers = new List<MedicalServiceProvider>();
            while (read.Read())
            {
                providers.Add(new MedicalServiceProvider(read.GetInt32(0).ToString(),read.GetString(1),read.GetString(2),read.GetString(3)));
            }

            scon.Close();
            return providers;
            
            
            
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
