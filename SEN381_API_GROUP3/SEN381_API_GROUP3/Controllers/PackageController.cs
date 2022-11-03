using Microsoft.AspNetCore.Mvc;
using SEN381_API_Group3.shared.models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEN381_API_GROUP3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        // GET: api/<PackageController>
        [HttpGet]
        public List<PackageList> Get(int page, int size)
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PackageController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PackageController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PackageController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PackageController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
