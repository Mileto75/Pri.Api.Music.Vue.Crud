using System.ComponentModel.DataAnnotations;

namespace Pri.Api.Music.Api.Dtos
{
    public class RecordRequestDto
    {
        [Required(ErrorMessage = "Title missing")]
        public string Title { get; set; }
        [Required]
        [Range(0.0,int.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        public int GenreId { get; set; }
        [Required]
        public int ArtistId { get; set; }
        public IEnumerable<int> PropertyIds { get; set; }
    }
}
