using System.ComponentModel.DataAnnotations;

namespace Week9_Day1_ContactManagementApi.DTOs
{
    public class ContactUpdateDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string Phone { get; set; } = string.Empty;
    }
}
