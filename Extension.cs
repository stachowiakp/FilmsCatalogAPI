using FilmsCatalog.Entities;
using FilmsCatalog.Repos;

namespace FilmsCatalog
{
    public class Extension
    {
        public static FilmDTO AsDTO(Film film)
        {
            return new FilmDTO { Id = film.Id, ScreeningDate = film.ScreeningDate, Title = film.Title };
            
        }
    }
}
