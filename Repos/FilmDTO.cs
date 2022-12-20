using System.ComponentModel.DataAnnotations;

namespace FilmsCatalog.Repos
{
    public record FilmDTO
    {
        [Required]
        public string Title { get; set; }
        public Guid Id { get; set; }
        public string ScreeningDate { get; set; }
    }
}
