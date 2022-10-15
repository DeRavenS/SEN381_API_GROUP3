﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SEN381_API_Group3.shared.models;
using SEN381_API_GROUP3.Database;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEN381_API_GROUP3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentController : ControllerBase
    {
        // GET: api/<TreatmentController>
        [HttpGet]
        public List<Treatment> Get(int page, int size)
        {
            int offset = (page - 1) * size;
            List<Treatment> modules = new List<Treatment>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Treatment] ORDER BY TreatmentID OFFSET @offset ROWS FETCH NEXT @size ROWS ONLY;", scon);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@size", size);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new Treatment(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(2), reader.GetInt32(3).ToString()));


                }
            }


            return modules;
        }

        // GET api/<TreatmentController>/5
        [HttpGet("{id}")]
        public List<Treatment> Get(int id)
        {
            List<Treatment> modules = new List<Treatment>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand($@"SELECT * FROM [dbo].[Treatment] where TreatmentID = '{id}'", scon);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new Treatment(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(2), reader.GetInt32(3).ToString()));
                }
            }


            return modules;
        }

        // POST api/<TreatmentController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TreatmentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TreatmentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
