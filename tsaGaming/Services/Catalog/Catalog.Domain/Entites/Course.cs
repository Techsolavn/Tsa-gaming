using Catalog.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Domain.Entites
{
    public class Course : BaseEntity, IAggregateRoot
    {
        public required string Name { get; set; }
        public required string DisplayName { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
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
