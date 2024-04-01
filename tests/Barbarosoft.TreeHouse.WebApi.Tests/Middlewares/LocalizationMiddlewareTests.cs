using System.Globalization;
using Barbarosoft.TreeHouse.WebApi.Middlewares;
using Microsoft.AspNetCore.Http;
using NSubstitute;

namespace Barbarosoft.TreeHouse.WebApi.Tests.Middlewares;

[TestFixture]
public class LocalizationMiddlewareTests
{
    private RequestDelegate _next;
    private HttpContext _context;

    [SetUp]
    public void Setup()
    {
        _next = Substitute.For<RequestDelegate>();
        _context = new DefaultHttpContext();
    }

    [Test]
    public async Task InvokeAsync_WithTurkishLanguage_SetsTurkishCulture()
    {
        // Arrange
        var middleware = new LocalizationMiddleware(_next);
        _context.Request.Headers.AcceptLanguage = "tr";

        // Act
        await middleware.InvokeAsync(_context);

        // Assert
        CultureInfo.CurrentCulture = new CultureInfo(middleware.Culture);
        CultureInfo.CurrentUICulture = new CultureInfo(middleware.Culture);
        Assert.Multiple(() =>
        {
            Assert.That(CultureInfo.CurrentCulture.Name, Is.EqualTo("tr-TR"));
            Assert.That(CultureInfo.CurrentUICulture.Name, Is.EqualTo("tr-TR"));
        });
        await _next.Received(1).Invoke(_context);
    }

    [Test]
    public async Task InvokeAsync_WithUnsupportedLanguage_SetsDefaultCulture()
    {
        // Arrange
        var middleware = new LocalizationMiddleware(_next);
        _context.Request.Headers.AcceptLanguage = "de";

        // Act
        await middleware.InvokeAsync(_context);

        // Assert
        CultureInfo.CurrentCulture = new CultureInfo(middleware.Culture);
        CultureInfo.CurrentUICulture = new CultureInfo(middleware.Culture);
        Assert.Multiple(() =>
        {
            Assert.That(CultureInfo.CurrentCulture.Name, Is.EqualTo("en-US"));
            Assert.That(CultureInfo.CurrentUICulture.Name, Is.EqualTo("en-US"));
        });
        await _next.Received(1).Invoke(_context);
    }
}
