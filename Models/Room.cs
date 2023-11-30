using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZHosptel.Models
{
    public class Room
    {
        public int Id { get; set; }
        [Required]
        public int RoomNumber { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        [Range(1,10)]
        public int BedCount { get; set; }
        [Required]
        public bool Availability { get; set; }
        [Required]
        public int FloorNumber { get; set; }
        public string? Description { get; set; }
        public List<Reservation>? Reservations { get; set; }
    }
}
