using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ZHosptel.Models;
using ZHosptelWeb.DTOs;

namespace ZHosptelWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(UserManager<HosptelUser> userManager) : ControllerBase
    {
        private readonly UserManager<HosptelUser> userManager = userManager;

        [HttpPost]
        [Route("/CreateUser")]
        public async Task<IActionResult> AddUser([FromForm]UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                HosptelUser hosptelUser = new HosptelUser() 
                {
                    UserName = userDTO.UserName,
                    Email = userDTO.Email,
                    Type = userDTO.Type,
                    TypeId = userDTO.TypeId
                };
                if (hosptelUser.Type == "Admin")
                    hosptelUser.TypeId = 0;
                IdentityResult result = await userManager.CreateAsync(hosptelUser,userDTO.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(hosptelUser, hosptelUser.Type);
                    return StatusCode(200);
                }
                return BadRequest(result.Errors.FirstOrDefault());
            }
            return BadRequest(ModelState);
        }
    }
}
