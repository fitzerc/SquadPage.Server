namespace SquadPage.Shared.Models
{
    public class SquadInfoResp
    {
        public string Name { get; set; } = "Squad Name";
        public string Id { get; set; } = "0";
        public int Number { get; set; } = 0;
        public SquadRecord Record { get; set; } = new SquadRecord(0, 0, 0);
    }

    public class SquadRecord
    {
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }

        public SquadRecord() { }

        public SquadRecord(int wins, int losses, int draws)
        {
            Wins = wins;
            Losses = losses;
            Draws = draws;
        }

        public void AddRecord(SquadRecord recToBeAdded)
        {
            Wins += recToBeAdded.Wins;
            Losses += recToBeAdded.Losses;
            Draws += recToBeAdded.Draws;
        }
    }
}
