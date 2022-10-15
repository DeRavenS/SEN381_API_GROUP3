using Microsoft.AspNetCore.Mvc;
using SEN381_API_GROUP3.Database;
using SEN381_API_Group3.shared.models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.RazorPages;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEN381_API_GROUP3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalConditionController : ControllerBase
    {
        // GET: api/<MedicalConditionController>
        [HttpGet]
        public List<MedicalCondition> Get(int page, int size)
        {
            int offset = (page-1) * size;
            List<MedicalCondition> modules = new List<MedicalCondition>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[MedicalCondition] ORDER BY MedicalConditionID OFFSET @offset ROWS FETCH NEXT @size ROWS ONLY;", scon);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@size", size);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new MedicalCondition(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(2), reader.GetInt32(3).ToString()));


                }
            }


            return modules;
        }

        // GET api/<MedicalConditionController>/5
        [HttpGet("{id}")]
        public List<MedicalCondition> Get(int id)
        {
            List<MedicalCondition> modules = new List<MedicalCondition>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand($@"SELECT * FROM [dbo].[MedicalCondition] where MedicalConditionID = '{id}'", scon);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new MedicalCondition(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(2), reader.GetInt32(3).ToString()));
                }
            }


            return modules;
        }

        // POST api/<MedicalConditionController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MedicalConditionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MedicalConditionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
