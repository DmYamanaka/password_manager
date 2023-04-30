using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using PasswordManager.ServiceInterface;
using PasswordManager.Services.UserService;
using PaswordManager.Context;
using System.Web.Helpers;

namespace PaswordManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie("Cookies", options =>
            {
                options.Cookie.Name = "auth_cookie";
                options.Cookie.SameSite = SameSiteMode.None;
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = redirectContext =>
                    {
                        redirectContext.HttpContext.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    }
                };
            });


#pragma warning disable CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.
            string connection = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<PasswordManagerRepasitory>(options => options.UseSqlServer(connection));
            builder.Services.AddControllersWithViews(options =>
            {
                //options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
            builder.Services.AddCors();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //builder.Services.AddAntiforgery(options =>
            //{
            //    options.FormFieldName = "AntiforgeryFieldname";
            //    options.HeaderName = "XSRF-TOKEN";
            //    options.Cookie.Name = "AntiForgeryCookieName";
            //});

            var app = builder.Build();
            var antiforgery = app.Services.GetRequiredService<IAntiforgery>();

            app.UseSwagger();
            app.UseSwaggerUI(c=> { c.SwaggerEndpoint("/swagger/v1/swagger.json", "PasswordManagerAPI");
                c.RoutePrefix = string.Empty;
            });
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseRouting();
            app.UseCors(policy=>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.SetIsOriginAllowed(hostName => true);
                    policy.AllowCredentials();
                });
            app.UseAuthentication();
            app.UseAuthorization();

            //TODO Нужно подумать как реализовать защиту от csrf атак, возможно нужно будет логин выносить в MVC, а не в ангуляр
            //app.Use((context, next) =>
            //{
            //    string path = context.Request.Path.Value;
            //
            //    if (path.IndexOf("/api/", StringComparison.OrdinalIgnoreCase) != -1)
            //    {
            //        var tokens = antiforgery.GetAndStoreTokens(context);
            //        context.Response.Cookies.Append("X-XSRF-TOKEN", tokens.RequestToken,
            //          new CookieOptions() { HttpOnly = false });
            //    }
            //
            //    return next(context);
            //});
            app.MapDefaultControllerRoute();

            app.Run();
        }
    }
}