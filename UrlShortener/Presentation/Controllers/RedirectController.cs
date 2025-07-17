using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.Interfaces;

namespace UrlShortener.Presentation.Controllers
{
    [ApiController]
    [Route("s/{shortCode}")]
    public class RedirectController : ControllerBase
    {
        private readonly IShortUrlService _service;

        public RedirectController(IShortUrlService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> RedirectToOriginal(string shortCode)
        {
            var url = await _service.GetByShortCodeAsync(shortCode);
            if (url == null)
                return NotFound();

            return Redirect(url.OriginalUrl);
        }
    }
}