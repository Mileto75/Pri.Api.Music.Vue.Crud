using System.ComponentModel.DataAnnotations;

namespace Pri.Api.Music.Api.Dtos
{
    public class RecordCreateWithImageRequestDto : RecordRequestDto
    {
        public IFormFile Image { get; set; }
    }
}
