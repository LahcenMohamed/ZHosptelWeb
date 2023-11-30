using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ZHosptelWeb.DTOs
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        [Required]
        public DateOfBirthModel DateOfReservation { get; set; }
        [Required]
        public TimeDTO TimeOfReservation { get; set; }
        public int PatientId { get; set; }
        public int RoomId { get; set; }
    }
    public class TimeDTO
    {
        public int Hour { get; set; }
        public int minut { get; set; }
        public int sconde { get; set; }
    }
}
