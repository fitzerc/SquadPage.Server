using Microsoft.Extensions.Configuration;
using Npgsql;
using SquadPage.Data.Models;
using SquadPage.Shared.DataInterfaces;
using SquadPage.Shared.Models;

namespace SquadPage.Data;

public class SquadDataAccess : ISquadDataAccess
{
    private readonly IConfiguration _config;

    public SquadDataAccess(IConfiguration config)
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

    public SquadRecord GetSquadRecord(Int64 squadId)
    {
        var da = new DataAccess(_config);
        using var conn = new NpgsqlConnection(BuildConString());
        //move to singleton?
        using var db = new NPoco.Database(conn);
        db.Connection?.Open();

        var record = new SquadRecord(0, 0, 0);
        var gameDays = da.GetGameDays(squadId);

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

    public SquadRecord GetRecordFromResults(int homeGameWins, int awayGameWins, bool squadIsHome)
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

    public IEnumerable<SquadInfoResp> GetSquads()
    {
        throw new NotImplementedException();
    }

    public SquadInfoResp GetSquadInfoByNumber(int squadNumber)
    {
        throw new NotImplementedException();
    }
}
