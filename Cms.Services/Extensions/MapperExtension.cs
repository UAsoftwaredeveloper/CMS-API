using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Cms.Services.Extensions
{
    public static class MapperExtension
    {
        public static void AutoMapperConfiguration(this IServiceCollection services )
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mapper=mappingConfig.CreateMapper();

            services.AddSingleton(mapper); 
        }

    }
}
