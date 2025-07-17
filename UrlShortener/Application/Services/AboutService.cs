using System;
using System.Threading.Tasks;
using UrlShortener.Application.Interfaces;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Interfaces;

namespace UrlShortener.Application.Services
{
    public class AboutService : IAboutService
    {
        private readonly IAboutInfoRepository _repository;

        public AboutService(IAboutInfoRepository repository)
        {
            _repository = repository;
        }

        public async Task<AboutInfo> GetAsync()
        {
            return await _repository.GetAsync();
        }

        public async Task UpdateAsync(string description)
        {
            var about = await _repository.GetAsync();
            if (about == null)
            {
                about = new AboutInfo
                {
                    Description = description,
                };
            }
            else
            {
                about.Description = description;
            }
            await _repository.UpdateAsync(about);
            await _repository.SaveChangesAsync();
        }
    }
}