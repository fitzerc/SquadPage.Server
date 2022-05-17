using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadPage.Shared.Models
{
    public class SquadInfoResp
    {
        public string Name { get; set; } = "Squad Name";
        public Int64 Id { get; set; } = 0;
        public SquadRecord Record { get; set; } = new SquadRecord(0, 0, 0);
    }

    public class SquadRecord
    {
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }

        public SquadRecord(int wins, int losses, int draws)
        {
            Wins = wins;
            Losses = losses;
            Draws = draws;
        }
    }
}
