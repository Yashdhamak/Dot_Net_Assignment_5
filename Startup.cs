using AutoMapper;
using Employee_Management_System.Data;
using Employee_Management_System.Profiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.Azure.Cosmos;

namespace Employee_Management_System
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configure Cosmos DB
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseCosmos(
                    Configuration.GetConnectionString("CosmosConnection"), // Retrieve from appsettings.json
                    databaseName: "EmployeeManagementDB" // Replace with your desired database name
                );
            });

            // Register Cosmos Client
            services.AddSingleton((serviceProvider) =>
            {
                var connectionString = Configuration.GetConnectionString("CosmosConnection");
                var cosmosClient = new CosmosClient(connectionString);
                return cosmosClient;
            });

            // Register AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));

            // Add Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Employee Management API", Version = "v1" });
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Swagger UI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee Management API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
