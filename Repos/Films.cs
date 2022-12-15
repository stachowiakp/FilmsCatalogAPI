using System.Collections.Generic;
using System;
using FilmsCatalog.Entities;

namespace FilmsCatalog.Repos
{
    public class Films : IFilms
    {
        public readonly List<Film> FilmsCatalog = new()
        {
            new Film ("Titanic"),
            new Film ("Die Hard"),
            new Film ("Shrek")
        };

        public IEnumerable<Film> GetFilms() { return FilmsCatalog; }
        public Film GetFilm(Guid id)
        {
            var Index = FilmsCatalog.FindIndex(film => film.Id == id);
            return FilmsCatalog[Index];
        }
        public void AddFilm(Film film)
        {
            FilmsCatalog.Add(film);
        }

        public void RemoveFilm(Guid id)
        {
            var Index = FilmsCatalog.FindIndex(film => film.Id == id);
            FilmsCatalog.RemoveAt(Index);
        }
        public void RescheduleFilm(Guid id, DateTime schedule)
        {
            var Index = FilmsCatalog.FindIndex(film => film.Id == id);
            FilmsCatalog[Index].ScreeningDate = schedule;
        }
    }
}
