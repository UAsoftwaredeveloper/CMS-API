using CMS.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CMS.Extensions
{
    public static class DIExtension
    {
        public static void DepandancyCMS(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();

        }
    }
}
