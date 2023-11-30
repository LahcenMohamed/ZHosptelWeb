using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZHosptel.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        [Required]
        public DateOnly DateOfReservation { get; set; }
        [Required]
        public TimeOnly TimeOfReservation { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public Patient Patient { get; set; }
    }
}
