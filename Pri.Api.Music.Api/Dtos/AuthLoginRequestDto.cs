using System.ComponentModel.DataAnnotations;

namespace Pri.Api.Music.Api.Dtos
{
    public class AuthLoginRequestDto
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
