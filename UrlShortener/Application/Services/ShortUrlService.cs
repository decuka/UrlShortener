using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShortener.Application.Interfaces;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Interfaces;

namespace UrlShortener.Application.Services
{
    public class ShortUrlService : IShortUrlService
    {
        private readonly IShortUrlRepository _repository;

        public ShortUrlService(IShortUrlRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ShortUrl>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ShortUrl> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<ShortUrl> GetByShortCodeAsync(string shortCode)
        {
            return await _repository.GetByShortCodeAsync(shortCode);
        }

        public async Task<ShortUrl> GetByOriginalUrlAsync(string originalUrl)
        {
            return await _repository.GetByOriginalUrlAsync(originalUrl);
        }

        public async Task<ShortUrl> AddAsync(string originalUrl, string userId)
        {
            if (string.IsNullOrWhiteSpace(originalUrl))
                throw new ArgumentException("Original URL cannot be empty", nameof(originalUrl));

            originalUrl = originalUrl.Trim();

            var existing = await _repository.GetByOriginalUrlAsync(originalUrl);
            if (existing != null)
                throw new Exception("URL already exists!");

            var shortUrl = new ShortUrl
            {
                OriginalUrl = originalUrl,
                ShortCode = await GenerateUniqueShortCodeAsync(),
                CreatedById = userId,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(shortUrl);
            await _repository.SaveChangesAsync();
            return shortUrl;
        }

        public async Task DeleteAsync(int id, string userId, bool isAdmin)
        {
            var url = await _repository.GetByIdAsync(id);
            if (url == null)
                throw new Exception("URL not found!");

            if (!isAdmin && url.CreatedById != userId)
                throw new Exception("Access denied!");

            await _repository.DeleteAsync(url);
            await _repository.SaveChangesAsync();
        }

        /// <summary>
        /// Generates an 8-character short code based on a GUID fragment and guarantees uniqueness by
        /// querying the repository until a free code is found. Probability of collision is extremely low
        /// (16^8 ≈ 4.3 billion combinations).
        /// </summary>
        private async Task<string> GenerateUniqueShortCodeAsync()
        {
            string code;
            do
            {
                code = Guid.NewGuid().ToString("N").Substring(0, 8);
            } while (await _repository.GetByShortCodeAsync(code) != null);
            return code;
        }
    }
}