using SQLite;

namespace ClientBtgApp.Base.Models
{
    [Table("client")]
    public class Client
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("lastname")]
        public string Lastname { get; set; }

        [Column("age")]
        public int? Age { get; set; }

        [Column("adress")]
        public string Address { get; set; }

    }
}
