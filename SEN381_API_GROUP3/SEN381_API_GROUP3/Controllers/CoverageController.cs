using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SEN381_API_GROUP3.Database;
using SEN381_API_Group3.shared.models;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEN381_API_GROUP3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoverageController : ControllerBase
    {
        // GET: api/<CoverageController>
        [HttpGet]
        public List<TreatmentCoverage> Get(int page, int size)
        {
            int offset = (page - 1) * size;
            List<TreatmentCoverage> modules = new List<TreatmentCoverage>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Coverage] ORDER BY CoverageID OFFSET @offset ROWS FETCH NEXT @size ROWS ONLY;", scon);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@size", size);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read()) // Kyk of die coverage silver, gold, platinum is en maak n class van wat hy is 
                {
                    if (reader.GetString(1) == "Silver") {
                        modules.Add(new SilverCoverage(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4)));
                    }
                    else if (reader.GetString(1) == "Gold") {
                        modules.Add(new GoldCoverage(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4)));
                    }
                    else if(reader.GetString(1) == "Platinum")
                    {
                        modules.Add(new PlatinumCoverage(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4)));

                    }
                }
            }


            return modules;
        }

        // GET api/<CoverageController>/5
        [HttpGet("{id}")]
        public List<TreatmentCoverage> Get(int id)
        {
            List<TreatmentCoverage> modules = new List<TreatmentCoverage>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            // Select * from Client where ClientID = 1
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Coverage] where CoverageID = " + id, scon);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read()) // Kyk of die coverage silver, gold, platinum is en maak n class van wat hy is 
                {
                    if (reader.GetString(1) == "Silver")
                    {
                        modules.Add(new SilverCoverage(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4)));
                    }
                    else if (reader.GetString(1) == "Gold")
                    {
                        modules.Add(new GoldCoverage(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4)));
                    }
                    else if (reader.GetString(1) == "Platinum")
                    {
                        modules.Add(new PlatinumCoverage(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4)));

                    }
                }
            }


            return modules;
        }

        // POST api/<CoverageController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CoverageController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CoverageController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
