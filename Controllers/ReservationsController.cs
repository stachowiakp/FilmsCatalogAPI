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

        [HttpGet] //Get all reservations in Reservations Catalog
        public ActionResult<IEnumerable<ReservationDTO>> GetReservations()
        {
            List<ReservationDTO> DTO = new();
            var reservations = ReservationsCatalog.GetAllReservations();
            foreach(var res in reservations)
            {
                DTO.Add(Extension.AsReservationDTO(res));
            }
            return Ok(DTO);
        }

        [HttpGet("GetReservationById/{id}")] //Get reservation with given ID
        public ActionResult<ReservationDTO> GetReservation(Guid id)
        {
            var res = ReservationsCatalog.GetReservation(id);
            if (res == null) { return BadRequest(); }
            else { return Ok(Extension.AsReservationDTO(res)); }
        }

        [HttpGet("GetReservationsByFilmID/{ID}")] //Get reservations for film, filtering by FilmID
        public ActionResult<IEnumerable<ReservationDTO>> GetFilmReservations(Guid ID)
        {
            var reservations = ReservationsCatalog.GetReservationsByFilmID(ID);
            List<ReservationDTO> DTO = new();
            
            foreach (var res in reservations)
            {
                DTO.Add(Extension.AsReservationDTO(res));
            }
            return Ok(DTO);
            
        }

        [HttpPost] //Add new reservation, required input: Guid FilmID, string: FirstName, LastName, Email
        public ActionResult<ReservationDTO> NewReservation(ReservationDTO resDTO)
        {
            
                Reservation res = new Reservation(resDTO.FilmId, resDTO.FirstName, resDTO.LastName, resDTO.Email);
                ReservationsCatalog.NewReservation(res);
                return CreatedAtAction(nameof(GetReservation), new { id = res.Id }, Extension.AsReservationDTO(res));
           
        }

        [HttpPut("{Id}")] //Update reservation with given ID, required input: Guid ID, FilmID; string: FirstName, LastName, Email
        public ActionResult<ReservationDTO> UpdateReservation(Guid id, ReservationDTO resUpdate)
        { 
            Reservation res = new Reservation(resUpdate.FilmId,resUpdate.FirstName,resUpdate.LastName, resUpdate.Email);
            res.Id = id;
            ReservationsCatalog.UpdateReservation(id,res);
            return Ok(GetReservation(id));
        }

        [HttpDelete("{Id}")] //Delete reservation with given Id
        public ActionResult DeleteReservation(Guid id)
        {
            ReservationsCatalog.DeleteReservation(id);
            return Ok();
        }
    }
}

