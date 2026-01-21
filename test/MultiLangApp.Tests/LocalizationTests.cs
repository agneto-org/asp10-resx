using System.Globalization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Xunit;

namespace MultiLangApp.Tests;

public class LocalizationTests
{
    private readonly IStringLocalizer<SharedResource> _localizer;

    public LocalizationTests()
    {
        var options = Options.Create(new LocalizationOptions { ResourcesPath = "Resources" });
        var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
        _localizer = new StringLocalizer<SharedResource>(factory);
    }

    [Fact]
    public void English_Welcome_ReturnsCorrectValue()
    {
        CultureInfo.CurrentCulture = new CultureInfo("en");
        CultureInfo.CurrentUICulture = new CultureInfo("en");

        var result = _localizer["Welcome"];

        Assert.Equal("Welcome", result.Value);
        Assert.False(result.ResourceNotFound);
    }

    [Fact]
    public void Spanish_Welcome_ReturnsCorrectValue()
    {
        CultureInfo.CurrentCulture = new CultureInfo("es");
        CultureInfo.CurrentUICulture = new CultureInfo("es");

        var result = _localizer["Welcome"];

        Assert.Equal("Bienvenido", result.Value);
        Assert.False(result.ResourceNotFound);
    }

    [Fact]
    public void English_AppTitle_ReturnsCorrectValue()
    {
        CultureInfo.CurrentCulture = new CultureInfo("en");
        CultureInfo.CurrentUICulture = new CultureInfo("en");

        var result = _localizer["AppTitle"];

        Assert.Equal("Multi-Language App", result.Value);
        Assert.False(result.ResourceNotFound);
    }

    [Fact]
    public void Spanish_AppTitle_ReturnsCorrectValue()
    {
        CultureInfo.CurrentCulture = new CultureInfo("es");
        CultureInfo.CurrentUICulture = new CultureInfo("es");

        var result = _localizer["AppTitle"];

        Assert.Equal("Aplicación Multilingüe", result.Value);
        Assert.False(result.ResourceNotFound);
    }

    [Theory]
    [InlineData("en", "Home", "Home")]
    [InlineData("es", "Home", "Inicio")]
    [InlineData("en", "About", "About")]
    [InlineData("es", "About", "Acerca de")]
    [InlineData("en", "Contact", "Contact")]
    [InlineData("es", "Contact", "Contacto")]
    public void NavigationLabels_ReturnCorrectTranslations(string culture, string key, string expected)
    {
        CultureInfo.CurrentCulture = new CultureInfo(culture);
        CultureInfo.CurrentUICulture = new CultureInfo(culture);

        var result = _localizer[key];

        Assert.Equal(expected, result.Value);
        Assert.False(result.ResourceNotFound);
    }

    [Theory]
    [InlineData("en", "Email", "Email")]
    [InlineData("es", "Email", "Correo Electrónico")]
    [InlineData("en", "Phone", "Phone")]
    [InlineData("es", "Phone", "Teléfono")]
    [InlineData("en", "Address", "Address")]
    [InlineData("es", "Address", "Dirección")]
    public void ContactLabels_ReturnCorrectTranslations(string culture, string key, string expected)
    {
        CultureInfo.CurrentCulture = new CultureInfo(culture);
        CultureInfo.CurrentUICulture = new CultureInfo(culture);

        var result = _localizer[key];

        Assert.Equal(expected, result.Value);
        Assert.False(result.ResourceNotFound);
    }

    [Fact]
    public void AllEnglishKeys_HaveSpanishTranslations()
    {
        var englishKeys = new[]
        {
            "AppTitle", "Home", "About", "Contact", "Welcome", "WelcomeMessage",
            "HomeDescription", "AboutTitle", "AboutDescription", "ContactTitle",
            "ContactDescription", "Email", "Phone", "Address", "SelectLanguage",
            "English", "Spanish", "LearnMore"
        };

        CultureInfo.CurrentCulture = new CultureInfo("es");
        CultureInfo.CurrentUICulture = new CultureInfo("es");

        foreach (var key in englishKeys)
        {
            var result = _localizer[key];
            Assert.False(result.ResourceNotFound, $"Spanish translation missing for key: {key}");
        }
    }
}
