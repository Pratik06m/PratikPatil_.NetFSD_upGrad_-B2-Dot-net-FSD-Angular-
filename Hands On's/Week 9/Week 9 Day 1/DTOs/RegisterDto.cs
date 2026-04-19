using System.ComponentModel.DataAnnotations;

namespace Week9_Day1_ContactManagementApi.DTOs
{
    public class RegisterDto
    {
        [Required, EmailAddress]
        public string EmailId { get; set; } = string.Empty;

        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;
    }
}
