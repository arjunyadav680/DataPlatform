using DataPlatform.Application.Interfaces;
using DataPlatform.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DataPlatform.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register application services
            services.AddScoped<ITranslationService, TranslationService>();
            services.AddScoped<ILanguageService, LanguageService>();

            return services;
        }
    }
}