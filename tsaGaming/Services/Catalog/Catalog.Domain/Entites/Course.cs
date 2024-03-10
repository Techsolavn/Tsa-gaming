using Catalog.Domain.Interfaces;

namespace Catalog.Domain.Entites
{
    public class Course : BaseEntity, IAggregateRoot
    {
        public required string Name { get; set; }
        public required string DisplayName { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal PriceVAT { get; set; }
        public bool IsActive{ get; set; }
        public int SortIndex { get; set; }
        public bool IsTop { get; set; }
        public string ImageUrl { get; set; }
        public IList<Lesson> Lessons { get; set; }
        public Course()
        {
        }
        

    }
}
