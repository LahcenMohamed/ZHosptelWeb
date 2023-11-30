using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ZHosptel.Models;
using ZHosptel.Reposetories.DataHalper;
using ZHosptelWeb.DTOs;

namespace ZHosptelWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Patient")]
    public class PatientUserController(IPetienHalper petienHalper, IWebHostEnvironment webHostEnvironment, IAppointmentHalper appointmentHalper, IReservationHalper reservationHalper, ClaimsIdentity claimsIdentity) : ControllerBase
    {
        private readonly IPetienHalper petienHalper = petienHalper;
        private readonly IWebHostEnvironment webHostEnvironment = webHostEnvironment;
        private readonly IAppointmentHalper appointmentHalper = appointmentHalper;
        private readonly IReservationHalper reservationHalper = reservationHalper;
        private readonly ClaimsIdentity claimsIdentity = claimsIdentity;

        [HttpGet]
        [Route("/PatientUserDetails")]
        public async Task<IActionResult> Details()
        {
            int id;
            var typeIdClaim = claimsIdentity.FindFirst("TypeId");

            if (typeIdClaim != null && int.TryParse(typeIdClaim.Value, out id))
            {
                Patient patient = await petienHalper.GetById(id);
                return Ok(patient);
            }
            return BadRequest("PatientId claim not found or invalid.");

        }

        [HttpGet]
        [Route("/PatientUserAppointemnt")]
        public async Task<IActionResult> GetAppointemnt()
        {
            int id;
            var typeIdClaim = claimsIdentity.FindFirst("TypeId");

            if (typeIdClaim != null && int.TryParse(typeIdClaim.Value, out id))
            {
                var lst = await appointmentHalper.GetAppointmentsPatient(id);
                return Ok(lst);
            }
            return BadRequest("DoctorId claim not found or invalid.");

        }

        [HttpGet]
        [Route("/PatientUserReservation")]
        public async Task<IActionResult> GetReservations()
        {
            int id;
            var typeIdClaim = claimsIdentity.FindFirst("TypeId");

            if (typeIdClaim != null && int.TryParse(typeIdClaim.Value, out id))
            {
                var lst = await reservationHalper.reservationsPatient(id);
                return Ok(lst);
            }
            return BadRequest("DoctorId claim not found or invalid.");

        }

        [HttpPut("/PatientUserUpdate")]
        public async Task<IActionResult> Edit([FromForm] PatientDTO patientDto)
        {
            int id;
            var typeIdClaim = claimsIdentity.FindFirst("TypeId");

            if (typeIdClaim != null && int.TryParse(typeIdClaim.Value, out id))
            {
                if (ModelState.IsValid)
                {
                    Patient patient = await DTOsConverter.ConvertToPatient(patientDto);
                    await petienHalper.Update(patient);
                    return Created();
                }
                return BadRequest(ModelState);
            }
            return Unauthorized();
        }
    }
}
