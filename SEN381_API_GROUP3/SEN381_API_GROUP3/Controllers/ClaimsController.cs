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
        public Claim Get(int id)
        {
            return new ClaimService().getClaimById(id);
        }

        // POST api/<ClaimsController>
        [HttpPost]
        public void Post(Claim claim)
        {
             new ClaimService().addNewClaim(claim);
        }

        // PUT api/<ClaimsController>/5
        [HttpPut("{id}")]
        public void Put(int id, Claim claim)
        {
            new ClaimService().UpdateClaim(id, claim);
        }

        // DELETE api/<ClaimsController>/5
        [HttpDelete("{id}")]
        public void Delete( int id)
        {
            new ClaimService().deleteClaim(id);
        }
    }
}
