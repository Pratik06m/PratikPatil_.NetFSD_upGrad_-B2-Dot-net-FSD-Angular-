using System.ComponentModel.DataAnnotations;

namespace Week9_Day1_ContactManagementApi.DTOs
{
    public class LoginDto
    {
        [Required, EmailAddress]
        public string EmailId { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
