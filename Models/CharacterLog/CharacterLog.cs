
namespace SuperHero240327.Models.CharacterLog
{
    public sealed class CharacterLog
    {
        public long ID { get; set; }

        public long CharacterID { get; set; }

        public string Name { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Place { get; set; }

        public string Action { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
