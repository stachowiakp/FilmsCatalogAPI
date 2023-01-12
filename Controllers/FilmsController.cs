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

        [HttpGet] //Get all films in catalog
        public IEnumerable<FilmDTO> GetFilms()
        {
            List<FilmDTO> DTO = new List<FilmDTO>();
            var results = Films.GetFilms();
            foreach(var result in results)
            {
                DTO.Add(Extension.AsFilmDTO(result));
            }
            return DTO;
        }

        [HttpGet("{id}")] //Get film with given ID
        public ActionResult<FilmDTO> GetFilm(Guid id)
        {
            
            if (Films.GetFilm(id)==null) { return BadRequest(); }
            else { 
                Film item = Films.GetFilm(id);
               FilmDTO result = Extension.AsFilmDTO(item);
                return result;
             }
        }

        [HttpDelete("{id}")] //Delete film with given ID
        public ActionResult DeleteFilm(Guid id) {


            Films.RemoveFilm(id);
            return Ok();
        }

        [HttpPost] //Add film to catalog (required input: string Title, DateTime Schedule)
        public ActionResult<FilmDTO> AddFilm(FilmDTO filmDTO)
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

        [HttpPut("{id}")] //Update film record with given ID (required input: guid ID - to filter catalog, DateTime schedule - changed data)
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