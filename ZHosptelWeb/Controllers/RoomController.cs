using Microsoft.AspNetCore.Mvc;
using ZHosptel.Models;
using ZHosptel.Reposetories.DataHalper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZHosptelWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController(IRoomHalper roomHalper) : ControllerBase
    {
        private readonly IRoomHalper roomHalper = roomHalper;
        [HttpGet]
        [Route("/GetRooms")]
        public async Task<IActionResult> GetAll() 
        {
            var lst = await roomHalper.GetAll();
            return Ok(lst);
        }
        [HttpGet]
        [Route("/GetRoom{id:int}")]
        public async Task<IActionResult> Details(int id) 
        {
            var room = await roomHalper.GetById(id);
            return Ok(room);
        }
        [HttpPost]
        [Route("/CreateRoom")]
        public async Task<IActionResult> Add([FromForm] Room room) 
        {
            if (ModelState.IsValid)
            {
                await roomHalper.Add(room);
                return Created();
            }
            return BadRequest(ModelState);

        }
        [HttpPut]
        [Route("/UpdateRoom")]
        public async Task<IActionResult> Edit([FromForm] Room room)
        {
            if (ModelState.IsValid)
            {
                await roomHalper.Update(room);
                return Created();
            }
            return BadRequest(ModelState);

        }

        [HttpDelete]
        [Route("/RemoveRoom{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await roomHalper.Delete(id);
            return Ok();
        }
    }
}
