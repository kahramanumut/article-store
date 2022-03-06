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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OData;
using Article.Application;
using Article.Infrastructure.EfCore;
using Article.Application.Interfaces;
using Article.Application.Interfaces.Repositories;
using Article.Infrastructure.EfCore.Repositories;
using System.Reflection;

namespace Article.WebApi
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
            string connectionString = String.Empty;
            if(String.IsNullOrEmpty(Environment.GetEnvironmentVariable("ISDOCKER")))
                connectionString = Configuration.GetConnectionString("Default");
            else
                connectionString = Configuration.GetConnectionString("DockerDefault");
            
            services.AddDbContext<ArticleDbContext>(options =>
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly("Article.Infrastructure")));

            services.Register(Configuration);
            services.AddTransient( typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>) );
            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddTransient<IArticleRepositoryAsync, ArticleRepositoryAsync>();

            services.AddControllers().AddOData(opt => opt.Count().Filter().Expand().Select().OrderBy().SetMaxTop(null));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Article.WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Article.WebApi v1"));
            }

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var dbContext = serviceScope.ServiceProvider.GetService<ArticleDbContext>())
                {
                    SeedData.SeedSampleData(dbContext);
                }
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
