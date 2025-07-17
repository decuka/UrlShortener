using Moq;
using UrlShortener.Application.Services;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Interfaces;

public class ShortUrlServiceTests
{
    private readonly Mock<IShortUrlRepository> _repo = new();
    private readonly ShortUrlService _svc;

    public ShortUrlServiceTests()
    {
        _svc = new ShortUrlService(_repo.Object);
    }

    [Fact]
    public async Task AddAsync_WhenOriginalExists_ShouldThrow()
    {
        // Arrange
        _repo.Setup(r => r.GetByOriginalUrlAsync("http://a"))
             .ReturnsAsync(new ShortUrl());

        // Act + Assert
        await Assert.ThrowsAsync<Exception>(() =>
            _svc.AddAsync("http://a", "user1"));
    }

    [Fact]
    public async Task AddAsync_ShouldTrimSpacesAndDetectDuplicate()
    {
        // Arrange
        _repo.Setup(r => r.GetByOriginalUrlAsync("http://a"))
             .ReturnsAsync(new ShortUrl { OriginalUrl = "http://a" });

        // Act + Assert
        await Assert.ThrowsAsync<Exception>(() =>
            _svc.AddAsync("  http://a  ", "user1"));
    }

    [Fact]
    public async Task AddAsync_ShouldReturnUrlWith8CharCode()
    {
        // Arrange
        _repo.Setup(r => r.GetByOriginalUrlAsync(It.IsAny<string>()))
             .ReturnsAsync((ShortUrl?)null);
        _repo.Setup(r => r.GetByShortCodeAsync(It.IsAny<string>()))
             .ReturnsAsync((ShortUrl?)null);

        // Act
        var result = await _svc.AddAsync("http://b", "user1");

        // Assert
        Assert.Equal(8, result.ShortCode.Length);
        Assert.Equal("http://b", result.OriginalUrl);
    }

    [Fact]
    public async Task DeleteAsync_WhenUrlNotFound_ShouldThrow()
    {
        // Arrange
        _repo.Setup(r => r.GetByIdAsync(1))
             .ReturnsAsync((ShortUrl?)null);

        // Act + Assert
        await Assert.ThrowsAsync<Exception>(() =>
            _svc.DeleteAsync(1, "user1", false));
    }

    [Fact]
    public async Task DeleteAsync_WhenNotOwnerAndNotAdmin_ShouldThrow()
    {
        // Arrange
        var url = new ShortUrl { Id = 1, CreatedById = "other" };
        _repo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(url);

        // Act + Assert
        await Assert.ThrowsAsync<Exception>(() =>
            _svc.DeleteAsync(1, "user1", false));
    }

    [Fact]
    public async Task DeleteAsync_WhenOwner_ShouldDeleteAndSave()
    {
        // Arrange
        var url = new ShortUrl { Id = 1, CreatedById = "user1" };
        _repo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(url);
        _repo.Setup(r => r.DeleteAsync(url)).Returns(Task.CompletedTask);
        _repo.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        // Act
        await _svc.DeleteAsync(1, "user1", false);

        // Assert
        _repo.Verify(r => r.DeleteAsync(url), Times.Once);
        _repo.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WhenAdmin_ShouldDeleteAndSave()
    {
        // Arrange
        var url = new ShortUrl { Id = 1, CreatedById = "other" };
        _repo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(url);
        _repo.Setup(r => r.DeleteAsync(url)).Returns(Task.CompletedTask);
        _repo.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        // Act
        await _svc.DeleteAsync(1, "adminUser", true);

        // Assert
        _repo.Verify(r => r.DeleteAsync(url), Times.Once);
        _repo.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}