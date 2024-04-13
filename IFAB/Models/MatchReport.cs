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
        public TimeOnly DurationBtwRounds { get; set; }
        public int HalfTimeScore { get; set; }
        public int FinalScore { get; set; }
    }
}
