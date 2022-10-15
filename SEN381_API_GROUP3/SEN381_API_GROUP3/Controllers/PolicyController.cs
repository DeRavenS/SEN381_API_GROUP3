using Microsoft.AspNetCore.Mvc;
using SEN381_API_Group3.shared.models;
using SEN381_API_GROUP3.Services;

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
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PolicyController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PolicyController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
