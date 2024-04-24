using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IFAB.Models.Auth
{
    public class AppUser : IdentityUser
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }
    }
}
