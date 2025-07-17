using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Interfaces
{
    public interface IShortUrlService
    {
        Task<IEnumerable<ShortUrl>> GetAllAsync();
        Task<ShortUrl> GetByIdAsync(int id);
        Task<ShortUrl> GetByShortCodeAsync(string shortCode);
        Task<ShortUrl> GetByOriginalUrlAsync(string originalUrl);
        Task<ShortUrl> AddAsync(string originalUrl, string userId);
        Task DeleteAsync(int id, string userId, bool isAdmin);
    }
}