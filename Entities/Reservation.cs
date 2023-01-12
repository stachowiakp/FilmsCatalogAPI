namespace FilmsCatalog.Entities
{
    public record Reservation
    {
        public Guid Id { get; set; }
        public Guid FilmId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        
        public Reservation (Guid filmID, string firstName, string lastName, string email)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.FilmId = filmID;
        }
        

        
    }
}
