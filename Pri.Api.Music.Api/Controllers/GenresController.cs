using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pri.Api.Music.Api.Dtos;
using Pri.Api.Music.Core.Interfaces.Services;

namespace Pri.Api.Music.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _genreService.GetAllAsync();
            if(result.IsSucces)
            {
                return Ok(new GenresResponseDto
                {
                    Genres = result.Value.Select(g => new BaseDto 
                    { 
                        Id = g.Id,
                        Name = g.Name,
                    })
                });
            }
            return Ok("No Genres found!");
        }
    }
}
