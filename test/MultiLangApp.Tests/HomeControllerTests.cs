using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using MultiLangApp.Controllers;
using Xunit;

namespace MultiLangApp.Tests;

public class HomeControllerTests
{
    [Fact]
    public void Index_ReturnsViewResult()
    {
        var controller = new HomeController();

        var result = controller.Index();

        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void About_ReturnsViewResult()
    {
        var controller = new HomeController();

        var result = controller.About();

        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void Contact_ReturnsViewResult()
    {
        var controller = new HomeController();

        var result = controller.Contact();

        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void SetLanguage_ReturnsLocalRedirect()
    {
        var controller = new HomeController();
        var httpContext = new DefaultHttpContext();
        httpContext.Response.Body = new MemoryStream();
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };

        var result = controller.SetLanguage("es", "/Home/Index");

        var redirectResult = Assert.IsType<LocalRedirectResult>(result);
        Assert.Equal("/Home/Index", redirectResult.Url);
    }

    [Fact]
    public void SetLanguage_SetsCultureCookie()
    {
        var controller = new HomeController();
        var httpContext = new DefaultHttpContext();
        httpContext.Response.Body = new MemoryStream();
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };

        controller.SetLanguage("es", "/Home/Index");

        Assert.True(httpContext.Response.Headers.ContainsKey("Set-Cookie"));
        var cookieHeader = httpContext.Response.Headers["Set-Cookie"].ToString();
        Assert.Contains(CookieRequestCultureProvider.DefaultCookieName, cookieHeader);
        Assert.Contains("es", cookieHeader);
    }

    [Theory]
    [InlineData("en")]
    [InlineData("es")]
    public void SetLanguage_AcceptsValidCultures(string culture)
    {
        var controller = new HomeController();
        var httpContext = new DefaultHttpContext();
        httpContext.Response.Body = new MemoryStream();
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };

        var result = controller.SetLanguage(culture, "/");

        Assert.IsType<LocalRedirectResult>(result);
    }
}
