using System.ComponentModel.DataAnnotations;

namespace IFAB.Models
{
    public class Recusal
    {

        [Key]
        public int RecusalId { get; set; }
        public string? Reason { get; set; }
        public DateOnly Unavailability { get; set; }
        public int MatchId { get; set; }
        public Match? Match { get; set;}
        public int UserId { get; set; }
        public User? User { get; set; }
        
    }
}
