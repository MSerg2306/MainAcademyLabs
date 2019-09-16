using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArvitumNew.Models;
using ArvitumNew.Service.CountExamination;
using ArvitumNew.Util;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArvitumNew
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
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //для определения текущего пользователя
            services.AddTransient<CountPayExamination>();
            services.AddTransient<CountMakeExamination>();
            services.AddTransient<CountDeliveryExamination>();

            services.AddMvc();

            ////стандартное объявление Identity
            //services.AddIdentity<User, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            //объявление Identity с настройкой пароля
            services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 8;   // минимальная длина
                opts.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы, если равно true, то пароль должен будет иметь как минимум один символ, который не является алфавитно-цифровым
                opts.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре, если равно true, то пароль должен будет иметь как минимум один символ в нижнем регистре
                opts.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре, если равно true, то пароль должен будет иметь как минимум один символ в верхнем регистре
                opts.Password.RequireDigit = false; // требуются ли цифры, если равно true, то пароль должен будет иметь как минимум одну цифру
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //подмена обработчика для View
            services.Configure<MvcViewOptions>(options => {
                //options.ViewEngines.Clear();
                options.ViewEngines.Insert(1, new CustomViewEngine());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMiddleware<CountPayExaminationMiddleware>();
            app.UseMiddleware<CountMakeExaminationMiddleware>();
            app.UseMiddleware<CountDeliveryExaminationMiddleware>();

            app.UseMvc(routes =>
            {
                //routes.MapRoute("api/get", async context =>
                //{
                //    await context.Response.WriteAsync("для обработки использован маршрут api/get");
                //});

                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
