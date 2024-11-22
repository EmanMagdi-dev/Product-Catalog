using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Product_Catalog.BLL.Services;
using Product_Catalog.DAL.Repositories;
using Product_Catalog.DTOs;
using Product_Catalog.Models;

namespace Product_Catalog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession(options =>
            {
                //options.IdleTimeout = TimeSpan.FromSeconds(1800);
                //options.Cookie.HttpOnly = true;
                //options.Cookie.IsEssential = true;
            });


            builder.Services.AddDbContext<ProductDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

            builder.Services.AddScoped<Icommon<Product>, ProductRepository>();
            builder.Services.AddScoped<Icommon<Category>, CategoryRepository>();
            builder.Services.AddScoped<ISelectList<Category>, CategoryRepository>();
            builder.Services.AddScoped<ILogg, LogRepository>();
            builder.Services.AddScoped(typeof(IFilter<>), typeof(FilterRepo<>));


            builder.Services.AddScoped<UserVM>();
            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ProductDbContext>();
            builder.Services.Configure<IdentityOptions>(options =>
            {

                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
            });
            ;
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
            app.UseSession();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Product}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
