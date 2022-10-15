using Microsoft.AspNetCore.Mvc;
using SEN381_API_GROUP3.Database;
using SEN381_API_Group3.shared.models;
using System.Data.SqlClient;
using SEN381_API_GROUP3.Services;


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
            return new MedicalConditionService().getAllMedicalConditions(page, size);
        }


        // GET api/<MedicalConditionController>/5
        [HttpGet("{id}")]
        public List<MedicalCondition> Get(int id)
        {
            return new MedicalConditionService().getMedicalConditionById(id);
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
