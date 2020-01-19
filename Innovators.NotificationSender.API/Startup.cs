using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.AspNetCore;
using Innovators.NotificationSender.Service.Mappings.Profiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;

using Innovators.NotificationSender.Api.ConventionsAndFilters;
using Innovators.NotificationSender.Domain.Validations;
using Innovators.NotificationSender.Persistence;
using Serilog;
using Innovators.FileService.API.ConventionsAndFilters;
using Innovators.NotificationSender.Service.Services;

namespace Innovators.NotificationSender.API
{
    public class Startup
    {
        private readonly string _allowedOrigins = "NotificationOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

           services.AddPersistence(Configuration);

             services.AddScoped<INotificationService, NotificationService>();

            #region Options
            services.AddOptions();

            #endregion

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAutoMapper(Assembly.GetAssembly(typeof(BaseMappingProfile)));

            services.AddCors(options =>
            {
                options.AddPolicy(_allowedOrigins, builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-XSRF-TOKEN";
            });
            services.AddMvc(o =>
            {
                o.EnableEndpointRouting = false;
                o.Conventions.Add(new ApiExplorerGroupPerVersionConvention());
                o.Filters.Add(new ModelStateFilter());
            }).AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<CommonValidator>();
                fv.ImplicitlyValidateChildProperties = true;
            })

            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            .AddDataAnnotationsLocalization(
                options =>
                {
                      //  options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(ValidationMessageResources));
                });

            #region Customise default API behavour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            #endregion

            #region Authentication
            //Todo: Add authentication
            #endregion
            //sanaxavi
            #region Versioning
            services.AddApiVersioning(o =>
            {
                o.ApiVersionReader = new HeaderApiVersionReader("api-version");
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.ReportApiVersions = true;
            });
            #endregion

            #region Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new OpenApiInfo
                {
                   Title = Configuration.GetValue<string>("SwaggerDocOptions:Title"),
                   Version = Configuration.GetValue<string>("SwaggerDocOptions:Version"),
                   Description = Configuration.GetValue<string>("SwaggerDocOptions:Description")
                });


                c.OperationFilter<RemoveApiVersionFromParamsOperationFilter>();
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "notificationsender.xml");
            //    c.IncludeXmlComments(xmlPath);

                //Todo: add identity stuff
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }


            app.UseCors(_allowedOrigins);
            app.UseSerilogRequestLogging();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger(
                o =>
                {
                    o.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                    {
                        var paths = new OpenApiPaths();
                        foreach (var x in swaggerDoc.Paths)
                        {
                            var key = x.Key.Contains("{version}") ? x.Key.Replace("{version}", swaggerDoc.Info.Version) : x.Key;
                            paths.Add(key, x.Value);
                        }
                        swaggerDoc.Paths = paths;
                    });
                    o.RouteTemplate = "docs/{documentName}/swagger.json";
                });
            app.UseSwaggerUI(
                c =>
                {
                    c.SwaggerEndpoint("/docs/v1.0/swagger.json", "Innovators File API");
                }
            );
        }
    }
}

