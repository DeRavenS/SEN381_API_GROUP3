using DevExpress.Utils.CommonDialogs.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
           return new ClientService().getClients(page, size);
        }



        // GET api/<ClientController>/5
        [HttpGet("{id}")]
        public Client Get(string id)
        {
            return new ClientService().getClientById(id);
        }


        // POST api/<ClientController>
        [HttpPost]
        public Client Post([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] Client client)
        {
            Console.WriteLine("Insert controller");
            return new ClientService().addNewClient(client);

        }


        // PUT api/<ClientController>/5
        [HttpPut("{id}")]
        public Client Put(string id,[FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] Client client)
        {
            
            return new ClientService().updateClient(id, client);
        }

        // DELETE api/<ClientController>/5
        [HttpDelete("{id}")]
        public Client Delete(string id)
        {
            return new ClientService().deleteClient(id);
        }
    }
}
