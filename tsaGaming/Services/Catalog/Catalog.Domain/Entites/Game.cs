using Catalog.Domain.Interfaces;

namespace Catalog.Domain.Entites
{
    public class Game : BaseEntity, IAggregateRoot
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive{ get; set; }
        public int SortIndex { get; set; }
        public int LessonId { get; set; }
        public int FrameRate { get; set; }
        public Lesson Lesson { get; set; }
        public Game()
        {
        }
    }
}
