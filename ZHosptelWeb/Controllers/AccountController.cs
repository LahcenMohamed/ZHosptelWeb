using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ZHosptel.Models;
using ZHosptel.Reposetories.DataHalper;
using ZHosptelWeb.DTOs;

namespace ZHosptelWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(UserManager<HosptelUser> userManager,IPetienHalper petienHalper, IConfiguration configuration) : ControllerBase
    {
        private readonly UserManager<HosptelUser> userManager = userManager;
        private readonly IPetienHalper petienHalper = petienHalper;
        private readonly IConfiguration configuration = configuration;


        [HttpPost("/Resgister")]
        public async Task<IActionResult> Register([FromForm]RegisterUserDto registerUserDto)
        {
            if (ModelState.IsValid)
            {
                Patient patient = new Patient() 
                {
                    FirstName = registerUserDto.FirstName,
                    LastName = registerUserDto.LastName,
                    Address = registerUserDto.Address,
                    Email = registerUserDto.Email,
                    PhoneNumber = registerUserDto.PhoneNumber,
                    Gender = registerUserDto.Gender,
                    DateOfBirth = new DateOnly(registerUserDto.DateOfBirth.Year, registerUserDto.DateOfBirth.Month, registerUserDto.DateOfBirth.Day)
                };
                await petienHalper.Add(patient);
                HosptelUser hosptelUser = new HosptelUser() 
                {
                    UserName = registerUserDto.UserName,
                    Email = registerUserDto.Email,
                    Type = "Patient",
                    TypeId = patient.Id
                };
                IdentityResult result = await userManager.CreateAsync(hosptelUser,registerUserDto.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(hosptelUser, "Patient");
                    return Ok("Account Add Success");
                }
                return BadRequest(result.Errors.FirstOrDefault());
            }
            return BadRequest(ModelState);
        }
        [HttpPost("/Login")]
        public async Task<IActionResult> Register([FromForm] LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                HosptelUser? hosptelUser = await userManager.FindByNameAsync(loginDTO.UserName);
                if (hosptelUser != null)
                {
                    bool found = await userManager.CheckPasswordAsync(hosptelUser, loginDTO.Password);
                    if (found)
                    {
                        //try
                        //{
                            var claims = new List<Claim>
                            { 
                              new Claim(ClaimTypes.Name, hosptelUser.UserName),
                              new Claim(ClaimTypes.NameIdentifier, hosptelUser.Id),
                              new Claim("Type", hosptelUser.Type),
                              new Claim("TypeId", hosptelUser.TypeId.ToString()),
                              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                            };

                            var roles = await userManager.GetRolesAsync(hosptelUser);
                            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                        var securityKey = new SymmetricSecurityKey(GenerateSymmetricKey(configuration["JWT:Secret"]));
                        var signincred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                        var mytoken = new JwtSecurityToken(
                                issuer: configuration["JWT:ValidIssuer"],
                                audience: configuration["JWT:ValidAudiance"],
                                claims: claims,
                                expires: DateTime.Now.AddHours(1),
                                signingCredentials: signincred
                            );



                        // Return the token in the response
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                            expiration = mytoken.ValidTo
                        });
                        //}
                        //catch (Exception ex)
                        //{

                        //    // Log or debug the exception
                        //    Console.WriteLine($"Token creation error: {ex.Message}");
                        //    return StatusCode(500, "Internal Server Error");
                        //}
                    }
                }
                return Unauthorized();
            }
            return BadRequest(ModelState);
        }
        private byte[] GenerateSymmetricKey(string secret)
        {
            // Convert the string secret to bytes using UTF-8 encoding
            byte[] keyBytes = Encoding.UTF8.GetBytes(secret);

            // Ensure the key is at least 256 bits (32 bytes) long
            if (keyBytes.Length < 32)
            {
                // If the key is too short, pad it with zeros or handle it as appropriate for your security requirements
                Array.Resize(ref keyBytes, 32);
            }

            return keyBytes;
        }
    }
}
