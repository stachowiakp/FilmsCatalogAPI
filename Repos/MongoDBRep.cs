using MongoDB.Driver;
using MongoDB.Bson;
using FilmsCatalog.Entities;
using System.Globalization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FilmsCatalog.Repos
{
    public class MongoDBRep : IFilms, IReservations
    {
        //Constans for MongoDB connections
        private const string Database = "FilmsAndReservations";
        private const string FilmsCollection = "FilmsCatalog";
        private const string ReservationsCollection = "ReservationsCatalog";

        //MongoCollections init
        private readonly IMongoCollection<Film> FilmsCatalog;
        private readonly IMongoCollection<Reservation> ReservationsCatalog;

        //Init of Filter Builders used for filtering docs in mongoDB collections
        private readonly FilterDefinitionBuilder<Film> FilmBuilder = Builders<Film>.Filter;
        private readonly FilterDefinitionBuilder<Reservation> ReservationBuilder = Builders<Reservation>.Filter;

        public MongoDBRep(IMongoClient mongo)
        {
            IMongoDatabase db = mongo.GetDatabase(Database);
            FilmsCatalog = db.GetCollection<Film>(FilmsCollection);
            ReservationsCatalog = db.GetCollection<Reservation>(ReservationsCollection);
        }

        //Films methods

        public void AddFilm(Film film) //Add film
        {
            FilmsCatalog.InsertOne(film);
        }

        public Film GetFilm(Guid id) //Get film with given ID
        {
            var filter = FilmBuilder.Eq(item => item.Id, id);
            return FilmsCatalog.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Film> GetFilms() //Get all films in collection
        {
            return FilmsCatalog.Find(new BsonDocument()).ToList();
        }

        public void RemoveFilm(Guid id) //Remove film with given ID
        {
            var filter = FilmBuilder.Eq(item => item.Id, id);
            if (FilmsCatalog.Find(filter).SingleOrDefault() != null)
            {
                var ResFilter = ReservationBuilder.Eq(item => item.FilmId, id);
                long ResCount = ReservationsCatalog.Find(ResFilter).CountDocuments();
                //mechanism for removing all reservations related to film being deleted
                if (ResCount == 0) { FilmsCatalog.DeleteOne(filter); }
                else
                {
                    ReservationsCatalog.DeleteMany(ResFilter);
                    FilmsCatalog.DeleteOne(filter);
                }
            }
        }

        public void RescheduleFilm(Guid id, DateTime DateTimeInput) //Reschedule Film (PUT / UpdateFilmSchedule method in Films controller)
        {
            var filter = FilmBuilder.Eq(item => item.Id, id);

            var update = Builders<Film>.Update
                .Set(f => f.ScreeningDate, DateTimeInput);
            FilmsCatalog.FindOneAndUpdate(filter, update);
        }


        //Reservations methods

        public void DeleteReservation(Guid id) //Remove reservation
        {
            var filter = ReservationBuilder.Eq(item => item.Id, id);
            ReservationsCatalog.DeleteOne(filter);
            
        }

        public IEnumerable<Reservation> GetAllReservations() //Get all reservations
        {
            return ReservationsCatalog.Find(new BsonDocument()).ToList();
        }

        public Reservation GetReservation(Guid id) //Get reservation with given ID
        {
            var filter = ReservationBuilder.Eq(item => item.Id, id);
            return ReservationsCatalog.Find(filter).SingleOrDefault();
        }

        public void NewReservation(Reservation reservation) //Add reservation
        {
            var filmfilter = FilmBuilder.Eq(item => item.Id, reservation.FilmId);
            if ((FilmsCatalog.Find(filmfilter).SingleOrDefault()) == null)
            {
                throw new Exception("Wrong FilmID"); //mechanism for checking if inputed FilmID is correct
            }
            else { ReservationsCatalog.InsertOne(reservation); }
        }

        public void UpdateReservation(Guid ID, Reservation reservation) //Update reservation
        {
            var filter = ReservationBuilder.Eq(item => item.Id, ID);
            if ((ReservationsCatalog.Find(filter).SingleOrDefault()) == null)
            { throw new Exception("Wrong Reservation ID"); }
            else
            {
                ReservationsCatalog.FindOneAndReplace(filter, reservation);
            }
        }

        public IEnumerable<Reservation> GetReservationsByFilmID(Guid FilmID) //Get reservations for related film, using FilmID
        {

            var filmfilter = FilmBuilder.Eq(item => item.Id, FilmID);
            if ((FilmsCatalog.Find(filmfilter).SingleOrDefault()) == null)
            {
                throw new Exception("Wrong Film ID"); //mechanism for checking if inputed FilmID is correct

            }
            else
            {
                var filter = ReservationBuilder.Eq(item => item.FilmId, FilmID);
                return ReservationsCatalog.Find(filter).ToList();
            }
        }
    }
}
