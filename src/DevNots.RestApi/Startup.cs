using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DevNots.Application.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.CompilerServices;
using MongoDB.Bson.Serialization.Serializers;

namespace DevNots.RestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        #region Constant Variables

        private string PROJECT_TITLE = "Devnots Advanced Note API Documentation";

        private string PROJECT_VERSION = "1.0";

        private string PROJECT_DESCRIPTION = "Advanced Note Application";

        private string GENERAL_URL = "https://github.com/fasetto/devnots/issues";

        private string CONTACT_NAME = "Team of Devnots";

        #endregion
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // var appConfig = ReadAppConfig();
            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STR");

            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("Environment variable 'CONNECTION_STR' is not set. \nExiting..");
                Environment.Exit(0);
            }

            // services.AddSingleton(appConfig);
            services.AddApplicationDependencies();
            services.AddMongoDb(connectionString);
            services.AddControllers();

            services.AddSwaggerDocument(config =>
                config.PostProcess = (settings =>
                        {
                            settings.Info.Title = PROJECT_TITLE;
                            settings.Info.Version = PROJECT_VERSION;
                            settings.Info.Description = PROJECT_DESCRIPTION;
                            settings.Info.Contact = new NSwag.OpenApiContact
                            {
                                Name = CONTACT_NAME,
                                Url = GENERAL_URL
                            };
                        }
                     )); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerUi3();

            app.UseOpenApi();
        }

        public AppConfig ReadAppConfig()
        {
            var path = "./appConfig.json";

            #if DEBUG
                path = "../../appConfig.json";
            #endif

            var appConfigJson = File.ReadAllText(path);
            var serializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<AppConfig>(appConfigJson, serializerOptions);
        }
    }
}
