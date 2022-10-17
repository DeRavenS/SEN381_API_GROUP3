using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SEN381_API_Group3.shared.models;
using SEN381_API_GROUP3.Database;
using SEN381_API_GROUP3.Services;
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
        public List<MedicalServiceProvider> Get(int page, int size)
        {
            return MedicalServiceProviderService.getProviderList(page, size);
        }

        // GET api/<MedicalServiceProviderController>/5
        [HttpGet("{id}")]
        public MedicalServiceProvider Get(int id)
        {
            return MedicalServiceProviderService.getProviderByID(id);
        }

        // POST api/<MedicalServiceProviderController>
        [HttpPost]
        public void Post(string MedicalServiceProvidorName, string MedicalServiceProvidorAddress, string MedicalServiceProvidorEmail, string MedicalServiceProvidorPhone)
        {
            new MedicalServiceProviderService().addMedicalServiceProvider( MedicalServiceProvidorName,  MedicalServiceProvidorAddress,  MedicalServiceProvidorEmail,  MedicalServiceProvidorPhone);
        }

        // PUT api/<MedicalServiceProviderController>/5
        [HttpPut("{id}")]
        public void Put(int id, string MedicalServiceProvidorName, string MedicalServiceProvidorAddress, string MedicalServiceProvidorEmail, string MedicalServiceProvidorPhone)
        {
            new MedicalServiceProviderService().updateMedicalServiceProvider( id,  MedicalServiceProvidorName,  MedicalServiceProvidorAddress,  MedicalServiceProvidorEmail,  MedicalServiceProvidorPhone);
        }

        // DELETE api/<MedicalServiceProviderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            new MedicalServiceProviderService().deleteMedicalServiceProvider(id);
        }
    }
}
