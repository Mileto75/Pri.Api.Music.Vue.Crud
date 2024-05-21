using Microsoft.AspNetCore.Mvc;
using Pri.Api.Music.Api.Dtos;
using Pri.Api.Music.Core.Interfaces.Services;

namespace Pri.Api.Music.Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _artistService
                .GetAllAsync();
            return Ok(new ArtistsResponseDto
            {
                Artists = result.Value.Select(a =>
                new BaseDto
                {
                    Id = a.Id,
                    Name = a.Name,
                })
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _artistService.GetByIdAsync(id);
            if(result.IsSucces)
            {
                return Ok(new ArtistResponseDto
                {
                    Id = result.Value.Id,
                    Name = result.Value.Name,
                    Records = result.Value.Records.Select
                    (a => new BaseDto
                    {
                        Id= a.Id,
                        Name = a.Title,
                    })
                });
            }
            return NotFound(result.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ArtistRequestDto artistRequestDto)
        {
            var result = await _artistService.CreateAsync(artistRequestDto.Name);
            if (result.IsSucces)
                return CreatedAtAction(nameof(GetById), new { Id = result.Value.Id },new ArtistResponseDto
                {
                    Id = result.Value.Id,
                    Name = result.Value.Name,
                });
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
            return BadRequest(ModelState.Values);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _artistService.DeleteAsync(id);
            if(result.IsSucces)
            {
                return Ok("artist deleted!");
            }
            return BadRequest("Something went wrong!");
        }
        [HttpPut]
        public async Task<IActionResult> Update(ArtistUpdateRequestDto artistUpdateRequestDto) 
        {
            var result = await _artistService.UpdateAsync(artistUpdateRequestDto.Id, artistUpdateRequestDto.Name);
            if(result.IsSucces)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
