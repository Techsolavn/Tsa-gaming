using Catalog.Domain.Interfaces;

namespace Catalog.Domain.Entites
{
    public class Lesson : BaseEntity, IAggregateRoot
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive{ get; set; }
        public int SortIndex { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public IList<Game> Games { get; set; }

        public Lesson()
        {
        }
    }
}
