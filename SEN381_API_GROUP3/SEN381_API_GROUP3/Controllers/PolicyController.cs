using Microsoft.AspNetCore.Mvc;
using SEN381_API_Group3.shared.models;
using SEN381_API_GROUP3.Services;
using SEN381_API_GROUP3.shared.requests;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEN381_API_GROUP3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        // GET: api/<PolicyController>
        [HttpGet]
        public List<Policy> Get(int size, int page)
        {
            return PolicyService.getPolicyList(size,page);
        }


        // GET api/<PolicyController>/5
        [HttpGet("{id}")]
        public Policy Get(int id)
        {
            return PolicyService.getPolicyByID(id.ToString());
        }

        // POST api/<PolicyController>
        [HttpPost]
        public Policy Post([FromBody] Policy req)
        {
            return new PolicyService().createPolicy(req);
        }

        // PUT api/<PolicyController>/5
        [HttpPut("{id}")]
        public Policy Put(int id, [FromBody] Policy policy)
        {
            return new PolicyService().updatePolicy(id,policy);
        }

        // DELETE api/<PolicyController>/5
        [HttpDelete("{id}")]
        public Policy Delete(int id)
        {
            return new PolicyService().deletePolicy(id);
        }
    }
}
