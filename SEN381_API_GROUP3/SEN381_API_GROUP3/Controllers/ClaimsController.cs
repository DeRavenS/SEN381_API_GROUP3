using Microsoft.AspNetCore.Mvc;
using SEN381_API_Group3.shared.models;
using SEN381_API_GROUP3.Database;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEN381_API_GROUP3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        // GET: api/<ClaimsController>
        [HttpGet]
        public List<Claim> Get(int page, int size)
        {
            int offset = (page - 1) * size;
            List<Claim> modules = new List<Claim>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Claim] ORDER BY CLAIMID OFFSET @offset ROWS FETCH NEXT @size ROWS ONLY;", scon);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@size", size);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new Claim(reader.GetInt32(0).ToString(), reader.GetInt32(1).ToString(), reader.GetInt32(2).ToString(), reader.GetString(3), reader.GetInt32(4).ToString(), reader.GetString(5)));


                }
            }


            return modules;
        }

        // GET api/<ClaimsController>/5
        [HttpGet("{id}")]
        public List<Claim> Get(int id)
        {
            List<Claim> modules = new List<Claim>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand($@"SELECT * FROM [dbo].[Claim] where CLAIMID = '{id}'", scon);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new Claim(reader.GetInt32(0).ToString(), reader.GetInt32(1).ToString(), reader.GetInt32(2).ToString(), reader.GetString(3), reader.GetInt32(4).ToString(), reader.GetString(5)));
                }
            }


            return modules;
        }

        // POST api/<ClaimsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ClaimsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClaimsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
