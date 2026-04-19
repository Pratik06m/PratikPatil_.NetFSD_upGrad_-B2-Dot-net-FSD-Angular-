using System.ComponentModel.DataAnnotations;

namespace AppUi.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string EmailId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be 6-20 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string EmailId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Username must be 1-50 characters")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be 6-20 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    public class EventViewModel
    {
        public Guid EventId { get; set; }

        [Required(ErrorMessage = "Event Name is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Event name must be 1-50 characters")]
        [Display(Name = "Event Name")]
        public string EventName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Category must be 1-50 characters")]
        [Display(Name = "Category")]
        public string EventCategory { get; set; } = string.Empty;

        [Required(ErrorMessage = "Event Date is required")]
        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; } = DateTime.Now.AddDays(1);

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Required]
        public string Status { get; set; } = "Active";

        public List<SessionViewModel> Sessions { get; set; } = new();
    }

    public class SessionViewModel
    {
        public Guid SessionId { get; set; }

        [Required(ErrorMessage = "Event is required")]
        public Guid EventId { get; set; }

        [Required(ErrorMessage = "Session Title is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Title must be 1-50 characters")]
        [Display(Name = "Session Title")]
        public string SessionTitle { get; set; } = string.Empty;

        public Guid? SpeakerId { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Start time is required")]
        [Display(Name = "Start Time")]
        public DateTime SessionStart { get; set; }

        [Required(ErrorMessage = "End time is required")]
        [Display(Name = "End Time")]
        public DateTime SessionEnd { get; set; }

        [Display(Name = "Session URL")]
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string? SessionUrl { get; set; }

        // Display helpers
        public string? SpeakerName { get; set; }
        public string? EventName { get; set; }
    }

    public class SpeakerViewModel
    {
        public Guid SpeakerId { get; set; }

        [Required(ErrorMessage = "Speaker Name is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Name must be 1-50 characters")]
        [Display(Name = "Speaker Name")]
        public string SpeakerName { get; set; } = string.Empty;
    }

    public class AssignSpeakerViewModel
    {
        public Guid SessionId { get; set; }
        public string SessionTitle { get; set; } = string.Empty;
        public string EventName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a speaker")]
        public Guid SpeakerId { get; set; }

        public List<SpeakerViewModel> Speakers { get; set; } = new();
    }

    public class ParticipantDashboardViewModel
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<RegisteredEventViewModel> RegisteredEvents { get; set; } = new();
    }

    public class RegisteredEventViewModel
    {
        public Guid RegistrationId { get; set; }
        public Guid EventId { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string EventCategory { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool IsAttended { get; set; }
        public List<SessionViewModel> Sessions { get; set; } = new();
    }

    public class AdminDashboardViewModel
    {
        public int TotalEvents { get; set; }
        public int ActiveEvents { get; set; }
        public int TotalSessions { get; set; }
        public int TotalSpeakers { get; set; }
        public int TotalParticipants { get; set; }
    }

    public class HomeViewModel
    {
        public List<EventViewModel> Events { get; set; } = new();
        public List<string> Categories { get; set; } = new();
        public string? SelectedCategory { get; set; }
    }
}
