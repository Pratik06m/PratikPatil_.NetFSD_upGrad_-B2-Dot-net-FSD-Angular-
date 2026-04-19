using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class SpeakersDetails
    {
        [Key]
        public Guid SpeakerId { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string SpeakerName { get; set; } = string.Empty;

        // Navigation property
        public ICollection<SessionInfo> Sessions { get; set; } = new List<SessionInfo>();
    }
}
