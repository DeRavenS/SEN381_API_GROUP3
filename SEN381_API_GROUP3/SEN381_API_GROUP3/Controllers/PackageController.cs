using Microsoft.AspNetCore.Mvc;
using SEN381_API_Group3.shared.models;
using SEN381_API_GROUP3.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEN381_API_GROUP3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        // GET: api/<PackageController>
        [HttpGet]
        public List<Package> Get(int page, int size)
        {
            return new PackageService().getPackageList(page,size);
        }

        // GET api/<PackageController>/5
        [HttpGet("{id}")]
        public Package Get(string id)
        {
            return new PackageService().getPackageByID(int.Parse(id));
        }

        // POST api/<PackageController>
        [HttpPost]
        public Package Post([FromBody] Package package)
        {
            Console.WriteLine("Start POST");
            return new PackageService().addPackage(package);
        }

        // PUT api/<PackageController>/5
        [HttpPut("{id}")]
        public Package Put(string id, [FromBody] Package package)
        {
            return new PackageService().updatePackage(package);
        }

        // DELETE api/<PackageController>/5
        [HttpDelete("{id}")]
        public Package Delete(string id)
        {
            return new PackageService().deletePackage(int.Parse(id));
        }
    }
}
