﻿using System;
using System.Collections.Generic;
using System.IO;
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
        IHostingEnvironment _hostingEnvironment;
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _hostingEnvironment = env;
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
            services.AddTransient<ICardsTemplatesService>(serviceProvider => {
                var contentRoot = Path.Combine(_hostingEnvironment.ContentRootPath, Configuration["AppSettings:CardsTemplateName"]);
                return new CardsTemplateService(contentRoot);
            });
            services.AddTransient<ICardTableService, CardTableService>();

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
                    template: "{controller=Cards}/{action=Index}/{id?}");
            });
        }
    }
}
