using System.ComponentModel.DataAnnotations;

namespace IFAB.Models
{
    public class Match
    {
        [Key]
        public int MatchId { get; set; }
        public DateOnly Date { get; set; }
        public string? Location { get; set; }
        public string? HomeTeam { get; set; }
        public string? AwayTeam { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public Feedback? Feedback { get; set; }
        public MatchReport? Report { get; set; }
        public List<Recusal>? Recusals { get; set; }
    }
}
