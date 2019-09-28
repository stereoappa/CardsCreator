using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CardsCreator.DomainModel.Interfaces;
using CardsCreator.Infrastructure.Yandex;
using CardsCreator.Application;
using System.IO;

namespace CardsCreator.WebUI
{
    public class Startup
    {
        IHostingEnvironment _hostingEnvironment;
        readonly string _localCorsPolicy = "localCorsPolicy";
        readonly string[] _localCorsHosts = new string[] { "http://localhost:8080", "http://cardscreator.local" };

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _hostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient("yandexApiClient", c =>
            {
                c.BaseAddress = new Uri(Configuration["AppSettings:YandexApiBaseUri"]);
            });

            services.AddTransient<ITranslator, YandexTranslate>();
            services.AddTransient<ILanguageTypeConverter, YandexLanguageTypeConverter>();
            services.AddTransient<ICardRestoreService, CardRestoreService>();
            services.AddTransient<ICardsTemplatesService>(serviceProvider => {
                var contentRoot = Path.Combine(_hostingEnvironment.ContentRootPath, Configuration["AppSettings:CardsTemplateName"]);
                return new CardsTemplateService(contentRoot);
            });
            services.AddTransient<ICardsTableService, CardsTableService>();
            services.AddTransient<ICardParserService, CardParserService>();

            services.AddCors(options =>
            {
                options.AddPolicy(_localCorsPolicy,
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    //builder.WithOrigins(_localCorsHosts);
                });
            });

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
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseCors(_localCorsPolicy);

            app.UseMvc();
        }
    }
}
