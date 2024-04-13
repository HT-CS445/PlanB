using SQLite;

namespace HabitTracker
{
    [Table("user")]
    public class User
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }
        [Column("day")]
        public string Day { get; set; }
        [Column("time")]
        public string Time { get; set; }
        [Column("habit")]
        public string Habit { get; set; }

        [Column("goal")]
        public string Goal { get; set; }

    }
}
