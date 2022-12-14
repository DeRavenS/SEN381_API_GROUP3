using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SEN381_API_GROUP3.Services;
using SEN381_API_GROUP3.shared.models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEN381_API_GROUP3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginRegController : ControllerBase
    {
        //// GET: api/<LoginController>
        //[HttpGet]
        //public bool Get(EmployeeDetails employee)
        //{
        //    return new LoginRegService().GetEmployeeDetails(employee);

        //}

        // GET api/<LoginController>/5
        [HttpGet("login")]
        public EmployeeDetails? Get(string email, string password, string name)
        {
            Console.WriteLine("Login");
            return new LoginRegService().GetEmployeeByInput(email, password, name);
        }

        // POST api/<LoginController>
        [HttpPost]
        public EmployeeDetails Post([FromBody] EmployeeDetails values)
        {
            return new LoginRegService().addEmployee(values);
        }

        // PUT api/<LoginController>/5
        [HttpPut("{email}")]
        public EmployeeDetails Put( [FromBody] EmployeeDetails value)
        {
            return new LoginRegService().updateEmployee(value);
        }

        // DELETE api/<LoginController>/5
        [HttpDelete("{email}")]
        public bool Delete(string email)
        {
             new LoginRegService().removeEmployee(email);
            return true;
        }
    }
}
