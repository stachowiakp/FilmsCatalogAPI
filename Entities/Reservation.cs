namespace FilmsCatalog.Entities
{
    public record Reservation
    {
        public Guid Id { get; init; }
        public Guid FilmId { get; init; }

        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }

        public Reservation(Guid filmId, string firstName, string lastname, string email) {
        this.Id=Guid.NewGuid();
            this.FilmId=filmId;
            this.FirstName=firstName;
            this.LastName=lastname; 
            this.Email=email;
        }

    }
}
