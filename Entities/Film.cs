namespace FilmsCatalog.Entities
{
    public record Film
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTimeOffset ScreeningDate { get; set; }

        public Film(string title)
        {
            this.Title= title;
            this.Id= Guid.NewGuid();
            this.ScreeningDate = DateTimeOffset.UtcNow.AddDays(1);
        }
    }
}
