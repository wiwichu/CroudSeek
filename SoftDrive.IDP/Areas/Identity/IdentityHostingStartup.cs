using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SoftDrive.IDP.Areas.Identity.Data;
using SoftDrive.IDP.Data;

[assembly: HostingStartup(typeof(SoftDrive.IDP.Areas.Identity.IdentityHostingStartup))]
namespace SoftDrive.IDP.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<UserDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("SoftDriveIDPContextConnection")));

                //services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //    .AddEntityFrameworkStores<SoftDriveIDPContext>();

                services.AddIdentity<ApplicationUser, IdentityRole>(
                    options => options.SignIn.RequireConfirmedAccount = true)
                  .AddEntityFrameworkStores<UserDbContext>()
                  .AddDefaultTokenProviders();
            });



        }
    }
}