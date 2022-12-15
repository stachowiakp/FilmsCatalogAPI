using FilmsCatalog.Entities;

namespace FilmsCatalog.Repos
{
    public interface IFilms
    {
        void AddFilm(Film film);
        Film GetFilm(Guid id);
        IEnumerable<Film> GetFilms();
        void RemoveFilm(Guid id);
        void RescheduleFilm(Guid id, DateTimeOffset schedule);
    }
}