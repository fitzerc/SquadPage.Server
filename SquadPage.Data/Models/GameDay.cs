using NPoco;

namespace SquadPage.Data.Models
{
    [TableName("game_day")]
    [PrimaryKey("game_day_id")]
    public class GameDay
    {
        [Column("game_day_id")]
        public Int64 Id { get; set; }

        [Column("game_date")]
        public DateTime GameDate { get; set; }

        [Column("game_location")]
        public string Location { get; set; }

        [Column("home_squad_id")]
        public Int64 HomeSquadId { get; set; }

        [Column("away_squad_id")]
        public Int64 AwaySquadId { get; set; }

        [Column("game_status")]
        public string Status { get; set; }

        [Column("game_type")]
        public string Type { get; set; }
    }
}
