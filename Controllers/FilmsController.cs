using Microsoft.AspNetCore.Mvc;
using FilmsCatalog;
using FilmsCatalog.Repos;
using FilmsCatalog.Entities;

namespace FilmsCatalog.Controllers
{
    [ApiController]
    [Route("/api/Films")]
    public class FilmsController : ControllerBase
    {

        public readonly IFilms Films;

        public FilmsController(IFilms films)
        {

            this.Films = films;
        }

        [HttpGet]
        public IEnumerable<Film> GetFilms()
        {
            var result = Films.GetFilms();
            return result;
        }

        [HttpGet("{id}")]
        public ActionResult<FilmDTO> GetFilm(Guid id)
        {
            
            if (Films.GetFilm(id)==null) { return BadRequest(); }
            else { 
                Film item = Films.GetFilm(id);
               FilmDTO result = Extension.AsFilmDTO(item);
                return result;
             }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteFilm(Guid id) {


            Films.RemoveFilm(id);
            return NoContent();
        }

        [HttpPost]
        public ActionResult<Film> AddFilm(FilmDTO filmDTO)
        {
            DateTime schedule = DateTime.UtcNow;
            if (DateTime.TryParse(filmDTO.ScreeningDate, out schedule))
            {
                Film film = new Film(filmDTO.Title, schedule);
                Films.AddFilm(film);
                return CreatedAtAction(nameof(GetFilm), new { id = film.Id }, Extension.AsFilmDTO(film));

            }
            else { return BadRequest(); }
           
        }

        [HttpPut("{id}")]
        public ActionResult<FilmDTO> UpdateFilmSchedule(Guid id, UpdateFilmDTO update)
        {
            DateTime schedule = DateTime.UtcNow;
            if (DateTime.TryParse(update.ScreeningDate, out schedule))
            {
                Films.RescheduleFilm(id,schedule);

            }
            else { return BadRequest(); }
            
            var result = GetFilm(id);
            return result;
                
        }

        
       
        
    }
}
