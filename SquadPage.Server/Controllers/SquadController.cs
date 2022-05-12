using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SquadPage.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SquadController : ControllerBase
    {
        // GET: api/<SquadController>
        [HttpGet]
        public ActionResult<SquadInfo> Get()
        {
            SquadInfo info = new SquadInfo(){ Name = "Test From Api" };
            return info;
        }

        /*
        // GET api/<SquadController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SquadController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SquadController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SquadController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }

    public class SquadInfo
    {
        public string Name { get; set; }
    }
}
