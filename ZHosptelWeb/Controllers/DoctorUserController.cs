using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ZHosptel.Models;
using ZHosptel.Reposetories.DataHalper;
using ZHosptelWeb.DTOs;

namespace ZHosptelWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Doctor")]
    public class DoctorUserController(ClaimsIdentity claimsIdentity,IWebHostEnvironment webHostEnvironment,IDoctorHalper doctorHalper,IAppointmentHalper appointmentHalper) : ControllerBase
    {
        private ClaimsIdentity claimsIdentity = claimsIdentity ;
        private readonly IWebHostEnvironment webHostEnvironment = webHostEnvironment;
        private readonly IDoctorHalper doctorHalper = doctorHalper;
        private readonly IAppointmentHalper appointmentHalper = appointmentHalper;

        [HttpGet]
        [Route("/DoctorDetails")]
        public async Task<IActionResult> Details()
        {
            int id;
            var typeIdClaim = claimsIdentity.FindFirst("TypeId");

            if (typeIdClaim != null && int.TryParse(typeIdClaim.Value, out id))
            {
                Doctor doctor = await doctorHalper.GetById(id);
                return Ok(doctor);
            }
            return BadRequest("DoctorId claim not found or invalid.");

        }

        [HttpGet]
        [Route("/DoctorAppointemnt")]
        public async Task<IActionResult> GetAppointemnt()
        {
            int id;
            var typeIdClaim = claimsIdentity.FindFirst("TypeId");

            if (typeIdClaim != null && int.TryParse(typeIdClaim.Value, out id))
            {
                var lst = await appointmentHalper.GetAppointmentAsync(id);
                return Ok(lst);
            }
            return BadRequest("DoctorId claim not found or invalid.");

        }

        [HttpPut("/DoctorUserUpdate")]
        public async Task<IActionResult> Edit([FromForm] DoctorDto doctorDto)
        {
            int id;
            var typeIdClaim = claimsIdentity.FindFirst("TypeId");

            if (typeIdClaim != null && int.TryParse(typeIdClaim.Value, out id))
            {
                doctorDto.Id = id;
                var doctImg = await doctorHalper.GetById(doctorDto.Id);
                string? imgUrl = doctImg.ImageUrl;
                if (ModelState.IsValid)
                {
                    if (doctorDto.Image != null)
                    {
                        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "image", "Doctors");
                        string file_name = Guid.NewGuid().ToString() + "_" + doctorDto.Image.FileName;
                        string file_path = Path.Combine(uploadsFolder, file_name);
                        using (var stream = new FileStream(file_path, FileMode.Create))
                        {
                            doctorDto.Image.CopyTo(stream);
                            stream.Close();
                        }
                        imgUrl = "/image/Doctors" + file_name;
                    }
                    var doctor = await DTOsConverter.ConvertToDoctor(doctorDto, imgUrl);
                    await doctorHalper.Update(doctor);
                    return Created("DoctorsDetails", new { id = doctor.Id });
                }
                return BadRequest(ModelState);
            }
            return Unauthorized();
        }
    }
}
