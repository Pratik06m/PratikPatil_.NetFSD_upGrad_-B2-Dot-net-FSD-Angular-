using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class SessionInfo
    {
        [Key]
        public Guid SessionId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid EventId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string SessionTitle { get; set; } = string.Empty;

        public Guid? SpeakerId { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime SessionStart { get; set; }

        [Required]
        public DateTime SessionEnd { get; set; }

        public string? SessionUrl { get; set; }

        // Navigation properties
        [ForeignKey("EventId")]
        public EventDetails? Event { get; set; }

        [ForeignKey("SpeakerId")]
        public SpeakersDetails? Speaker { get; set; }
    }
}
