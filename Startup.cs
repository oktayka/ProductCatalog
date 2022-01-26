using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using ProductCatalog.Models;
using ProductCatalog.Services;

namespace ProductCatalog
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
            ConfigureDatabase(services);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductCatalog", Version = "v1" });
            });
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            var settings = GetDatabaseSettings();
            services.AddSingleton(_ = CreateDatabase(settings));
            AddCollection<Book>(services, settings.ProductCollectionName);
            AddCollection<Phone>(services, settings.ProductCollectionName);
        }

        private void AddCollection<T>(IServiceCollection services, string collectionName) where T : Product, IProduct
        {
            services.AddSingleton(s=>s.GetRequiredService<IMongoDatabase>().GetCollection<T>(collectionName));
            services.AddSingleton<IProductService<T>, ProductService<T>>();
        }

        private IMongoDatabase CreateDatabase(MongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            return client.GetDatabase(settings.Database);
        }

        private MongoDbSettings GetDatabaseSettings() => Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductCatalog v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
