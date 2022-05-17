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
        public ActionResult<SquadInfoResp> Get()
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
    }
}
