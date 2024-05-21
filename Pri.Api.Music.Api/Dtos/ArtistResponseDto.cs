namespace Pri.Api.Music.Api.Dtos
{
    public class ArtistResponseDto : BaseDto
    {
        public IEnumerable<BaseDto> Records { get; set; }
    }
}
