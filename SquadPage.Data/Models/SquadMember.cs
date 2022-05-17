using NPoco;

namespace SquadPage.Data.Models
{
    [TableName("squad_member")]
    [PrimaryKey("squad_member_id")]
    public class SquadMember
    {
        [Column("squad_member_id")]
        public Int64 Id { get; set; }

        [Column("squad_id")]
        public Int64 SquadId { get; set; }

        [Column("person_id")]
        public Int64 PersonId { get; set; }
    }
}
