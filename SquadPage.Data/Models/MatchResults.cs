using NPoco;

namespace SquadPage.Data.Models
{
    [TableName("match_results")]
    [PrimaryKey("match_results_id")]
    public class MatchResults
    {
        [Column("match_results_id")]
        public Int64 Id { get; set; }

        [Column("game_day_id")]
        public Int64 GameDayId { get; set; }
    }
}
