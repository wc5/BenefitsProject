using BenefitsProject.Core.Application.Contracts;
using BenefitsProject.Core.Application.Services;
using BenefitsProject.Core.Application.Validators;
using BenefitsProject.Core.Domain.Contracts;
using BenefitsProject.Core.Domain.Entities;
using BenefitsProject.Infrastructure.Persistence;
using BenefitsProject.WebApi.Models.Mappings;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BenefitsProject.WebApi
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_localhost";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            services.AddDbContext <BenefitsDbContext>(options =>
                  options.UseSqlite("Data Source=benefits_database.sqlite"));

            services.AddControllers();

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BenefitsProject.WebApi", Version = "v1" });
            });

            services.AddScoped(typeof(IBenefitCalculator), typeof(BenefitCalculator));
            services.AddScoped(typeof(IEmployeeService), typeof(EmployeeService));
            services.AddScoped(typeof(IEmployeeRepository), typeof(EmployeeRepository));

            services.AddTransient(typeof(IValidator<Employee>), typeof(EmployeeValidator));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BenefitsProject.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
