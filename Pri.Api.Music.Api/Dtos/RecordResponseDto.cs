namespace Pri.Api.Music.Api.Dtos
{
    public class RecordResponseDto : RecordBaseDto
    {
        public decimal Price { get; set; }
        public BaseDto Genre { get; set; }
        public BaseDto Artist { get; set; }
        public IEnumerable<BaseDto> Properties{ get; set; }
    }
}
