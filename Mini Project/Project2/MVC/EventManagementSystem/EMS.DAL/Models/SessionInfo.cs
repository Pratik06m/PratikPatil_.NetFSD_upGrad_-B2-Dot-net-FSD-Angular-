using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Models
{
    public class SessionInfo
    {
        [Key]
        public Guid SessionId { get; set; }

        [Required]
        public Guid EventId { get; set; }

        [ForeignKey("EventId")]
        public EventDetails Event { get; set; }

        [Required]
        [StringLength(50)]
        public string SessionTitle { get; set; }

        public Guid? SpeakerId { get; set; }

        [ForeignKey("SpeakerId")]
        public SpeakersDetails Speaker { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime SessionStart { get; set; }

        [Required]
        public DateTime SessionEnd { get; set; }

        public string? SessionUrl { get; set; }
    }
}
