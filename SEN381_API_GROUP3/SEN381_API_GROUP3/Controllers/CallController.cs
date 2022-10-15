using Microsoft.AspNetCore.Mvc;
using SEN381_API_Group3.shared.models;
using SEN381_API_GROUP3.Database;
using System.Data;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEN381_API_GROUP3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallController : ControllerBase
    {
        // GET: api/<CallController>
        [HttpGet]
        public List<CallDetails> Get(int page, int size)
        {
            int offset = (page - 1) * size;

            List<CallDetails> modules = new List<CallDetails>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            SqlCommand command = new SqlCommand($@"SELECT * FROM [dbo].[CallDetails] ORDER BY CALLID OFFSET @offset ROWS FETCH NEXT @size ROWS ONLY;" , scon);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@size", size);
            SqlDataReader reader = command.ExecuteReader();
            
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    modules.Add(new CallDetails(reader.GetInt32(0).ToString(), reader.GetDateTime(1), reader.GetDateTime(2)));


                }
            }


            return modules;
        }

        // GET api/<CallController>/5
        [HttpGet("{id:int}")]
        public List<CallDetails> Get(int id)
        {
            List<CallDetails> modules = new List<CallDetails>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand($@"SELECT * FROM [dbo].[CallDetails] where CALLID = '{id}'" , scon);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new CallDetails(reader.GetInt32(0).ToString(), reader.GetDateTime(1), reader.GetDateTime(2)));
                }
            }


            return modules;
        }

        // POST api/<CallController>
        [HttpPost]
        public void Post( string StartTime, string EndTime)
        {
            string query = $@"INSERT INTO CallDetails (startTime, endTime)VALUES('{StartTime}', '{EndTime}')";

            SqlParameter starttime = new SqlParameter("@StartTime", SqlDbType.VarChar);
            SqlParameter endtime = new SqlParameter("@EndTime", SqlDbType.VarChar);

            starttime.Value = StartTime.ToString();
            endtime.Value = EndTime.ToString();


            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();


            SqlCommand insertCommand = new SqlCommand(query, scon);
            insertCommand.Parameters.Add(starttime);
            insertCommand.Parameters.Add(endtime);

            insertCommand.ExecuteNonQuery();
            scon.Close();
        }

        // PUT api/<CallController>/5
        [HttpPut("{id:int}")]
        public void Put(int id, string StartTime, string EndTime)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = $@"Update CallDetails set startTime = '{StartTime}', endTime = '{EndTime}' WHERE CALLID = '{id}'";
            SqlParameter Starttime = new SqlParameter("@ClientName", SqlDbType.VarChar);
            SqlParameter Endtime = new SqlParameter("@ClientAdress", SqlDbType.VarChar);
            
            Starttime.Value = StartTime.ToString();
            Endtime.Value = EndTime.ToString();
            SqlCommand updateCommand = new(query, scon);
            updateCommand.Parameters.Add(Starttime);
            updateCommand.Parameters.Add(Endtime);


            updateCommand.ExecuteNonQuery();
            scon.Close();
        }

        // DELETE api/<CallController>/5
        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = $@"DELETE from CallDetails WHERE CALLID = '{id}'";
            SqlCommand com = new SqlCommand(query, scon);
            com.ExecuteNonQuery();
            scon.Close();
        }
    }
}
