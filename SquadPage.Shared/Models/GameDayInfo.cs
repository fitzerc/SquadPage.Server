
namespace SquadPage.Shared.Models
{
    public class GameDayInfo
    {
        public Int64 Id { get; set; }
        public DateTime GameDate { get; set; }
        public string GameLocation { get; set; }
        public Int64 HomeSquadId { get; set; }
        public Int64 AwaySquadId { get; set; }
        public string GameStatus { get; set; } //Enum?
        public string GameType { get; set; } //Enum?
    }
}
