using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SquadPage.Data.Models;
using SquadPage.Shared.DataInterfaces;
using SquadPage.Shared.Models;

namespace SquadPage.Data.DataAccess
{
    public class GameDayDataAccess : IGameDayDataAccess
    {
        private readonly IConfiguration _config;

        public GameDayDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public IEnumerable<GameDayInfo> GetGameDays(long squadId)
        {
            using var conn = new NpgsqlConnection(BuildConString());
            //move to singleton?
            using var db = new NPoco.Database(conn);

            db.Connection?.Open();
            var dtos = db.Fetch<GameDay>()
                .Where(day => day.HomeSquadId == squadId | day.AwaySquadId == squadId);

            db.Connection?.Close();

            if (dtos == null)
            {
                throw new Exception("Unable to retrieve data");
            }

            return GameDaysToGameDayInfos(dtos);
        }

        public string BuildConString()
        {
            var builder = new NpgsqlConnectionStringBuilder();
            builder.Host = _config["Cockroach:Host"];
            builder.Port = int.Parse(_config["Cockroach:Port"]);
            builder.Database = _config["Cockroach:Database"];
            builder.SslMode = SslMode.VerifyFull;
            builder.Options = _config["Cockroach:Options"];
            builder.Username = _config["Cockroach:Username"];
            builder.Password = _config["Cockroach:Password"];

            return builder.ConnectionString;
        }

        private IEnumerable<GameDayInfo> GameDaysToGameDayInfos(IEnumerable<GameDay> gameDays)
        {
            return gameDays.Select(gameDay => gameDay.ToGameDayInfo()).ToList();
        }

        public GameDayInfo GetGameDay(long gameDayId)
        {
            using var conn = new NpgsqlConnection(BuildConString());
            //move to singleton?
            using var db = new NPoco.Database(conn);

            db.Connection?.Open();

            var gameDay = db.Fetch<GameDay>()
                .First(gameDay => gameDay.Id == gameDayId);

            db.Connection?.Close();

            if (gameDay == null)
            {
                throw new Exception("Unable to retrieve data");
            }

            return gameDay.ToGameDayInfo();
        }

        public GameDayDetails GetGameDayDetails(long gameDayId, long squadId)
        {
            var gameDayInfo = GetGameDay(gameDayId);

            var gameDay = new GameDayDetails()
            {
                Id = gameDayInfo.Id,
                AwaySquadId = gameDayInfo.AwaySquadId,
                GameDate = gameDayInfo.GameDate,
                GameLocation = gameDayInfo.GameLocation,
                GameStatus = gameDayInfo.GameStatus,
                GameType = gameDayInfo.GameType,
                HomeSquadId = gameDayInfo.HomeSquadId
            };

            if (gameDay == null)
            {
                throw new Exception("Unable to retrieve data");
            }

            using var conn = new NpgsqlConnection(BuildConString());
            //move to singleton?
            using var db = new NPoco.Database(conn);

            db.Connection?.Open();

            var sql = "SELECT match_results_id FROM match_results WHERE game_day_id = @gameDayId";
            var matchId = db.Fetch<long>(sql, new { gameDayId }).FirstOrDefault(0);
            var gameResults = db
                .Fetch<GameResults>()
                .Where(result => result.MatchResultsId == matchId)
                .ToList();

            gameDay.Result = ConvertGameResultsToGameResultInfo(gameResults, gameDay.HomeSquadId == squadId.ToString());

            db.Connection?.Close();

            return gameDay;
        }

        private MatchResultInfo ConvertGameResultsToGameResultInfo(List<GameResults> gameResults, bool isHome)
        {
            var gameResultInfos = new List<GameResultInfo>();

            if (isHome)
            {
                gameResultInfos.AddRange(gameResults.Select(result =>
                    new GameResultInfo()
                    {
                        OurScore = result.HomeScore,
                        TheirScore = result.AwayScore,
                        Won = result.HomeScore > result.AwayScore

                    }));

                return new MatchResultInfo(gameResultInfos);
            }

            gameResultInfos.AddRange(gameResults.Select(result =>
                new GameResultInfo()
                {
                    OurScore = result.AwayScore,
                    TheirScore = result.HomeScore,
                    Won = result.HomeScore < result.AwayScore
                }));

            return new MatchResultInfo(gameResultInfos);
        }

        public IEnumerable<GameDayDetails> GetGamesDetailsBySquad(long squadId)
        {
            var gameDetails = new List<GameDayDetails>();
            var games = GetGameDays(squadId);

            foreach (var game in games)
            {
                gameDetails.Add(GetGameDayDetails(long.Parse(game.Id), squadId));
            }

            if (gameDetails.Count == 0)
            {
                throw new Exception("Unable to retrieve data");
            }

            return gameDetails;
        }
    }
}
