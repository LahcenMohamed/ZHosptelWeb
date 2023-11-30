using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ZHosptelWeb.DTOs
{
    public class UserDTO
    {
        [Required]
        public string UserName { get; set; }
        [EmailAddress]
        public virtual string Email { get; set; }
        [Required]
        public string Password{ get; set; }
        [Required]
        [AllowedValues("Admin","Patient","Doctor")]
        public string Type { get; set; }
        public int? TypeId { get; set; }
    }
}
