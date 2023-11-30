using System.ComponentModel.DataAnnotations;

namespace ZHosptelWeb.DTOs
{
    public class RegisterUserDto
    {

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        [MaxLength(45)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(45)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(75)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        [Required]
        public DateOfBirthModel DateOfBirth { get; set; }
        [Required]
        [RegularExpression("Male|Female")]
        public string Gender { get; set; }

    }
}
