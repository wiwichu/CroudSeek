using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CroudSeek.Core.Services;

namespace CroudSeek.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
//            builder.RootComponents.Add<CroudSeek.Core.App>("app");
            builder.RootComponents.Add<App>("app");
            builder.Services.AddHttpClient<IQuestDataService, QuestDataService>(
                 client =>
                 {
                     client.BaseAddress = new Uri("http://localhost:51044");
                 }
                 );
            builder.Services.AddHttpClient<IZoneDataService, ZoneDataService>(
                client =>
                {
                    client.BaseAddress = new Uri("http://localhost:51044");
                }
                );
            builder.Services.AddHttpClient<IDataPointDataService, DataPointDataService>(
                client =>
                {
                    client.BaseAddress = new Uri("http://localhost:51044");
                }
                );
            builder.Services.AddHttpClient<IViewDataService, ViewDataService>(
                client =>
                {
                    client.BaseAddress = new Uri("http://localhost:51044");
                }
                );
                 ;
            await builder.Build().RunAsync();
        }
    }
}
