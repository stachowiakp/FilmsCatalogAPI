﻿using FilmsCatalog.Entities;

namespace FilmsCatalog.Repos
{
    public interface IReservations
    {
        void DeleteReservation(Guid id);
        IEnumerable<Reservation> GetAllReservations();
        Reservation GetReservation(Guid id);
        void NewReservation(Reservation reservation);
        void UpdateReservation(Reservation reservation);
    }
}