using System.Threading.Tasks;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Domain.Interfaces
{
    public interface IAboutInfoRepository
    {
        Task<AboutInfo> GetAsync();
        Task UpdateAsync(AboutInfo aboutInfo);
        Task SaveChangesAsync();
    }
}