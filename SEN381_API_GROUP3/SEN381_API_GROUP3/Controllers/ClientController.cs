using DevExpress.Utils.CommonDialogs.Internal;
using Microsoft.AspNetCore.Mvc;
using SEN381_API_Group3.shared.models;
using SEN381_API_GROUP3.Database;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Reflection;
using System.Reflection.PortableExecutable;
using Xceed.Wpf.Toolkit;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEN381_API_GROUP3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        // GET: api/<ClientController>
        [HttpGet]
        public List<Client> Get(int page, int size)
        {
            int offset = page * size;
            List<Client> modules = new List<Client>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Client] ORDER BY ClientID OFFSET @offset ROWS FETCH NEXT @size ROWS ONLY;", scon);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@size", size);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new Client(reader.GetInt32(0).ToString(),reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8)));


                }
            }


            return modules;
        }

        // GET api/<ClientController>/5
        [HttpGet("{id:int}")]
        public List<string> Get(int id)
        {
            List<string> modules = new List<string>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            // Select * from Client where ClientID = 1
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Client] where ClientID = "+ id, scon);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(reader.GetValue(1).ToString());
                    modules.Add(reader.GetValue(2).ToString());
                    modules.Add(reader.GetValue(3).ToString());

                }
            }


            return modules;
            scon.Close();
        }

        // POST api/<ClientController>
        [HttpPost]
        public void Post(string ClientName, string ClientAdress, string ClientEmail)
        {
            string query = $@"INSERT INTO Client (ClientName, ClientAddress,clientEmail)VALUES('{ClientName}', '{ClientAdress}', '{ClientEmail}')";

            SqlParameter Clientname = new SqlParameter("@ClientName", SqlDbType.VarChar);
            SqlParameter Clientadress = new SqlParameter("@ClientAdress", SqlDbType.VarChar);
            SqlParameter Clientemail = new SqlParameter("@ClientEmail", SqlDbType.VarChar);

            Clientname.Value = ClientName.ToString();
            Clientadress.Value = ClientAdress.ToString();
            Clientemail.Value = ClientEmail.ToString();

            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();


            SqlCommand insertCommand = new SqlCommand(query, scon);
            insertCommand.Parameters.Add(Clientname);
            insertCommand.Parameters.Add(Clientadress);
            insertCommand.Parameters.Add(Clientemail);

            insertCommand.ExecuteNonQuery();
            scon.Close();

        }

        // PUT api/<ClientController>/5
        [HttpPut("{id:int}")]
        public void Put(int id, string ClientName, string ClientAdress, string ClientEmail)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = $@"Update Client set ClientName = '{ClientName}', ClientAddress = '{ClientAdress}', clientEmail = '{ClientEmail}' WHERE ClientID = '{id}'";
            SqlParameter Clientname = new SqlParameter("@ClientName", SqlDbType.VarChar);
            SqlParameter Clientadress = new SqlParameter("@ClientAdress", SqlDbType.VarChar);
            SqlParameter Clientemail = new SqlParameter("@ClientEmail", SqlDbType.VarChar);
            Clientname.Value = ClientName.ToString();
            Clientadress.Value = ClientAdress.ToString();
            Clientemail.Value = ClientEmail.ToString();
            SqlCommand updateCommand = new(query, scon);
            updateCommand.Parameters.Add(Clientname);
            updateCommand.Parameters.Add(Clientadress);
            updateCommand.Parameters.Add(Clientemail);

            updateCommand.ExecuteNonQuery();
            scon.Close();
        }

        // DELETE api/<ClientController>/5
        [HttpDelete("{id:int}")]
        public void Delete(int id )
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = $@"DELETE from Client WHERE ClientID = '{id}'";
            SqlCommand com = new SqlCommand(query, scon);
            com.ExecuteNonQuery();
            scon.Close();
        }
    }
}
