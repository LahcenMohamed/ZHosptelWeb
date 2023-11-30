using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZHosptel.Models;
using ZHosptel.Reposetories.DataHalper;
using ZHosptelWeb.DTOs;

namespace ZHosptelWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(IEmployeeHalper employeeHalper) : ControllerBase
    {
        private readonly IEmployeeHalper employeeHalper = employeeHalper;

        [HttpGet]
        [Route("/GetEmployees")]
        public async Task<IActionResult> GetAll()
        {
            var lst = await employeeHalper.GetAll();
            return Ok(lst);
        }
        [HttpGet]
        [Route("/GetEmployee{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var emp = await employeeHalper.GetById(id);
            return Ok(emp);
        }
        [HttpPost]
        [Route("/CreateEmployee")]
        public async Task<IActionResult> Add([FromForm] EmployeeDTO employeeDTO)
        {
            if (ModelState.IsValid)
            {
                var emp = await DTOsConverter.ConvertToEmployee(employeeDTO);
                await employeeHalper.Add(emp);
                return Created();
            }
            return BadRequest(ModelState);

        }
        [HttpPut]
        [Route("/UpdateEmployee")]
        public async Task<IActionResult> Edit([FromForm] EmployeeDTO employeeDTO)
        {
            if (ModelState.IsValid)
            {
                var emp = await DTOsConverter.ConvertToEmployee(employeeDTO);
                await employeeHalper.Update(emp);
                return Created();
            }
            return BadRequest(ModelState);

        }

        [HttpDelete]
        [Route("/RemoveEmployee{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await employeeHalper.Delete(id);
            return Ok();
        }
    }
}
