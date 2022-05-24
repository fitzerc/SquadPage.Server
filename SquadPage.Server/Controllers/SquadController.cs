using Microsoft.AspNetCore.Mvc;
using SquadPage.Shared.DataInterfaces;
using SquadPage.Shared.Models;

namespace SquadPage.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SquadController : ControllerBase
    {
        private readonly IDataAccess _dataAccess;

        // GET: api/<SquadController>
        public SquadController(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        [HttpGet]
        [Route("/squad")]
        public ActionResult<SquadInfoResp> Get(Int64 squadId)
        {
            try
            {
                return _dataAccess.GetSquadInfo();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<SquadInfoResp>> Get()
        {
            try
            {
                return Ok(_dataAccess.GetSquads());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
