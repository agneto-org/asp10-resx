using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace MultiLangApp.Tests;

public class StaticFilesTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public StaticFilesTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData("/images/hero-home.svg")]
    [InlineData("/images/about-icon.svg")]
    [InlineData("/images/contact-icon.svg")]
    [InlineData("/images/globe-icon.svg")]
    public async Task Images_ReturnSuccessStatusCode(string url)
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync(url);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Theory]
    [InlineData("/images/hero-home.svg", "image/svg+xml")]
    [InlineData("/images/about-icon.svg", "image/svg+xml")]
    [InlineData("/images/contact-icon.svg", "image/svg+xml")]
    [InlineData("/images/globe-icon.svg", "image/svg+xml")]
    public async Task Images_ReturnCorrectContentType(string url, string expectedContentType)
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync(url);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains(expectedContentType, response.Content.Headers.ContentType?.ToString());
    }

    [Fact]
    public async Task CssFile_ReturnsSuccessAndCorrectContentType()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/css/site.css");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("text/css", response.Content.Headers.ContentType?.ToString());
    }

    [Fact]
    public async Task JsFile_ReturnsSuccessAndCorrectContentType()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/js/site.js");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var contentType = response.Content.Headers.ContentType?.ToString();
        Assert.True(
            contentType?.Contains("application/javascript") == true ||
            contentType?.Contains("text/javascript") == true,
            $"Expected JavaScript content type but got: {contentType}");
    }

    [Fact]
    public async Task CssFile_ContainsCustomStyles()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/css/site.css");
        var content = await response.Content.ReadAsStringAsync();

        Assert.Contains(".hero-section", content);
        Assert.Contains(".hero-image", content);
        Assert.Contains(".page-icon", content);
        Assert.Contains(".feature-card", content);
        Assert.Contains(".language-badge", content);
        Assert.Contains(".fade-in", content);
    }

    [Fact]
    public async Task JsFile_ContainsCustomScripts()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/js/site.js");
        var content = await response.Content.ReadAsStringAsync();

        Assert.Contains("DOMContentLoaded", content);
        Assert.Contains("initFeatureCards", content);
        Assert.Contains("checkStaticAssets", content);
    }

    [Fact]
    public async Task SvgImages_ContainValidSvgContent()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/images/hero-home.svg");
        var content = await response.Content.ReadAsStringAsync();

        Assert.Contains("<svg", content);
        Assert.Contains("</svg>", content);
        Assert.Contains("xmlns", content);
    }

    [Fact]
    public async Task NonExistentStaticFile_Returns404()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/images/nonexistent.svg");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Favicon_ReturnsSuccess()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/favicon.ico");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task BootstrapCss_ReturnsSuccess()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/lib/bootstrap/dist/css/bootstrap.min.css");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("text/css", response.Content.Headers.ContentType?.ToString());
    }

    [Fact]
    public async Task JQuery_ReturnsSuccess()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/lib/jquery/dist/jquery.min.js");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
