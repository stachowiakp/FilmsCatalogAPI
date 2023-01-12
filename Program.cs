using Microsoft.Extensions.DependencyInjection;
using FilmsCatalog.Repos;
using FilmsCatalog.Controllers;
using FilmsCatalog.Entities;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using FilmsCatalog.Settings;
using MongoDB.Driver;

namespace FilmsCatalog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //serializers for MongoDB for different data types
            BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));

            //init of connection settings to MongoDB (Configuration = appsettings.json)
            var settings = builder.Configuration.GetSection(nameof(MongoCS))
                .Get<MongoCS>();

            // Add services to the container.

            //setting services and servers
            builder.Services.AddSingleton<IFilms, MongoDBRep>();
            builder.Services.AddSingleton<IReservations, MongoDBRep>();

            //init of MongoClient, using settings set in line 23
            builder.Services.AddSingleton<IMongoClient>(serviceProvider => {return new MongoClient(settings.ConnectionString);});

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}