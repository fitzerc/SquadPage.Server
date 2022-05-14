using Microsoft.AspNetCore.Mvc;
using NPoco;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SquadPage.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SquadController : ControllerBase
    {
        private readonly IConfiguration _config;
        // GET: api/<SquadController>
        public SquadController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public ActionResult<SquadInfo> Get()
        {
            return new Database(_config).GetSquadInfo();
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

    [TableName("squad")]
    [PrimaryKey("squad_id")]
    public class SquadInfo
    {
        [Column("squad_name")]
        public string Name { get; set; }

        [Column("squad_id")]
        public Int64 Id { get; set; }
    }
}
