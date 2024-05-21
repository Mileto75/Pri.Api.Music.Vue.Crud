using System.ComponentModel.DataAnnotations;

namespace Pri.Api.Music.Api.Dtos
{
    public class ArtistRequestDto 
    {
        [Required(ErrorMessage = "Name missing!")]
        public string Name { get; set; }
    }
}
