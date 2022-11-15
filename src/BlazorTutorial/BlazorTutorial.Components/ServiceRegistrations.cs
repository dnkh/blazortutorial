using BlazorTutorial.Components.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Text;

namespace BlazorTutorial.Components
{
    public static partial class ServiceRegistration
    {
        public static IServiceCollection AddMarkdownViewerService(this IServiceCollection services)
        {
            services.AddHttpClient<MarkdownViewerService>((provider, client) =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                client.BaseAddress = new Uri("http://localhost:7071");
                client.Timeout = TimeSpan.FromSeconds(30);
            });
            return services;
        }
    }
}
