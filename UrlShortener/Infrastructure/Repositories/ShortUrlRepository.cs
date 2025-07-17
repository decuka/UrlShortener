using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Interfaces;
using UrlShortener.Infrastructure.Data;

namespace UrlShortener.Infrastructure.Repositories
{
    public class ShortUrlRepository : IShortUrlRepository
    {
        private readonly ApplicationDbContext _context;

        public ShortUrlRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShortUrl>> GetAllAsync()
        {
            return await _context.ShortUrls.Include(x => x.CreatedBy).ToListAsync();
        }

        public async Task<ShortUrl> GetByIdAsync(int id)
        {
            return await _context.ShortUrls.Include(x => x.CreatedBy)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ShortUrl> GetByShortCodeAsync(string shortCode)
        {
            return await _context.ShortUrls.Include(x => x.CreatedBy)
                .FirstOrDefaultAsync(x => x.ShortCode == shortCode);
        }

        public async Task<ShortUrl> GetByOriginalUrlAsync(string originalUrl)
        {
            return await _context.ShortUrls.Include(x => x.CreatedBy)
                .FirstOrDefaultAsync(x => x.OriginalUrl == originalUrl);
        }

        public async Task AddAsync(ShortUrl shortUrl)
        {
            await _context.ShortUrls.AddAsync(shortUrl);
        }

        public async Task DeleteAsync(ShortUrl shortUrl)
        {
            _context.ShortUrls.Remove(shortUrl);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}