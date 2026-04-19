using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class UserInfo
    {
        [Key]
        [Required]
        [EmailAddress]
        public string EmailId { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = "Participant"; // Admin or Participant

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        // Navigation property
        public ICollection<ParticipantEventDetails> ParticipantEventDetails { get; set; } = new List<ParticipantEventDetails>();
    }
}
