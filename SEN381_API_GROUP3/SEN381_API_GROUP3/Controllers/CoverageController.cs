using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SEN381_API_GROUP3.Database;
using SEN381_API_Group3.shared.models;
using System.Data.SqlClient;
using SEN381_API_GROUP3.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEN381_API_GROUP3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoverageController : ControllerBase
    {
        // GET: api/<CoverageController>
        [HttpGet]
        public List<TreatmentCoverage> Get(int page, int size)
        {
            return new CoverageService().getCoverageList(page, size);
        }

        // GET api/<CoverageController>/5
        [HttpGet("{id}")]
        public TreatmentCoverage Get(int id)
        {
            return new CoverageService().getCoverageByID(id);
        }

        // POST api/<CoverageController>
        [HttpPost]
        public TreatmentCoverage Post([FromBody] TreatmentCoverage coverage)
        {
            return new CoverageService().insertCoverage(coverage);
        }

        // PUT api/<CoverageController>/5
        [HttpPut("{id}")]
        public TreatmentCoverage Put(int id, [FromBody] TreatmentCoverage coverage)
        {
            return new CoverageService().updateCoverage(id, coverage);
        }

        // DELETE api/<CoverageController>/5
        [HttpDelete("{id}")]
        public TreatmentCoverage? Delete(string id)
        {
            return new CoverageService().deleteCoverage(int.Parse(id));
        }
    }
}
