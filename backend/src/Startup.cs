using System.Text.Json.Serialization;
using BenefitsApp.Business.Managers;
using BenefitsApp.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BenefitsApp
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
      services.AddCors(options =>
      {
        options.AddDefaultPolicy(
          builder =>
            builder
              .AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod()
              .Build()
        );
      });
      services.AddControllers()
        .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "BenefitsApp", Version = "v1" });
        c.CustomSchemaIds(x => x.FullName);
      });
      services.AddDbContext<DatabaseContext>(options =>
      {
        if (Configuration["ASPNETCORE_ENVIRONMENT"] == "Production")
        {
          var connectionString = Configuration.GetConnectionString("default");
          options.UseSqlServer(connectionString);
        }
        else
        {
          options.UseSqlite(@"Data Source=app.db");
        }
      });
      services.AddScoped<IEmployeeManager, EmployeeManager>();
      services.AddScoped<IBenefitsManager, BenefitsManager>();
      services.AddScoped<IDiscountsManager, DiscountsManager>();
      services.AddScoped<IEnrollmentManager, EnrollmentManager>();
      services.AddBenefits();
      services.AddDiscounts();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DatabaseContext context)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseSwagger();
      app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BenefitsApp v1"));

      // app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseCors();
      app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

      context.Database.Migrate();
    }
  }
}