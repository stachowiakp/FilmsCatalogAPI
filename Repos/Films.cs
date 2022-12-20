using System.Collections.Generic;
using System;
using FilmsCatalog.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FilmsCatalog.Repos
{
    public class Films : IFilms
    {
        public readonly List<Film> FilmsCatalog = new()
        {
            new Film ("Titanic",new DateTime(2022,12,21,16,00,00)),
            new Film ("Die Hard",new DateTime(2022,12,21,18,00,00)),
            new Film ("Shrek",new DateTime(2022,12,21,21,30,00))
        };

        public IEnumerable<Film> GetFilms() { return FilmsCatalog; }
        public Film GetFilm(Guid id)
        {
            var Index = FilmsCatalog.FindIndex(existingFilm => existingFilm.Id == id);
            if (Index == -1) { return null; }
            else
            {
                var item= FilmsCatalog[Index];
                return item;
            }
            
            
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
