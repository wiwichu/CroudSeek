using AutoMapper;
using CroudSeek.API.DbContexts;
using CroudSeek.API.Services;
using CroudSeek.Identity;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CroudSeek.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;
            services.AddIdentityServices(Configuration);
            // Add an authorization policy
            services.AddAuthorizationCore(authorizationOptions =>
            {
                authorizationOptions.AddPolicy(
                    CroudSeek.Shared.Policies.CanManageQuests,
                    CroudSeek.Shared.Policies.CanManageQuestsPolicy());
            });


            var requireAuthenticatedUserPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();

            //services.AddAuthentication(
            //    IdentityServerAuthenticationDefaults.AuthenticationScheme)
            //.AddIdentityServerAuthentication(options =>
            //{
            //    options.Authority = "https://localhost:5003/";
            //    options.ApiName = "croudseekapi";
            //});

            services.AddAuthentication("BearerCS").AddJwtBearer("BearerCS",
             options =>
             {
                 options.Authority = "https://localhost:5003";
                 options.Audience = "croudseekapi";
                 options.RequireHttpsMetadata = false;

                 options.TokenValidationParameters = new
                TokenValidationParameters()
                 {
                     ValidateAudience = false
                 };
             });
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddControllers(setupAction =>
            {
                //setupAction.Filters.Add(new AuthorizeFilter(requireAuthenticatedUserPolicy));
                setupAction.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                setupAction.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
                setupAction.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
                setupAction.Filters.Add(new ProducesAttribute("application/json", "application/xml"));
                setupAction.Filters.Add(new ConsumesAttribute("application/json"));
                //setupAction.ReturnHttpNotAcceptable = true;

                var jsonOutputFormatter = setupAction.OutputFormatters.OfType<NewtonsoftJsonOutputFormatter>().FirstOrDefault();

                if (jsonOutputFormatter != null)
                {
                    if(jsonOutputFormatter.SupportedMediaTypes.Contains("text.json"))
                    {
                        jsonOutputFormatter.SupportedMediaTypes.Remove("text.json");
                    }
                }
            })
                .AddNewtonsoftJson(setupAction =>
             {
                 setupAction.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
             })
            .AddXmlDataContractSerializerFormatters()
            .ConfigureApiBehaviorOptions(setupAction =>
            {
                setupAction.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Type = "https://courselibrary.com/modelvalidationproblem",
                        Title = "One or more model validation errors occurred.",
                        Status = StatusCodes.Status422UnprocessableEntity,
                        Detail = "See the errors property for details.",
                        Instance = context.HttpContext.Request.Path
                    };

                    problemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);

                    return new UnprocessableEntityObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            })
            ;

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<ICroudSeekRepository, CroudSeekRepository>();
            var courseLibraryDBConnectionString = Configuration["connectionStrings:courseLibraryDBConnectionString"];
            var croudSeekDBConnectionString = Configuration["connectionStrings:croudSeekDBConnectionString"];
            services.AddDbContext<CroudSeekContext>(options =>
            {
                options.UseSqlServer(croudSeekDBConnectionString
                    //@"Server=(localdb)\mssqllocaldb;Database=CroudSeekDB;Trusted_Connection=True;"
            );
            });

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                    "CroudSeekOpenAPISpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "CroudSeek API",
                       Version = "1"
                    });
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                setupAction.IncludeXmlComments(xmlCommentsFullPath);
            });
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
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                    });
                });

            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint(
                    "/swagger/CroudSeekOpenAPISpecification/swagger.json",
                    "CroudSeek API");
                setupAction.RoutePrefix = "";
            });

            app.UseCors("MyPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
