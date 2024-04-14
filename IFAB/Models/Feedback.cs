using System.ComponentModel.DataAnnotations;

namespace IFAB.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }
        public int MatchId { get; set; }
        public Match? Match { get; set; }
        public string? Message { get; set; }
        public int Rating { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
