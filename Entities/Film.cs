namespace FilmsCatalog.Entities
{
    public record Film
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime ScreeningDate { get; set; }

        public Film(string title)
        {
            this.Title= title;
            this.Id= Guid.NewGuid();
            this.ScreeningDate = DateTime.UtcNow.AddDays(1);
        }
    }
}
