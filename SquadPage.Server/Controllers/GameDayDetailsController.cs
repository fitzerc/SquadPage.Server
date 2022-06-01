using Microsoft.AspNetCore.Mvc;
using SquadPage.Shared.DataInterfaces;
using SquadPage.Shared.Models;

namespace SquadPage.Server.Controllers
{
    [Route("api/[controller]")]
    public class GameDayDetailsController : Controller
    {
        private readonly IGameDayDataAccess _gameDayDataAccess;

        public GameDayDetailsController(IGameDayDataAccess gameDayDataAccess)
        {
            _gameDayDataAccess = gameDayDataAccess;
        }

        [HttpGet]
        [Route("{squadId}/{gameDayId}")]
        public ActionResult<GameDayDetails> GetGamedaySquadPerspective(Int64 squadId, Int64 gameDayId)
        {
            try
            {
                var gameDay = _gameDayDataAccess.GetGameDayDetails(gameDayId, squadId);
                return gameDay;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("{gameDayId}")]
        public ActionResult<GameDayInfo> Get(Int64 gameDayId)
        {
            try
            {
                var gameDay = _gameDayDataAccess.GetGameDay(gameDayId);
                return gameDay;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("all/{squadId}")]
        public ActionResult<IEnumerable<GameDayDetails>> GetAllBySquad(string squadId)
        {
            try
            {
                var squadIdIsInt = long.TryParse(squadId, out var squadIdAsInt);

                if (!squadIdIsInt)
                {
                    return BadRequest("squadId must be an integer");
                }

                return Ok(_gameDayDataAccess.GetGamesDetailsBySquad(squadIdAsInt));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
