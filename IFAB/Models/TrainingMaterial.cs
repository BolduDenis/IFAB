using System.ComponentModel.DataAnnotations;

namespace IFAB.Models
{
    public class TrainingMaterial
    {

        [Key] 
        public int MaterialId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
