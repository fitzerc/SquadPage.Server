using NPoco;

namespace SquadPage.Data.Models
{
    [TableName("person")]
    [PrimaryKey("person_id")]
    public class Person
    {
        [Column("person_id")]
        public Int64 Id { get; set; }

        [Column("person_name")]
        public string Name { get; set; }

        [Column("user_name")]
        public string UserName { get; set; }
    }
}
