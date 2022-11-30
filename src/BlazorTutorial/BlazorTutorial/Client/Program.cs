using BlazorTutorial.Components;
using BlazorTutorial.Language;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

namespace BlazorTutorial
{
    using BlazorTutorial.Core.StateHandling;
    using MudBlazor;
    using System.Net;

    using BlazorTutorial.Core.Services;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<ApplicationState>();
            builder.Services.AddScoped<ICookieService, CookieService>();
            builder.Services.AddScoped<ISettingService, SettingService>();
            builder.Services.AddLocalization();
            builder.Services.AddMudServices(config =>
                {
                    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
            builder.Services.AddHttpClient();
            builder.Services.AddMarkdownViewerService();

                    config.SnackbarConfiguration.PreventDuplicates = false;
                    config.SnackbarConfiguration.NewestOnTop = false;
                    config.SnackbarConfiguration.ShowCloseIcon = true;
                    config.SnackbarConfiguration.VisibleStateDuration = 5000;
                    config.SnackbarConfiguration.HideTransitionDuration = 500;
                    config.SnackbarConfiguration.ShowTransitionDuration = 500;
                    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
                }); builder.Services.AddLanguageService(new LanguageOptions()
                                                            {
                                                                TwoLetterISODefaultLanguage = "de",
                                                                TwoLetterISOLanguages = new[] { "de", "en" }
                                                            });

            builder.Services.AddMsalAuthentication(options =>
            {
                builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
            });

            var host = builder.Build();
            await host.SetDefaultCulture();
            await host.RunAsync();

        }
    }
}