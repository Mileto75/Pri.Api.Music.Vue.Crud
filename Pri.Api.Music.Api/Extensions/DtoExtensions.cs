using Pri.Api.Music.Api.Dtos;
using Pri.CleanArchitecture.Music.Core.Entities;

namespace Pri.Api.Music.Api.Extensions
{
    public static class DtoExtensions
    {
        public static RecordsResponseDto MapToDto(this IEnumerable<Record> records,HttpContext httpContext)
        {
            return new RecordsResponseDto
            {
                Records = records.Select(r => new RecordBaseDto
                {
                    Id = r.Id,
                    Name = r.Title,
                    ImageUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/images/records/{r.Image ?? "placeholder.svg"}"
                })
                
            };
        }
        public static RecordResponseDto MapToDto(this Record record,HttpContext httpContext)
        {
            return new RecordResponseDto
            {
                Id = record.Id,
                Name = record.Title,
                Price = record.Price,
                Genre = new BaseDto
                {
                    Id = record.Genre.Id,
                    Name = record.Genre.Name
                },
                Artist = new BaseDto
                {
                    Id = record.Artist.Id,
                    Name = record.Artist.Name
                },
                Properties
                = record.Properties.Select
                (p => new BaseDto
                {
                    Id = p.Id,
                    Name = p.Name
                }),
                ImageUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/images/records/{record.Image ?? "placeholder.svg"}"
            };
        }
    }
}
