using System.ComponentModel.DataAnnotations;

namespace Presentation
{
    public class LoginDto
    {
        [Required]
        [StringLength(100)]
        public string Login {  get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Password { get; set; } = string.Empty;
    }
}
