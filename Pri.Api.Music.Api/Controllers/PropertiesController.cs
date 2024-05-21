using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pri.Api.Music.Api.Dtos;
using Pri.Api.Music.Core.Interfaces.Services;
using Pri.Api.Music.Core.Services;

namespace Pri.Api.Music.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertiesController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _propertyService.GetAllAsync();
            if (result.IsSucces)
            {
                return Ok(new PropertiesResponseDto
                {
                    Properties = result.Value.Select(p => new BaseDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                    })
                });
            }
            return Ok("No Properties found!");
        }
    }
}
