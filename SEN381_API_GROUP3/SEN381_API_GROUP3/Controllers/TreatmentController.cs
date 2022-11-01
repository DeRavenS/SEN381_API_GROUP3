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
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentController : ControllerBase
    {
        // GET: api/<TreatmentController>
        [HttpGet]
        public List<Treatment> Get(int page, int size)
        {
            return new TreatmentService().getTreatment(page, size);
        }

        // GET api/<TreatmentController>/5
        [HttpGet("{id}")]
        public Treatment Get(string id)
        {
            return new TreatmentService().getTreatmentsByID(id);
        }

        // POST api/<TreatmentController>
        [HttpPost]
        public Treatment Post([FromBody] Treatment treatment)
        {
            Console.WriteLine("Controller Start");
            return new TreatmentService().addNewTreatment(treatment);

        }

        // PUT api/<TreatmentController>/5
        [HttpPut("{id}")]
        public Treatment Put(string id,[FromBody] Treatment treatment)
        {
            return new TreatmentService().updateTreatment(id, treatment);
        }

        // DELETE api/<TreatmentController>/5
        [HttpDelete("{id}")]
        public Treatment Delete(string id)
        {
           return new TreatmentService().deleteTreatment(id);
        }
    }
}
