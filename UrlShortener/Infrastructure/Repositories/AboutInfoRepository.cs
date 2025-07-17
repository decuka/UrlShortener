using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Interfaces;
using UrlShortener.Infrastructure.Data;

namespace UrlShortener.Infrastructure.Repositories
{
    public class AboutInfoRepository : IAboutInfoRepository
    {
        private readonly ApplicationDbContext _context;

        public AboutInfoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AboutInfo> GetAsync()
        {
            return await _context.AboutInfos
                                 .OrderBy(x => x.Id)
                                 .FirstOrDefaultAsync();
        }
        public async Task UpdateAsync(AboutInfo aboutInfo)
        {
            _context.AboutInfos.Update(aboutInfo);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}