﻿using MongoDB.Driver;
using MongoDB.Bson;
using FilmsCatalog.Entities;
using System.Globalization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FilmsCatalog.Repos
{
    public class MongoDBRep : IFilms,IReservations
    {
        private const string Database= "FilmsAndReservations";
        private const string FilmsCollection = "FilmsCatalog";
        private const string ReservationsCollection = "ReservationsCatalog";

        private readonly IMongoCollection<Film> FilmsCatalog;
        private readonly IMongoCollection<Reservation> ReservationsCatalog;

        private readonly FilterDefinitionBuilder<Film> FilmBuilder = Builders<Film>.Filter;
        private readonly FilterDefinitionBuilder<Reservation> ReservationBuilder = Builders<Reservation>.Filter;

        public MongoDBRep(IMongoClient mongo) {
            IMongoDatabase db = mongo.GetDatabase(Database);
            FilmsCatalog = db.GetCollection<Film>(FilmsCollection);
            ReservationsCatalog = db.GetCollection<Reservation>(ReservationsCollection);
        }

        //Films methods
        public void AddFilm(Film film)
        {
            FilmsCatalog.InsertOne(film);
        }
        public Film GetFilm(Guid id)
        {
            var filter = FilmBuilder.Eq(item => item.Id, id);
            return FilmsCatalog.Find(filter).SingleOrDefault();
        }
        public IEnumerable<Film> GetFilms()
        {
            return FilmsCatalog.Find(new BsonDocument()).ToList();
        }
        public void RemoveFilm(Guid id)
        {
            var filter = FilmBuilder.Eq(item => item.Id, id);
            
            if (FilmsCatalog.Find(filter).SingleOrDefault() != null)
            {
                var ResFilter = ReservationBuilder.Eq(item => item.FilmId, id);
                long ResCount = ReservationsCatalog.Find(ResFilter).CountDocuments();

                if (ResCount == 0) { FilmsCatalog.DeleteOne(filter); }
                else
                {
                    ReservationsCatalog.DeleteMany(ResFilter);
                    FilmsCatalog.DeleteOne(filter);
                }
                
            }
        }

        
        public void RescheduleFilm(Guid id, DateTime DateTimeInput)
        {
            var filter = FilmBuilder.Eq(item => item.Id, id);
            
            var update = Builders<Film>.Update
                .Set(f => f.ScreeningDate, DateTimeInput);
             
                FilmsCatalog.FindOneAndUpdate(filter, update);
           
            
        }

        //Reservations methods
        public void DeleteReservation(Guid id)
        {
            var filter = ReservationBuilder.Eq(item => item.Id, id);
            ReservationsCatalog.DeleteOne(filter);
            
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            return ReservationsCatalog.Find(new BsonDocument()).ToList();
        }

        public Reservation GetReservation(Guid id)
        {
            var filter = ReservationBuilder.Eq(item => item.Id, id);
            return ReservationsCatalog.Find(filter).SingleOrDefault();
        }

        public void NewReservation(Reservation reservation)
        { 
            var filmfilter = FilmBuilder.Eq(item => item.Id,reservation.FilmId);
            if ((FilmsCatalog.Find(filmfilter).SingleOrDefault()) == null)
            {
                throw new Exception("Wrong FilmID");
            }
            else {ReservationsCatalog.InsertOne(reservation); }
        }

        public void UpdateReservation(Guid ID, Reservation reservation)
        {
            var filter = ReservationBuilder.Eq(item => item.Id, ID);
            if ((ReservationsCatalog.Find(filter).SingleOrDefault()) == null)
            { throw new Exception("Wrong Reservation ID"); }
            else {
                ReservationsCatalog.FindOneAndReplace(filter, reservation);
                
               
            }

        }

        public IEnumerable<Reservation> GetReservationsByFilmID(Guid FilmID)
        {
            
            var filmfilter = FilmBuilder.Eq(item => item.Id, FilmID);
            if ((FilmsCatalog.Find(filmfilter).SingleOrDefault()) == null)
            {
                throw new Exception("Wrong Film ID");
            }
            else
            {
                var filter = ReservationBuilder.Eq(item => item.FilmId, FilmID);
                return ReservationsCatalog.Find(filter).ToList();
            }
        }
    }
}
