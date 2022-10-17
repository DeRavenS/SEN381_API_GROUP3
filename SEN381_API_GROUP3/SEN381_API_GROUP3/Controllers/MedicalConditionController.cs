using Microsoft.AspNetCore.Mvc;
using SEN381_API_GROUP3.Database;
using SEN381_API_Group3.shared.models;
using System.Data.SqlClient;
using SEN381_API_GROUP3.Services;



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
        public void Post([FromBody] string MedicalConditionName, string MedicalConditionDescription, int TreatmentID)
        {
            new MedicalConditionService().addMedicalCondition(MedicalConditionName,MedicalConditionDescription, TreatmentID);
        }

        // PUT api/<MedicalConditionController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] int id, string MedicalConditionName, string MedicalConditionDescription, int TreatmentID)
        {
            new MedicalConditionService().updateMedicalCondition(id, MedicalConditionName, MedicalConditionDescription, TreatmentID);
        }

        // DELETE api/<MedicalConditionController>/5
        [HttpDelete("{id}")]
        public void Delete([FromBody] int id)
        {
            new MedicalConditionService().deleteMedicalCondition(id);
        }
    }
}
