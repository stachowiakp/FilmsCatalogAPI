using FilmsCatalog.Entities;

namespace FilmsCatalog.Repos
{
    public interface IReservations
    {
        void DeleteReservation(Guid id);
        IEnumerable<Reservation> GetAllReservations();
        IEnumerable<Reservation> GetReservationsByFilmID(Guid FilmID);
        Reservation GetReservation(Guid id);
        void NewReservation(Reservation reservation);
        void UpdateReservation(Guid ID,Reservation reservation);
    }
}