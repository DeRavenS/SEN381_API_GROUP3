using Microsoft.AspNetCore.Mvc;
using SEN381_API_Group3.shared.models;
using SEN381_API_GROUP3.Database;
using SEN381_API_GROUP3.Services;
using System.Data.SqlClient;
using static DevExpress.Xpo.DB.DataStoreLongrunnersWatch;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEN381_API_GROUP3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        // GET: api/<ClaimsController>
        [HttpGet]
        public List<Claim> Get(int page, int size)
        {
            return new ClaimService().getClaims(page, size);
        }

        // GET api/<ClaimsController>/5
        [HttpGet("{id}")]
        public List<Claim> Get(int id)
        {
            return new ClaimService().getClaimById(id);
        }

        // POST api/<ClaimsController>
        [HttpPost]
        public void Post(int ClientID, int Medicalcondition, string PlaceOfTreament, int CallID, string ClaimeStatus)
        {
             new ClaimService().addNewClaim(ClientID, Medicalcondition, PlaceOfTreament, CallID, ClaimeStatus);
        }

        // PUT api/<ClaimsController>/5
        [HttpPut("{id}")]
        public void Put(int id, int ClientID, int MedicalCondition, string PlaceOfTreament, int CallID, string ClaimeStatus)
        {
            new ClaimService().UpdateClaim(id, ClientID,  MedicalCondition,  PlaceOfTreament,  CallID,  ClaimeStatus);
        }

        // DELETE api/<ClaimsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            new ClaimService().deleteClaim(id);
        }
    }
}
