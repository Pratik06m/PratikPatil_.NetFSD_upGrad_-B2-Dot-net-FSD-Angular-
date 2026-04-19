using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class EventDetails
    {
        [Key]
        public Guid EventId { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string EventName { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string EventCategory { get; set; } = string.Empty;

        [Required]
        public DateTime EventDate { get; set; }

        public string? Description { get; set; }

        [Required]
        public string Status { get; set; } = "Active"; // Active or In-Active

        // Navigation properties
        public ICollection<SessionInfo> Sessions { get; set; } = new List<SessionInfo>();
        public ICollection<ParticipantEventDetails> ParticipantEventDetails { get; set; } = new List<ParticipantEventDetails>();
    }
}
