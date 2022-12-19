using FilmsCatalog.Entities;

namespace FilmsCatalog.Repos
{
    public class Reservations : IReservations
    {
        private readonly List<Reservation> ReservationsCatalog = new()
        {

        };

        public Reservation GetReservation(Guid id)
        {
            var Index = ReservationsCatalog.FindIndex(res => res.Id == id);
            if (Index == -1) { return null; }
            else
            {
                return ReservationsCatalog[Index];
            }
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            return ReservationsCatalog;
        }

        public void NewReservation(Reservation reservation)
        {
            ReservationsCatalog.Add(reservation);
        }

        public void UpdateReservation(Reservation reservation)
        {
            var Index = ReservationsCatalog.FindIndex(res => res.Id == reservation.Id);
            ReservationsCatalog[Index] = reservation;
        }

        public void DeleteReservation(Guid id)
        {
            var Index = ReservationsCatalog.FindIndex(res => res.Id == id);
            ReservationsCatalog.RemoveAt(Index);
        }
    }
}
