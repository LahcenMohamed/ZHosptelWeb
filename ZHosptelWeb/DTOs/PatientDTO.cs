using System.ComponentModel.DataAnnotations;
using ZHosptel.Models;

namespace ZHosptelWeb.DTOs
{
    public class PatientDTO
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
        public List<DateOnly>? DateOfAppointment { get; set; }
        public List<TimeOnly>? TimeOfAppointment { get; set; }
        public List<DateOnly>? DateOfReservation { get; set; }
        public List<TimeOnly>? TimeOfReservation { get; set; }
    }
}
