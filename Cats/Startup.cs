using Cats.Infrastructure;
using Library;
using Library.Adapters;
using Library.FileReaders;
using Library.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Models;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace Cats
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
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(new PetsContext());
            services.AddSingleton<IAdapter, JsonAdapter>();
            services.AddScoped<IFileSystem, FileSystem>();
            services.AddScoped<IRepository<Owner>, OwnersRepository>();
            services.AddScoped<IService<GetCatsByOwnersGenderRequest, GetCatsByOwnersGenderResponse>, GetPetsByOwnersGenderService>();

            services.AddScoped<ExceptionFilter>();

            var serviceProvider = services.BuildServiceProvider();
            var adapter = serviceProvider.GetService<IAdapter>();
            PetsContext context = (PetsContext)serviceProvider.GetService(typeof(PetsContext));

            var filePath = Configuration["FilePath"];
            var fileInfo = new FileInfo(filePath);
            adapter.Fill(fileInfo.FullName, context);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Cats API", Version = "v1" });
            });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cats API V1");
            });

            app.UseMiddleware<UnhandledExceptionMiddleware>();

            app.UseMvc();

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
