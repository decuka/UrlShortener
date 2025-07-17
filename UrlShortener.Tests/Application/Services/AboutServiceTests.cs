using Moq;
using UrlShortener.Application.Services;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Interfaces;
using Xunit;

namespace UrlShortener.Tests.Application.Services
{
    public class AboutServiceTests
    {
        private readonly Mock<IAboutInfoRepository> _repo = new();
        private readonly AboutService _svc;

        public AboutServiceTests()
        {
            _svc = new AboutService(_repo.Object);
        }

        [Fact]
        public async Task UpdateAsync_WhenAboutDoesNotExist_ShouldCreateAndSave()
        {
            // Arrange
            _repo.Setup(r => r.GetAsync()).ReturnsAsync((AboutInfo?)null);
            _repo.Setup(r => r.UpdateAsync(It.IsAny<AboutInfo>())).Returns(Task.CompletedTask);
            _repo.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            await _svc.UpdateAsync("New description");

            // Assert
            _repo.Verify(r => r.UpdateAsync(It.Is<AboutInfo>(a => a.Description == "New description")), Times.Once);
            _repo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WhenAboutExists_ShouldUpdateDescriptionAndSave()
        {
            // Arrange
            var about = new AboutInfo { Id = 1, Description = "Old" };
            _repo.Setup(r => r.GetAsync()).ReturnsAsync(about);
            _repo.Setup(r => r.UpdateAsync(about)).Returns(Task.CompletedTask);
            _repo.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            await _svc.UpdateAsync("Updated");

            // Assert
            Assert.Equal("Updated", about.Description);
            _repo.Verify(r => r.UpdateAsync(about), Times.Once);
            _repo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
} 