using GameZone.Data;
using GameZone.Models;
using GameZone.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GameZone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //coonection
            var connectionstring = builder.Configuration.GetConnectionString("connection")
                ?? throw new InvalidOperationException("no coonection found");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionstring));

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<ICategoriesService, CategoriesServices>();
            builder.Services.AddScoped<IDevicesServices, DevicesServices>();
            builder.Services.AddScoped<IGameServices, GameServices>();
            builder.Services.AddScoped<IRoleServices, RoleServices>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
                options =>
                {
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireUppercase = false;
                })
            .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders();
            builder.Services.AddScoped<IRegisterServices, RegisterServices>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
