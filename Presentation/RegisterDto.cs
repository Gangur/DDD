using System.ComponentModel.DataAnnotations;

namespace Presentation
{
    public class RegisterDto
    {
        [Required]
        [StringLength(100)]
        public string Email {  get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string DisplayName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Password { get; set; } = string.Empty;
    }
}
