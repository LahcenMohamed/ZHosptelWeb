using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZHosptel.Reposetories.DataHalper;
using ZHosptelWeb.DTOs;

namespace ZHosptelWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController(IAppointmentHalper appointmentHalper) : ControllerBase
    {
        private readonly IAppointmentHalper appointmentHalper = appointmentHalper;
        [HttpGet]
        [Route("/index")]
        public async Task<IActionResult> Index()
        {
            var lst = await appointmentHalper.GetAll();
            return Ok(lst);
        }
        [HttpPost]
        [Route("/CreateAppointment")]
        public async Task<IActionResult> Add([FromForm] AppointmentDTO appointmentDTO)
        {
            if (ModelState.IsValid)
            {
                var app = await DTOsConverter.ConvertToAppointment(appointmentDTO);
                await appointmentHalper.Add(app);
                return Created();
            }
            return BadRequest(ModelState);
        }
        [HttpPut]
        [Route("/UpdateAppointment")]
        public async Task<IActionResult> Edit([FromForm] AppointmentDTO appointmentDTO)
        {
            if (ModelState.IsValid)
            {
                var app = await DTOsConverter.ConvertToAppointment(appointmentDTO);
                await appointmentHalper.Update(app);
                return Created();
            }
            return BadRequest(ModelState);
        }
        [HttpGet]
        [Route("/DetailsAppointment{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var app = await appointmentHalper.GetById(id);
            return Ok(app);
        }
        [HttpDelete]
        [Route("/DeleteAppointment{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await appointmentHalper.Delete(id);
            return Ok("Delete");
        }

    }
}
