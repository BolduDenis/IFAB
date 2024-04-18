using System.ComponentModel.DataAnnotations;

namespace IFAB.Models
{
    public class MatchReport
    {
        [Key]
        public int ReportId { get; set; }
        public int MatchId { get; set; }
        public Match? Match { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int? DurationBtwRounds { get; set; }
        public string? HalfTimeScore { get; set; }
        public string? FinalScore { get; set; }
    }
}
