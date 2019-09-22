﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardsCreator.Application;
using CardsCreator.DomainModel.Interfaces;
using CardsCreator.Infrastructure.Yandex;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CardsCreator.WebUI
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddHttpClient("yandexApiClient", c =>
            {
                c.BaseAddress = new Uri(Configuration["AppSettings:YandexApiBaseUri"]);
            });

            services.AddTransient<ITranslator, YandexTranslate>();
            services.AddTransient<ILanguageTypeConverter, YandexLanguageTypeConverter>();
            services.AddTransient<ICardRecoveryService, CardRecoveryService>();
            services.AddTransient<ICardDocumentService, CardDocumentService>();
            //services.AddHttpClient();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
