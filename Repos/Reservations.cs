using FilmsCatalog.Entities;

namespace FilmsCatalog.Repos
{//This class is not really needed nor used - all methods below are served by MongoDBRep class. This one was just for testing.
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

        public void UpdateReservation(Guid ID,Reservation reservation)
        {
            var Index = ReservationsCatalog.FindIndex(res => res.Id == ID);
            ReservationsCatalog[Index] = reservation;
        }

        public void DeleteReservation(Guid id)
        {
            var Index = ReservationsCatalog.FindIndex(res => res.Id == id);
            ReservationsCatalog.RemoveAt(Index);
        }

        public IEnumerable<Reservation> GetReservationsByFilmID(Guid FilmId)
        {
            

            var Indexes = ReservationsCatalog.FindAll(res=>res.Id == FilmId);
            if (Indexes.Count == 0) { return null; }
            else
            {
                return Indexes;  
                
            }
        }
    }
}
