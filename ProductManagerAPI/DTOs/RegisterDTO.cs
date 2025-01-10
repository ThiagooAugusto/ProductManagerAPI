using System.ComponentModel.DataAnnotations;

namespace ProductManagerAPI.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string? UserName {  get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Email { get; set; }
    }
}
