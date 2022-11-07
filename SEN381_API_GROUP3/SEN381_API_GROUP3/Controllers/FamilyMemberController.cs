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
        public List<FamilyMember> Get(string clientID)
        {
            return new FamilyMemberService().getAllFamilyMembers(clientID);
        }

        // GET api/<FamilyMemberController>/5
        [HttpGet("{id}")]
        public FamilyMember Get(int id)
        {
            return new FamilyMemberService().getFamilyMemberById(id);
        }

        // POST api/<FamilyMemberController>
        [HttpPost]
        public FamilyMember Post([FromBody] FamilyMember family)
        {
            return new FamilyMemberService().addNewFamilyMember(family);
        }

        // PUT api/<FamilyMemberController>/5
        [HttpPut("{id}")]
        public FamilyMember Put([FromBody] FamilyMember family)
        {
            return new FamilyMemberService().updateFamilyMember(family);
        }

        // DELETE api/<FamilyMemberController>/5
        [HttpDelete("{id}")]
        public FamilyMember Delete(int id)
        {
            return new FamilyMemberService().deleteFamilyMember(id);
        }
    }
}
