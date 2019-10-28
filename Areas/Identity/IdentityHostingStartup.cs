using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using University.Areas.Identity.Data;

[assembly: HostingStartup(typeof(University.Areas.Identity.IdentityHostingStartup))]
namespace University.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<UniversityIdentityDbContext>(options =>
                    options.UseSqlite(context.Configuration.GetConnectionString("IdentityConnection")));

                services.AddDefaultIdentity<Usuario>()
                    .AddEntityFrameworkStores<UniversityIdentityDbContext>();
            });
        }
    }
}