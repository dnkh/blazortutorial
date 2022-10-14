using BlazorTutorial.Components;
using BlazorTutorial.Language;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

namespace BlazorTutorial
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddLocalization();
            builder.Services.AddMudServices();
            builder.Services.AddLanguageService(new LanguageOptions()
            {
                TwoLetterISODefaultLanguage = "de",
                TwoLetterISOLanguages = new[] { "de", "en" }
            });
            builder.Services.AddHttpClient();
            builder.Services.AddMarkdownViewerService();

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