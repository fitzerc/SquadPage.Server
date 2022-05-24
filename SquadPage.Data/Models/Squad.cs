using NPoco;
using SquadPage.Shared.Models;

namespace SquadPage.Data.Models
{
    [TableName("squad")]
    [PrimaryKey("squad_id")]
    public class Squad
    {
        [Column("squad_name")]
        public string Name { get; set; } = "Squad Page";

        [Column("squad_id")]
        public Int64 Id { get; set; } = 0;

        [Column("squad_number")]
        public int Number { get; set; }

        public SquadInfoResp ToSquadInfoResp()
        {
            return new SquadInfoResp()
            {
                Id = Id,
                Name = Name,
                Number = Number
            };
        }
    }
}
