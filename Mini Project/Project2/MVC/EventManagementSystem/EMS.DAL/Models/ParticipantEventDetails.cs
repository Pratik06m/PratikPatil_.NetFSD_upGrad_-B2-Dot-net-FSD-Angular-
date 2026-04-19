using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Models
{
    public class ParticipantEventDetails
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string ParticipantEmailId { get; set; }

        [ForeignKey("ParticipantEmailId")]
        public UserInfo User { get; set; }

        [Required]
        public Guid EventId { get; set; }

        [ForeignKey("EventId")]
        public EventDetails Event { get; set; }

        public bool IsAttended { get; set; }
    }
}
