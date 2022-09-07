using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace BlazorTutorial.Language
{
    public static partial class ServiceRegistration
    {
        public static IServiceCollection AddLanguageService(this IServiceCollection services, LanguageOptions? languageOptions = null)
        {
            if (languageOptions == null)
            {
                languageOptions = new LanguageOptions();
            }

            services.AddScoped<ILanguageService>(sp =>
            {
                return new LanguageService(languageOptions, sp.GetService<NavigationManager>(), sp.GetService<IJSRuntime>());
            });

            return services;
        }
    }
}
