using Microsoft.AspNetCore.Mvc;
using SquadPage.Shared.DataInterfaces;
using SquadPage.Shared.Models;

namespace SquadPage.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SquadController : ControllerBase
    {
        private readonly ISquadDataAccess _squadDataAccess;

        // GET: api/<SquadController>
        public SquadController(ISquadDataAccess squadDataAccess)
        {
            _squadDataAccess = squadDataAccess;
        }

        [HttpGet]
        [Route("bySquadNumber/{squadNumber}")]
        public ActionResult<SquadInfoResp> GetBySquadNum(int squadNumber)
        {
            try
            {
                return _squadDataAccess.GetSquadInfoByNumber(squadNumber);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("{squadId}")]
        public ActionResult<SquadInfoResp> Get(Int64 squadId)
        {
            try
            {
                return _squadDataAccess.GetSquadInfo(squadId);
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
                return Ok(_squadDataAccess.GetSquads());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
