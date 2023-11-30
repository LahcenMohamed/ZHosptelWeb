using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ZHosptelWeb.DTOs
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
        [Required]
        public DateOfBirthModel DateOfAppointment { get; set; }
        [Required]
        public TimeDTO TimeOfAppointment { get; set; }
        public int PatientId { get; set; }
        public int DocterId { get; set; }
    }
}
