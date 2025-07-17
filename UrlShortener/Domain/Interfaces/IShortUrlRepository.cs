using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Domain.Interfaces
{
    public interface IShortUrlRepository
    {
        Task<IEnumerable<ShortUrl>> GetAllAsync();
        Task<ShortUrl> GetByIdAsync(int id);
        Task<ShortUrl> GetByShortCodeAsync(string shortCode);
        Task<ShortUrl> GetByOriginalUrlAsync(string originalUrl);
        Task AddAsync(ShortUrl shortUrl);
        Task DeleteAsync(ShortUrl shortUrl);
        Task SaveChangesAsync();
    }
}