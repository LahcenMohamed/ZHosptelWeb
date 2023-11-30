using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZHosptel.Models
{
    public class Patient
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
        public DateOnly DateOfBirth { get; set; }
        [Required]
        [RegularExpression("Male|Female")]
        public string Gender { get; set; }
        public List<Appointment>? Appointments { get; set; }
        public List<Reservation>? Reservations { get; set; }

    }
}
