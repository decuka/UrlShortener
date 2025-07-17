using System.Threading.Tasks;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Interfaces
{
    public interface IAboutService
    {
        Task<AboutInfo> GetAsync();
        Task UpdateAsync(string description);
    }
}