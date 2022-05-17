using NPoco;

namespace SquadPage.Data.Models
{
    [TableName("game_results")]
    [PrimaryKey("game_results_id")]
    public class GameResults
    {
        [Column("game_results_id")]
        public Int64 Id { get; set; }

        [Column("match_results_id")]
        public Int64 MatchResultsId { get; set; }

        [Column("home_score")]
        public int HomeScore { get; set; }

        [Column("away_score")]
        public int AwayScore { get; set; }
    }
}
