using System.Globalization;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SquadPage.Data.Models;
using SquadPage.Shared.DataInterfaces;
using SquadPage.Shared.Models;

namespace SquadPage.Data
{
    public class DataAccess : IDataAccess
    {
        private readonly IConfiguration _config;

        public DataAccess(IConfiguration config)
        {
            _config = config;
        }

        public SquadInfoResp GetSquadInfo(Int64 squadId)
        {
            using var conn = new NpgsqlConnection(BuildConString());
            //move to singleton?
            using var db = new NPoco.Database(conn);

            db.Connection?.Open();
            var dto = db.Fetch<Squad>().First(squad => squad.Id == squadId);

            db.Connection?.Close();

            if (dto == null)
            {
                throw new Exception("Unable to retrieve data");
            }

            var squad = dto.ToSquadInfoResp();
            squad.Record.AddRecord(GetSquadRecord(squadId));

            return squad;
        }

        public IEnumerable<SquadInfoResp> GetSquads()
        {
            using var conn = new NpgsqlConnection(BuildConString());
            //move to singleton?
            using var db = new NPoco.Database(conn);
            db.Connection?.Open();

            var squads = db.Fetch<Squad>().ToList();

            db.Connection?.Close();

            if (!squads.Any())
            {
                throw new Exception("Unable to retrieve data");
            }

            var responseSquads = squads.Select(squad =>
            {
                var squadResp = squad.ToSquadInfoResp();


                var idIsInteger = Int64.TryParse(squadResp.Id, out var squadIdAsInt);
                if (!idIsInteger)
                {
                    throw new Exception("Squad Id must be an integer");
                }

                squadResp.Record = GetSquadRecord(squadIdAsInt);
                return squadResp;
            });

            return responseSquads;
        }

        public SquadInfoResp GetSquadInfoByNumber(int squadNumber)
        {
            using var conn = new NpgsqlConnection(BuildConString());
            //move to singleton?
            using var db = new NPoco.Database(conn);
            db.Connection?.Open();

            var squad = db.Fetch<Squad>().FirstOrDefault(squad => squad.Number == squadNumber, null);
            db.Connection?.Close();

            if (squad == null)
            {
                throw new Exception("Unable to retrieve data");
            }


            var squadInfo = squad.ToSquadInfoResp();

            var idIsInteger = Int64.TryParse(squadInfo.Id, out var squadIdAsInt);

            if (!idIsInteger)
            {
                throw new Exception("Squad Id must be an integer");
            }

            squadInfo.Record.AddRecord(GetSquadRecord(squadIdAsInt));

            return squadInfo;
        }

        private SquadRecord GetSquadRecord(Int64 squadId)
        {
            using var conn = new NpgsqlConnection(BuildConString());
            //move to singleton?
            using var db = new NPoco.Database(conn);
            db.Connection?.Open();

            var record = new SquadRecord(0, 0, 0);
            var gameDays = GetGameDays(squadId);

            foreach (var gameDay in gameDays.Where(gameDay => gameDay.GameStatus == "Played"))
            {
                var matchId = db
                    .Fetch<MatchResults>()
                    .Where(matchResult => matchResult.GameDayId.ToString() == gameDay.Id)
                    .Select(matchResult => matchResult.Id)
                    .FirstOrDefault(0);

                var homeWinsSql = "SELECT count(game_results_id) " +
                                  "FROM game_results " +
                                  "WHERE match_results_id = @matchId AND home_score > away_score";
                var homeGameWins = db.Fetch<int>(homeWinsSql, new { matchId }).FirstOrDefault(0);

                var awayWinsSql = "SELECT count(game_results_id) " +
                                  "FROM game_results " +
                                  "WHERE match_results_id = @matchId AND home_score < away_score";
                var awayGameWins = db.Fetch<int>(awayWinsSql, new { matchId }).FirstOrDefault(0);

                var recordForResults = GetRecordFromResults(homeGameWins, awayGameWins, gameDay.HomeSquadId == squadId.ToString());

                record.AddRecord(recordForResults);
            }

            db.Connection?.Close();
            return record;
        }

        private SquadRecord GetRecordFromResults(int homeGameWins, int awayGameWins, bool squadIsHome)
        {
            if (homeGameWins == awayGameWins)
            {
                return new SquadRecord(0, 0, 1);
            }

            if (squadIsHome)
            {
                return homeGameWins > awayGameWins
                    ? new SquadRecord(1, 0, 0)
                    : new SquadRecord(0, 1, 0);
            }

            return homeGameWins < awayGameWins
                ? new SquadRecord(1, 0, 0)
                : new SquadRecord(0, 1, 0);
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

        private IEnumerable<GameDayInfo> GameDaysToGameDayInfos(IEnumerable<GameDay> gameDays)
        {
            return gameDays.Select(gameDay => gameDay.ToGameDayInfo()).ToList();
        }

        private string BuildConString()
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
    }
}