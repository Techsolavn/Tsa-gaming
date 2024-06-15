namespace Web.Bff.AdminPortal.Models
{
    public record CatalogDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string DisplayName { get; set; }
        public required string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public bool IsTop { get; set; }
        public int SortIndex { get; set; }
        public required IList<LessonDTO> Lessons { get; set; }
    }

    public record LessonDTO
    {
        public required string Name { get; set; }
        public bool IsActive { get; set; }
        public int SortIndex { get; set; }
        public required IList<GameDTO> Games { get; set; }
    }

    public record GameDTO
    {
        public required string Name { get; set; }
        public bool IsActive { get; set; }
        public int SortIndex { get; set; }
    }
}
