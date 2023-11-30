using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZHosptel.Models
{
    public class Employee
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
        public DateOnly DateOfBirth { get; set; }
        [Required]
        public string JobTitle { get; set; }
    }
}
