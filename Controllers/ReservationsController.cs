using Microsoft.AspNetCore.Mvc;
using FilmsCatalog.Repos;
using FilmsCatalog.Entities;

namespace FilmsCatalog.Controllers
{
    [ApiController]
    [Route("/Api/Reservations")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservations ReservationsCatalog;
        
        public ReservationsController(IReservations resCat)
        {
            this.ReservationsCatalog = resCat; 
        }

        [HttpGet]
        public IEnumerable<Reservation> GetReservations()
        {
            return ReservationsCatalog.GetAllReservations();
        }

        [HttpGet("{id}")]
        public ActionResult<Reservation> GetReservation(Guid id)
        {
            var res = ReservationsCatalog.GetReservation(id);
            if (res == null) { return BadRequest(); }
            else { return res; }
        }

        [HttpPost]
        public ActionResult<Reservation> NewReservation(ReservationDTO resDTO)
        {
            
                Reservation res = new Reservation(resDTO.FilmId, resDTO.FirstName, resDTO.LastName, resDTO.Email);
                ReservationsCatalog.NewReservation(res);
                return CreatedAtAction(nameof(GetReservation), new { id = res.Id }, Extension.AsReservationDTO(res));
            //missing mechanism for checking Film's ID with resDTO.FilmId - reference to FilmsController instance of Films?
        }
    }
}

