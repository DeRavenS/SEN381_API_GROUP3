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
        public void Post([FromBody] string FamilyMemberName, string FamilyMemberSurname, string FamilyMemberPhone, string FamilyMemberEmail, string FamilyMemberAddress, string FamilyIDnumber, string FamilyRole, string ClientID)
        {
            new FamilyMemberService().addNewFamilyMemer( FamilyMemberName,  FamilyMemberSurname,  FamilyMemberPhone,  FamilyMemberEmail,  FamilyMemberAddress,  FamilyIDnumber,  FamilyRole,  ClientID);
        }

        // PUT api/<FamilyMemberController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] int id, string FamilyMemberName, string FamilyMemberSurname, string FamilyMemberPhone, string FamilyMemberEmail, string FamilyMemberAddress, string FamilyIDnumber, string FamilyRole, string ClientID)
        {
            new FamilyMemberService().updateFamilyMemer(id,FamilyMemberName, FamilyMemberSurname, FamilyMemberPhone, FamilyMemberEmail, FamilyMemberAddress, FamilyIDnumber, FamilyRole, ClientID);
        }

        // DELETE api/<FamilyMemberController>/5
        [HttpDelete("{id}")]
        public void Delete([FromBody] int id)
        {
            new FamilyMemberService().deleteFamilyMember(id);
        }
    }
}
