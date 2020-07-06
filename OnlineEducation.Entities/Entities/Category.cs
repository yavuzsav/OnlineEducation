using OnlineEducation.Entities.Abstract;

namespace OnlineEducation.Entities.Entities
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
