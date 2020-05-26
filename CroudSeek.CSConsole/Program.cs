using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CroudSeek.CSConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }
        static async Task MainAsync(string[] args)
        {
            string json = @"{""Name"":""WhatAView - updated, again"",""Description"":""What a View"",""ImageUrl"":null,""IsPrivate"":false," +
                @"""ExcludeByDefault"":false,""Age"":-1,""UserWeights"":[{""UserId"":3,""ExcludeUser"":false,""Weight"":90},{""UserId"":2," +
                @"""ExcludeUser"":false,""Weight"":10}]}";

            var viewJson = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "api/quests/1/views/1";

            var hClient = new HttpClient();
            hClient.BaseAddress = new Uri("http://localhost:51044/");

            Thread.Sleep(20000);

            var response = await hClient.PutAsync(url, viewJson);
        }
    }
}
