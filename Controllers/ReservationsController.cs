using Microsoft.AspNetCore.Mvc;
using FilmsCatalog;
using FilmsCatalog.Repos;
using FilmsCatalog.Entities;

namespace FilmsCatalog.Controllers
{
    [ApiController]
    [Route("/api/Films")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservations Reservations;
        private readonly IFilms Films;

        public ReservationsController(IReservations reservations,IFilms films)
        {
            this.Reservations = reservations;
            this.Films= films;
        }

        [HttpGet]
        public IEnumerable<Film> GetFilms()
        {
            var result = Films.GetFilms();
            return result;
        }

        [HttpGet("{id}")]
        public ActionResult<Film> GetFilm(Guid id)
        {
            var result = Films.GetFilm(id);
            return result;
        }

        
       /* private void ReservationsInit()
        {
            var filmslist = Films.GetFilms();
            var numberoffilms=filmslist.Count<Film>();
            if(numberoffilms > 0)
            {
                Reservation res1 = new Reservation(filmslist[0])
            }
        }*/
        
    }
}
