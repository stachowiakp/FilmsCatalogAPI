using Microsoft.AspNetCore.Mvc.Formatters;
using System.Globalization;

namespace FilmsCatalog.Entities
{
    public record Film
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime ScreeningDate { get; set; }

        

        
        public Film(string title, DateTime DateTimeInput)
        {
            this.Title = title;
            this.Id = Guid.NewGuid();
            this.ScreeningDate = Convert.ToDateTime(DateTimeInput, new CultureInfo("en-US"));
        }
    
    }
    
        
    
}
