using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZHosptel.Models
{
    public class HosptelUser : IdentityUser
    {
        [AllowedValues("Admin", "Patient", "Doctor")]
        public string Type { get; set; }
        public int? TypeId { get; set; }
    }
}
