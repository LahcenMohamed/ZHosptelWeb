using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using ZHosptel.Models;
using ZHosptel.Reposetories.DataHalper;
using Microsoft.AspNetCore.Http;
using ZHosptelWeb.DTOs;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZHosptelWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class DoctorController(IDoctorHalper doctorHalper,IWebHostEnvironment webHostEnvironment) : ControllerBase
    {
        private readonly IDoctorHalper doctorHalper = doctorHalper;
        private readonly IWebHostEnvironment webHostEnvironment = webHostEnvironment;

        [HttpGet]
        [Route("/GetDoctors")]
        public async Task<IActionResult> GetDoctors()
        {
            var lst = await doctorHalper.GetAll();
            return Ok(lst);
        }
        [HttpPost]
        [Route("/CreateDoctor")]
        public async Task<IActionResult> Add([FromForm]DoctorDto doctorDto)
        {
            string imgUrl = null;
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
                var doctor = await DTOsConverter.ConvertToDoctor(doctorDto,imgUrl);
                await doctorHalper.Add(doctor);
                return Created("DoctorDetails", new { id = doctor.Id });
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("/Details/{id:int}",Name = "DoctorsDetails")]
        public async Task<IActionResult> Details(int id)
        {
            var doct = await doctorHalper.GetById(id);
            return Ok(doct);
        }

        [HttpPut]
        [Route("/UpdateDoctor")]
        public async Task<IActionResult> Edit([FromForm] DoctorDto doctorDto)
        {
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
                return Created("DoctorsDetails", new {id = doctor.Id});
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("/DeleteDoctor{id:int}")]
        public async Task<IActionResult> Remove(int id)
        {
            await doctorHalper.Delete(id);
            return Ok("Doctor is removed");
        }


    }
}
