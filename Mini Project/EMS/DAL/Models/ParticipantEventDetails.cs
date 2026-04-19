using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class ParticipantEventDetails
    {
        [Key]
        public Guid ID { get; set; } = Guid.NewGuid();

        [Required]
        public string ParticipantEmailId { get; set; } = string.Empty;

        [Required]
        public Guid EventId { get; set; }

        public bool IsAttended { get; set; } = false;

        // Navigation properties
        [ForeignKey("ParticipantEmailId")]
        public UserInfo? Participant { get; set; }

        [ForeignKey("EventId")]
        public EventDetails? Event { get; set; }
    }
}
