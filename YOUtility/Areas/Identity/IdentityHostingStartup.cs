using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YOUtility.Areas.Identity.Data;
using YOUtility.Data;

[assembly: HostingStartup(typeof(YOUtility.Areas.Identity.IdentityHostingStartup))]
namespace YOUtility.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<UsersDB>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("UsersDBConnection")));

                services.AddDefaultIdentity<YOUtilityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<UsersDB>();
            });
        }
    }
}