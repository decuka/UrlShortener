using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UrlShortener.Application.Interfaces;
using UrlShortener.Presentation.DTO;

namespace UrlShortener.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AboutController : ControllerBase
    {
        private readonly IAboutService _service;

        public AboutController(IAboutService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<AboutInfoDto>> Get()
        {
            var about = await _service.GetAsync();
            if (about == null)
                return Ok(new AboutInfoDto { Description = "" });

            return Ok(new AboutInfoDto
            {
                Description = about.Description
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] AboutInfoDto dto)
        {
            await _service.UpdateAsync(dto.Description);
            return NoContent();
        }
    }
}
