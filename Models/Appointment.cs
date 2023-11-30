using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZHosptel.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        [Required]
        public DateOnly DateOfAppointment { get; set; }
        [Required]
        public TimeOnly TimeOfAppointment { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        [ForeignKey("Doctor")]
        public int DocterId { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}
