namespace Pri.CleanArchitecture.Music.Core.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Record> Records { get; set; }
    }
}