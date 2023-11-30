using System.ComponentModel.DataAnnotations;

namespace ZHosptelWeb.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
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
        [Required]
        public string Address { get; set; }
        [Required]
        public DateOfBirthModel DateOfBirth { get; set; }
        [Required]
        public string JobTitle { get; set; }
    }
}
