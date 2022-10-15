using Microsoft.AspNetCore.Mvc;
using SEN381_API_Group3.shared.models;
using SEN381_API_GROUP3.Database;
using SEN381_API_GROUP3.Services;
using System.Data;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEN381_API_GROUP3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallController : ControllerBase
    {
        // GET: api/<CallController>
        [HttpGet]
        public List<CallDetails> Get(int page, int size)
        {
           return new CallService().getAllCallDetails(page, size);
        }

        // GET api/<CallController>/5
        [HttpGet("{id:int}")]
        public List<CallDetails> Get(int id)
        {
           return new CallService().getCallById(id);
        }

        // POST api/<CallController>
        [HttpPost]
        public void Post( string StartTime, string EndTime)
        {
            new CallService().addNewCall(StartTime, EndTime);
        }

        // PUT api/<CallController>/5
        [HttpPut("{id:int}")]
        public void Put(int id, string StartTime, string EndTime)
        {
            new CallService().updateCall(id, StartTime, EndTime);
        }

        // DELETE api/<CallController>/5
        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
            new CallService().deleteCall(id);
        }
    }
}
