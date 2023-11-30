using System.ComponentModel.DataAnnotations;

namespace ZHosptelWeb.DTOs
{
    public class MedicationDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(85)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile? Image { get; set; }
    }
}
