using Microsoft.AspNetCore.Mvc;
using SEN381_API_GROUP3.Database;
using SEN381_API_Group3.shared.models;
using System.Data.SqlClient;
using SEN381_API_GROUP3.Services;
using SEN381_API_GROUP3.shared.models;
using System.Security.Cryptography;

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
        public MedicalCondition Get(int id)
        {
            return new MedicalConditionService().getMedicalConditionById(id);
        }

        // POST api/<MedicalConditionController>
        [HttpPost]//, List<string> Tid, int count
        public bool Post([FromBody] InsertMedicalConditionRequest med)
        {
             new MedicalConditionService().addMedicalCondition(med);
            return true;
        }

        // PUT api/<MedicalConditionController>/5
        [HttpPut("{id}")]
        public MedicalCondition Put( int id, MedicalCondition medical)
        {
            return new MedicalConditionService().updateMedicalCondition(id,medical);
        }

        // DELETE api/<MedicalConditionController>/5
        [HttpDelete("{id}")]
        public void Delete( int id)
        {
            new MedicalConditionService().deleteMedicalCondition(id);
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class MedicalConditionByTreatmentController : ControllerBase
    {
        [HttpGet]
        public List<MedicalConditionTreatment> Get(int page, int size)
        {
            return new MedicalConditionService().getAllMedicalConditionsTreatments(page, size);
        }
        [HttpGet("{id}")]
        public List<MedicalConditionTreatment> Get(int id)
        {
            return new MedicalConditionService().getMedicalConditon(id);
        }
        [HttpDelete("{id}")]
        public void Delete(int id,string Tid)
        {
            new MedicalConditionService().deleteMedicalTreatment(id, Tid);
        }
        
    }
}
