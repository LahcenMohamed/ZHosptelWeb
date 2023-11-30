using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZHosptel.Reposetories.DataHalper;
using ZHosptelWeb.DTOs;

namespace ZHosptelWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController(IReservationHalper reservationHalper,IRoomHalper roomHalper) : ControllerBase
    {
        private readonly IReservationHalper reservationHalper = reservationHalper;
        private readonly IRoomHalper roomHalper = roomHalper;

        [HttpGet]
        [Route("/GetReservations")]
        public async Task<IActionResult> Index() 
        {
            var lst = await reservationHalper.GetAll();
            return Ok(lst);
        }
        [HttpPost]
        [Route("/Create")]
        public async Task<IActionResult> Add([FromForm] ReservationDTO reservationDTO)
        {
            if (ModelState.IsValid)
            {
                var room = await roomHalper.GetById(reservationDTO.RoomId);
                var reservation = await DTOsConverter.ConvertToReservation(reservationDTO);
                await reservationHalper.Add(reservation);
                room.Availability = false;
                await roomHalper.Update(room);
                return Created();
            }
            return BadRequest(ModelState);
        }
        [HttpPut]
        [Route("/Update")]
        public async Task<IActionResult> Edit([FromForm] ReservationDTO reservationDTO)
        {
            if (ModelState.IsValid)
            {
                var reservation = await DTOsConverter.ConvertToReservation(reservationDTO);
                await reservationHalper.Update(reservation);
                return Created();
            }
            return BadRequest(ModelState);
        }
        [HttpGet]
        [Route("/Details{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var reservartion = await reservationHalper.GetById(id);
            return Ok(reservartion);
        }

        [HttpDelete]
        [Route("/Delete{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await reservationHalper.Delete(id);
            return Ok("deleted");
        }


    }
}
