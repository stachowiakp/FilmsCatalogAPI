using FilmsCatalog.Entities;
using FilmsCatalog.Repos;

namespace FilmsCatalog
{
    public class Extension
    {
        public static FilmDTO AsFilmDTO(Film film)
        {
            return new FilmDTO { Id = film.Id, ScreeningDate = film.ScreeningDate, Title = film.Title };
            
        }

        public static ReservationDTO AsReservationDTO(Reservation reservation)
        {
            return new ReservationDTO {Id=reservation.Id,Email=reservation.Email,FilmId=reservation.FilmId,FirstName=reservation.FirstName,LastName=reservation.LastName };

        }
    }
}
