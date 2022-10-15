using DevExpress.Utils.CommonDialogs.Internal;
using Microsoft.AspNetCore.Mvc;
using SEN381_API_Group3.shared.models;
using SEN381_API_GROUP3.Database;
using SEN381_API_GROUP3.Services;
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
        [HttpGet(Name = "Client Controller")]
        public List<Client> Get(int page, int size)
        {
<<<<<<< HEAD
            return new ClientService().getClients(page, size);
=======
            int offset = (page - 1) * size;
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
>>>>>>> 0e89060509f7247b65051d8edf5ea8b66c16487f
        }


        // GET api/<ClientController>/5
        [HttpGet("{id:int}")]
        public List<Client> Get(int id)
        {
            return new ClientService().getClientById(id);
        }


        // POST api/<ClientController>
        [HttpPost]
        public void Post(string ClientName, string ClientAdress, string ClientEmail)
        {
            new ClientService().addNewClient(ClientName, ClientAdress, ClientEmail);

        }


        // PUT api/<ClientController>/5
        [HttpPut("{id:int}")]
        public void Put(int id, string ClientName, string ClientAdress, string ClientEmail)
        {
            new ClientService().updateClient(id, ClientName, ClientAdress, ClientEmail);
        }

        // DELETE api/<ClientController>/5
        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
            new ClientService().deleteClient(id);
        }
    }
}
