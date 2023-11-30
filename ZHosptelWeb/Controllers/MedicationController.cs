using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZHosptel.Reposetories.DataHalper;
using ZHosptelWeb.DTOs;

namespace ZHosptelWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationController(IMedicationHalper medicationHalper, IWebHostEnvironment webHostEnvironment) : ControllerBase
    {
        private readonly IMedicationHalper medicationHalper = medicationHalper;
        private readonly IWebHostEnvironment webHostEnvironment = webHostEnvironment;
        [HttpGet]
        [Route("/GetMedications")]
        public async Task<IActionResult> GetMedications()
        {
            var lst = await medicationHalper.GetAll();
            return Ok(lst);
        }
        [HttpPost]
        [Route("/CreateMedication")]
        public async Task<IActionResult> Add([FromForm] MedicationDTO medicationDTO)
        {
            string imgUrl = null;
            if (ModelState.IsValid)
            {
                if (medicationDTO.Image != null)
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "image", "Medications");
                    string file_name = Guid.NewGuid().ToString() + "_" + medicationDTO.Image.FileName;
                    string file_path = Path.Combine(uploadsFolder, file_name);
                    using (var stream = new FileStream(file_path, FileMode.Create))
                    {
                        medicationDTO.Image.CopyTo(stream);
                        stream.Close();
                    }
                    imgUrl = "/image/Medications" + file_name;
                }
                var doctor = await DTOsConverter.ConvertToMedication(medicationDTO, imgUrl);
                await medicationHalper.Add(doctor);
                return Created("MedicationDetails", new { id = doctor.Id });
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("/Details/{id:int}", Name = "MedicationDetails")]
        public async Task<IActionResult> Details(int id)
        {
            var doct = await medicationHalper.GetById(id);
            return Ok(doct);
        }

        [HttpPut]
        [Route("/UpdateMedication")]
        public async Task<IActionResult> Edit([FromForm] MedicationDTO medicationDTO)
        {
            string imgUrl = null;
            if (ModelState.IsValid)
            {
                if (medicationDTO.Image != null)
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "image", "Medications");
                    string file_name = Guid.NewGuid().ToString() + "_" + medicationDTO.Image.FileName;
                    string file_path = Path.Combine(uploadsFolder, file_name);
                    using (var stream = new FileStream(file_path, FileMode.Create))
                    {
                        medicationDTO.Image.CopyTo(stream);
                        stream.Close();
                    }
                    imgUrl = "/image/Medications" + file_name;
                }
                var doctor = await DTOsConverter.ConvertToMedication(medicationDTO, imgUrl);
                await medicationHalper.Update(doctor);
                return Created("MedicationDetails", new { id = doctor.Id });
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("/DeleteMedication{id:int}")]
        public async Task<IActionResult> Remove(int id)
        {
            await medicationHalper.Delete(id);
            return Ok("Doctor is removed");
        }

    }
}
