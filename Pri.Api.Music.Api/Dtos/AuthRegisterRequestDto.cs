using System.ComponentModel.DataAnnotations;

namespace Pri.Api.Music.Api.Dtos
{
    public class AuthRegisterRequestDto : AuthLoginRequestDto
    {
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RepeatPassword { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}
