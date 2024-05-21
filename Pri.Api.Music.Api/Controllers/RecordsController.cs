using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pri.Api.Music.Api.Dtos;
using Pri.Api.Music.Api.Extensions;
using Pri.CleanArchitecture.Music.Core.Entities;
using Pri.CleanArchitecture.Music.Core.Interfaces.Services;
using Pri.CleanArchitecture.Music.Core.Services.Models;
using System.Xml.Linq;

namespace Pri.Api.Music.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly IRecordService _recordService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<RecordsController> _logger;
        
        public RecordsController(IRecordService recordService, IWebHostEnvironment webHostEnvironment, ILogger<RecordsController> logger)
        {
            _recordService = recordService;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _recordService.GetAllAsync();
            return Ok(result.Value.MapToDto(HttpContext));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            //get the record
            var result = await _recordService.GetByIdAsync(id);
            //check for errors
            if (result.IsSucces)
            {
                return Ok(result.Value.MapToDto(HttpContext));
            }
            return NotFound(result.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> Add(RecordRequestDto recordRequestDto)
        {
            var result = await _recordService.CreateRecordAsync(
                new RecordCreateRequestModel
                {
                    Title = recordRequestDto.Title,
                    Price = recordRequestDto.Price,
                    GenreId = recordRequestDto.GenreId,
                    ArtistId = recordRequestDto.ArtistId,
                    PropertyIds = recordRequestDto.PropertyIds,
                });
            if (result.IsSucces)
            {
                return CreatedAtAction(nameof(Get), new {ID = result.Value.Id },result.Value
                    .MapToDto(HttpContext));
            }
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
            return BadRequest(ModelState.Values);
        }
        [HttpPut]
        public async Task<IActionResult> Update(RecordUpdateRequestDto recordUpdateRequestDto)
        {
            if (!await _recordService.CheckIfExistsAsync(recordUpdateRequestDto.Id))
            {
                return NotFound("Record not found!");
            }
            var result = await _recordService.UpdateRecordAsync
                (
                    new RecordUpdateRequestModel 
                    {
                        Id = recordUpdateRequestDto.Id,
                        Title = recordUpdateRequestDto.Title,
                        Price= recordUpdateRequestDto.Price,
                        PropertyIds= recordUpdateRequestDto.PropertyIds,
                        ArtistId= recordUpdateRequestDto.ArtistId,
                        GenreId = recordUpdateRequestDto.GenreId,
                    }
                );
            if (result.IsSucces)
            {
                return Ok();
            }
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
            return BadRequest(ModelState.Values);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //check if exists
            if(!await _recordService.CheckIfExistsAsync (id))
            {
                return NotFound("Record not found");
            }
            var result = await _recordService.DeleteRecordAsync(id);
            if(result.IsSucces)
            {
                return Ok("Product deleted!");
            }
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
            return BadRequest(ModelState.Values);
        }
        //implement SearchByArtist
        [HttpGet("Search/ByArtistName/{name}")]
        [Authorize(Policy ="AdultOnly")]
        public async Task<IActionResult> SearchByArtist(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("character not allowed!");
            }
            var result = await _recordService.SearchByArtistAsync(name);
            if(result.IsSucces)
            {
                return Ok(result.Value.MapToDto(HttpContext));
            }
            return Ok(result.Errors);
        }
        [HttpGet("ByGenre/{id}")]
        public async Task<IActionResult> GetByGenreId(int id)
        {
            var result = await _recordService.GetRecordsByGenreIdAsync(id);
            if (result.IsSucces)
            {
                return Ok(result.Value.MapToDto(HttpContext));
            }
            return Ok(result.Errors);
        }
        [HttpGet("Search/ByTitle/{title}")]
        public async Task<IActionResult> SearchByTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return BadRequest("character not allowed!");
            }
            var result = await _recordService.SearchByTitleAsync(title);
            if (result.IsSucces)
            {
                return Ok(result.Value.MapToDto(HttpContext));
            }
            return Ok(result.Errors);
        }
        [HttpPost("Image")]
        public async Task<IActionResult> CreateWithImage([FromForm]RecordCreateWithImageRequestDto
            recordCreateWithImageRequestDto)
        {
            var filename = "";
            if (recordCreateWithImageRequestDto.Image != null)
            {
                filename = await StoreFile<Record>(recordCreateWithImageRequestDto.Image);
            }
            var result = await _recordService
                .CreateRecordAsync(new RecordCreateRequestModel
                {
                    Title = recordCreateWithImageRequestDto.Title,
                    GenreId = recordCreateWithImageRequestDto.GenreId,
                    ArtistId = recordCreateWithImageRequestDto.ArtistId,
                    PropertyIds = recordCreateWithImageRequestDto.PropertyIds,
                    Price = recordCreateWithImageRequestDto.Price,
                    Image = filename,
                });
            
            //create(filename in db)
            if(result.IsSucces)
            {
                return 
                    CreatedAtAction(nameof(Get), new { Id = result.Value.Id }
                    , result.Value.MapToDto(HttpContext));
            }
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
            return BadRequest(ModelState.Values);
        }
        private async Task<string> StoreFile<T>(IFormFile file)
        {
            //generic
            //unique filename
            var filename
                = $"{Guid.NewGuid()}_{file.FileName}";
            //filepath
            var pathToImages 
                = Path.Combine(_webHostEnvironment.WebRootPath, "images", "records");
            if (!Directory.Exists(pathToImages))
            {
                Directory.CreateDirectory(pathToImages);
            }
            //create path to file
            var pathToFile = Path.Combine(pathToImages, filename);
            //copy to location
            using (FileStream filestream = new FileStream(pathToFile, FileMode.Create))
            {
                try
                {
                    await file.CopyToAsync(filestream);
                }
                catch (FileNotFoundException fileNotFoundException)
                {
                    //log the error
                    _logger.LogError(fileNotFoundException.Message);
                    Response.StatusCode = 500;
                    return "";
                }
            }
            return filename;
        }
    }
}
