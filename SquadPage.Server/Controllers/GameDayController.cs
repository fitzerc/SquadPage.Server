using Microsoft.AspNetCore.Mvc;
using SquadPage.Shared.DataInterfaces;
using SquadPage.Shared.Models;

namespace SquadPage.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameDayController : ControllerBase
    {
        private readonly IDataAccess _dataAccess;

        public GameDayController(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // GET: GameDayController
        [HttpGet]
        [Route("{squadId}")]
        public ActionResult<IEnumerable<GameDayInfo>> Get(Int64 squadId)
        {
            try
            {
                var gameDays = _dataAccess.GetGameDays(squadId);
                return Ok(gameDays);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    /*
        // GET: GameDayController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GameDayController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GameDayController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GameDayController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GameDayController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GameDayController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GameDayController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    */
    }
}
