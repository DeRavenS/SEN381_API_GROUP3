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
    public class FamilyMemberController : ControllerBase
    {
        // GET: api/<FamilyMemberController>
        [HttpGet]
        public List<FamilyMember> Get(int page, int size)
        {
            return new FamilyMemberService().getAllFamilyMembers(page, size);
        }

        // GET api/<FamilyMemberController>/5
        [HttpGet("{id}")]
        public List<FamilyMember> Get(int id)
        {
            return new FamilyMemberService().getFamilyMemerById(id);
        }

        // POST api/<FamilyMemberController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FamilyMemberController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FamilyMemberController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
