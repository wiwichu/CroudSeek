using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CroudSeek.Client.Services;
using AutoMapper;
using CroudSeek.Client.MessageHandlers;

namespace CroudSeek.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services
                .AddTransient<CroudSeekApiAuthorizationMessageHandler>();

            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("OidcConfiguration", options.ProviderOptions);
                options.ProviderOptions.DefaultScopes.Add("croudseekapi");

            });

            builder.Services.AddHttpClient<IQuestDataService, QuestDataService>(
                 client =>
                 {
                     client.BaseAddress = new Uri("https://localhost:44367");
                 }
                 )
                .AddHttpMessageHandler<CroudSeekApiAuthorizationMessageHandler>();
            builder.Services.AddHttpClient<IZoneDataService, ZoneDataService>(
                client =>
                {
                    client.BaseAddress = new Uri("https://localhost:44367");
                }
                )
                .AddHttpMessageHandler<CroudSeekApiAuthorizationMessageHandler>();
            builder.Services.AddHttpClient<IDataPointDataService, DataPointDataService>(
                client =>
                {
                    client.BaseAddress = new Uri("https://localhost:44367");
                }
                )
                .AddHttpMessageHandler<CroudSeekApiAuthorizationMessageHandler>();
            builder.Services.AddHttpClient<IViewDataService, ViewDataService>(
                client =>
                {
                    client.BaseAddress = new Uri("https://localhost:44367");
                }
                )
                .AddHttpMessageHandler<CroudSeekApiAuthorizationMessageHandler>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            await builder.Build().RunAsync();
        }
    }
}
