using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZHosptelWeb.DTOs
{
    public class DoctorDto
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
        public string? Address { get; set; }
        [Required]
        public DateOfBirthModel DateOfBirth { get; set; }
        [Required]
        [RegularExpression("Male|Female")]
        public string Gender { get; set; }
        [Required]
        public string Specialty { get; set; }
        [Required]
        [Range(1, 24)]
        public int DayHours { get; set; }
        public IFormFile? Image { get; set; }
        public string? OtherCredentials { get; set; }
        public List<DateOnly>? DateOfAppointment { get; set; }
        public List<TimeOnly>? TimeOfAppointment { get; set; }
    }
    public class DateOfBirthModel
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
    }
}
