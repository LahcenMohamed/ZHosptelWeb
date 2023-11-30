using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZHosptel.Models;
using ZHosptel.Reposetories.DataHalper;
using ZHosptelWeb.DTOs;

namespace ZHosptelWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController(IPetienHalper petienHalper) : ControllerBase
    {
        private readonly IPetienHalper petienHalper = petienHalper;
        [HttpGet]
        [Route("/GetPatients")]
        public async Task<IActionResult> GetPatients()
        {
            var lst = await petienHalper.GetAll();
            return Ok(lst);
        }
        [HttpGet]
        [Route("/PatientDetails{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var pat = await petienHalper.GetById(id);
            return Ok(pat);
        }

        [HttpPost]
        [Route("/CreatePatient")]
        public async Task<IActionResult> Add([FromForm]PatientDTO patientDto)
        {
            if (ModelState.IsValid)
            {
                Patient patient = await DTOsConverter.ConvertToPatient(patientDto);
                await petienHalper.Add(patient);
                return Created();
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("/UpdatePatient")]
        public async Task<IActionResult> Edit([FromForm] PatientDTO patientDto)
        {
            if (ModelState.IsValid)
            {
                Patient patient = await DTOsConverter.ConvertToPatient(patientDto);
                await petienHalper.Update(patient);
                return Created();
            }
            return BadRequest(ModelState);
        }
        [HttpDelete]
        [Route("/DeletePatient")]
        public async Task<IActionResult> Remove(int id)
        {
            await petienHalper.Delete(id);
            return Ok("Petient is Deleted");
        }
    }
}
