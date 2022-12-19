using FilmsCatalog.Controllers;
using FilmsCatalog.Entities;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace FilmsCatalog.Repos
{
    public record ReservationDTO
    {

        public Guid Id { get; set; }

        [Required]
        public Guid FilmId {get; set;}

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }






    }
}
