using System.ComponentModel.DataAnnotations;

namespace Pri.Api.Music.Api.Dtos
{
    public class RecordUpdateRequestDto : RecordRequestDto
    {
        [Required(ErrorMessage = "Id required")]
        [Range(1,int.MaxValue)]
        public int Id { get; set; }
    }
}
