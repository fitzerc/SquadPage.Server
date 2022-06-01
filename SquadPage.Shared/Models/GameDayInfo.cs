
namespace SquadPage.Shared.Models
{
    public class GameDayInfo
    {
        public string Id { get; set; }
        public DateTime GameDate { get; set; }
        public string GameLocation { get; set; }
        public string HomeSquadId { get; set; }
        public string AwaySquadId { get; set; }
        public string GameStatus { get; set; } //Enum?
        public string GameType { get; set; } //Enum?
    }

    public class GameDayDetails : GameDayInfo
    {
        public MatchResultInfo Result { get; set; }
    }

    public class MatchResultInfo
    {
        public List<GameResultInfo> GameResults { get; set; } = new List<GameResultInfo>();
        public int GamesWon { get; }
        public int GamesLost { get; }
        public string Result { get; }

        public readonly string WinString = "Win";
        public readonly string LossString = "Loss";
        public readonly string DrawString = "Draw";

        public MatchResultInfo(List<GameResultInfo> gameResults)
        {
            GameResults = gameResults;
            GamesWon = GameResults.Count(gameResult => gameResult.Won);
            GamesLost = GameResults.Count(gameResult => !gameResult.Won);

            if (GamesWon > GamesLost)
            {
                Result = WinString;
            }

            if (GamesWon < GamesLost)
            {
                Result = LossString;
            }

            if (GamesWon == GamesLost)
            {
                Result = DrawString;
            }
        }
    }

    public class GameResultInfo
    {
        public bool Won { get; set; }
        public int OurScore { get; set; }
        public int TheirScore { get; set; }
    }
}
