using AutoMapper;
using AutoMapper.Features;
using Cms.Services;
using Cms.Services.Extensions;
using Cms.Services.Interfaces;
using Cms.Services.Services;
using CMS.Extensions;
using CMS.Repositories.Extensions;
using CMS.Repositories.Interfaces;
using CMS.Repositories.Repositories;
using CMS.Services;
using CMS.Services.Services;
using DataManager;
using DataManager.DataClasses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;

namespace CMS
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        public IConfiguration Configuration { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void Configure(IApplicationBuilder app)
        {
            var scope= app.ApplicationServices.CreateScope();
            try
            {
                Log.Log.Info("CMS API program started");
                using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<CMSDBContext>();
                    
                    context.Install();

                }
                app.UseHttpsRedirection();

                app.UseRouting();
                app.UseStaticFiles();
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample API");
                });     // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
                app.UseAuthentication();
                app.UseAuthorization();
                app.UseEndpoints(endpoints => { endpoints.MapControllers(); }).UseWebSockets();
                //using (var serviceProvider = scope)
                //{
                //    var packageService = scope.ServiceProvider.GetRequiredService<IHolidayPackagesRepository>();
                //    var templateServiceRepo = scope.ServiceProvider.GetRequiredService<ITemplateDetailsRepository>();
                //    var dataCachingService = scope.ServiceProvider.GetRequiredService<IDataCachingService>();
                //    var dealDetailKey = "holidayPackageDetails-0-All";
                //    var templateDetailKey = "templateDetail";


                //    if (packageService != null)
                //    {
                //        var filterDBData = packageService.GetAll(deleted: false)
                //            .Where(x => x.ValidTo.HasValue && x.ValidTo.Value.Date >= DateTime.Now.Date)
                //            .Where(x => x.Active == true)
                //            .ToList();

                //        if (filterDBData.Count > 0)
                //        {
                //            dataCachingService.PushDataInCache(filterDBData, dealDetailKey);
                //        }
                //    }
                //    if (templateServiceRepo != null)
                //    {
                //        var filterDBData = templateServiceRepo.GetAll(deleted: false).Include(x => x.TemplateMaster).Include(x => x.Portal)
                //            .Where(x => x.Active == true)
                //            .ToList();

                //        if (filterDBData.Count > 0)
                //        {
                //            dataCachingService.PushDataInCache(filterDBData, templateDetailKey);
                //        }
                //    }
                //}
                app.Use(async (ctx, next) =>
                {
                    try
                    {
                        await next();
                    }
                    catch (Exception ex)
                    {
                        // Log the exception (use your logging framework here)
                        Console.WriteLine($"An error occurred: {ex.Message}");
                        // Set response details for a generic error page
                        ctx.Response.ContentType = "text/html";
                        ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        await ctx.Response.WriteAsync("<h1>500 - Internal Server Error</h1><p>Something went wrong on our end.</p>");
                        var stackTrace = new System.Diagnostics.StackTrace(ex, true);
                        var frame = stackTrace.GetFrame(0); // Get the first frame (or relevant frame)
                        var method = frame?.GetMethod();
                        var declaringType = method?.DeclaringType;
                        var namespaceName = declaringType?.Namespace;
                        var className = declaringType?.Name;
                        var methodName = method?.Name;
                        Log.Log.Error(ex.Message,namespaceName,className,methodName);
                    }
                    //await next();
                    if (ctx.Response.StatusCode == 404 && !ctx.Response.HasStarted)
                    {
                        //string originalPath = ctx.Request.Path.Value;
                        //ctx.Response.ContentType = "text/html";
                        //ctx.Items["originalPath"] = originalPath;
                        //ctx.Request.Path = "/error/404";
                        //await next();
                        ctx.Response.ContentType = "text/html";
                        await ctx.Response.WriteAsync("<h1>404 - Page Not Found</h1><p>Sorry, the page you're looking for does not exist.</p>");
                    }
                });

            }
            catch (Exception ex)
            {
                Log.Log.Error(ex.Message, Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {


                var jwtSettings = Configuration.GetSection("JwtSettings");
                var UseCacheForTemplateDetails = Configuration.GetValue<bool>("UseCacheForTemplateDetails");
                var UseCacheForHolidayPackages = Configuration.GetValue<bool>("UseCacheForHolidayPackages");
                var startJobWorker = Configuration.GetValue<bool>("startJobWorker");
                Cms.Services.Extensions.DependancyRegistrar.startJobWorker = startJobWorker;
                Cms.Services.Extensions.DependancyRegistrar.UseCacheForHolidayPackages = UseCacheForHolidayPackages;
                Cms.Services.Extensions.DependancyRegistrar.UseCacheForTemplateDetails = UseCacheForTemplateDetails;
                var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);
                services.AddDbContext<CMSDBContext>(option =>
                {
                    option.UseSqlServer(Configuration.GetConnectionString("CmsConnectionStringDev"));


                }); 
                services.AddDbContext<TMMDBContext>(option =>
                {
                    option.UseSqlServer(Configuration.GetConnectionString("CmsConnectionStringTMMDev"));
                }); 
                services.AddDbContext<ActivityAdminDBContext>(option =>
                {
                    option.UseSqlServer(Configuration.GetConnectionString("CmsConnectionStringActivityAdminDev"));
                });
                services.AddDbContext<HotelAdminDBContext>(option =>
                {
                    option.UseSqlServer(Configuration.GetConnectionString("CmsConnectionStringHotelAdminDev"));
                });
                services.AddDbContext<TransferAdminDBContext>(option =>
                {
                    option.UseSqlServer(Configuration.GetConnectionString("CmsConnectionStringTransferAdminDev"));
                });
               if(startJobWorker)
                    services.AddHostedService<JobWorkerService>();
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile<MappingProfile>();
                    mc.DisableConstructorMapping();
                    mc.AllowNullDestinationValues = true;
                    mc.AllowNullCollections = true;


                });
                services.AddMemoryCache();

                IMapper mapper = mappingConfig.CreateMapper();

                services.AddSingleton(mapper);
                services.RegisterDependancy();
                services.RegisterServiceDependancy();
                services.DepandancyCMS();
                services.AddControllers().AddJsonOptions(option =>
                {
                    option.AllowInputFormatterExceptionMessages = true;
                    option.JsonSerializerOptions.DefaultIgnoreCondition = new JsonIgnoreCondition();
                    option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                }).AddXmlSerializerFormatters();
                services.AddHttpClient("httpclient", option =>
                {
                    option.MaxResponseContentBufferSize = long.MaxValue;

                });
                services.AddResponseCompression(option =>
                {
                    option.Providers.Clear();
                    option.EnableForHttps = true;

                });
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "CMS API",
                        Description = "CMS API for Swagger integration",
                        // Add url of term of service details

                    });
                    options.OperationFilter<SwaggerXmlInputFilter>(); // Add this line to include XML support

                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = "Bearer",
                    });
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = JwtBearerDefaults.AuthenticationScheme
                       }
                      },
                      new string[] { }
                    }
                      });

                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    options.IncludeXmlComments(xmlPath);
                });
                services.AddRouting();
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "Bearer";
                    options.DefaultScheme = "Bearer";
                    options.DefaultSignInScheme = "Bearer";
                    options.DefaultSignOutScheme = "Bearer";
                    options.DefaultChallengeScheme = "Bearer";
                })
                   .AddJwtBearer(options =>
                   {
                       options.RequireHttpsMetadata = false;
                       options.SaveToken = true;
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuerSigningKey = true,
                           IssuerSigningKey = new SymmetricSecurityKey(key),
                           ValidateIssuer = true,
                           ValidateAudience = true,
                           ValidAudience = jwtSettings["Audience"],
                           ValidIssuer = jwtSettings["Issuer"],

                       };

                   });

                services.AddAuthorization(options =>
                {
                    options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                        .AddAuthenticationSchemes().Build();
                });
                // Setup Initial Data Cache
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
