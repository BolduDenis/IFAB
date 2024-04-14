using System.ComponentModel.DataAnnotations;

namespace IFAB.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public int League { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Role { get; set; }
        public List<Match>? Matches { get; set; }
        public List<Recusal>? Recusals { get; set; }
        public Feedback? Feedback { get; set; }  
    }
}
