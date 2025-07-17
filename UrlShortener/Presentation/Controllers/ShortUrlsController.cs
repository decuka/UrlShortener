using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UrlShortener.Application.Interfaces;
using UrlShortener.Presentation.DTO;

namespace UrlShortener.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShortUrlsController : ControllerBase
    {
        private readonly IShortUrlService _service;

        public ShortUrlsController(IShortUrlService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShortUrlDto>>> GetAll()
        {
            var urls = await _service.GetAllAsync();
            return Ok(urls.Select(u => new ShortUrlDto
            {
                Id = u.Id,
                OriginalUrl = u.OriginalUrl,
                ShortCode = u.ShortCode,
                CreatedBy = u.CreatedBy?.Email,
                CreatedAt = u.CreatedAt
            }));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ShortUrlDto>> Add([FromBody] CreateShortUrlDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var url = await _service.AddAsync(dto.OriginalUrl, userId);
            return Ok(new ShortUrlDto
            {
                Id = url.Id,
                OriginalUrl = url.OriginalUrl,
                ShortCode = url.ShortCode,
                CreatedBy = url.CreatedBy?.Email,
                CreatedAt = url.CreatedAt
            });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");
            await _service.DeleteAsync(id, userId, isAdmin);
            return NoContent();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ShortUrlDto>> GetById(int id)
        {
            var url = await _service.GetByIdAsync(id);
            if (url == null)
                return NotFound();

            return Ok(new ShortUrlDto
            {
                Id = url.Id,
                OriginalUrl = url.OriginalUrl,
                ShortCode = url.ShortCode,
                CreatedBy = url.CreatedBy?.Email,
                CreatedAt = url.CreatedAt
            });
        }
    }
}
