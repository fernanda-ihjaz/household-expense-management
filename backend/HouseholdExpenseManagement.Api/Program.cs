using HouseholdExpenseManagement.Application;
using HouseholdExpenseManagement.Domain.AggregatesModel;
using HouseholdExpenseManagement.Domain.AggregatesModel.CategoryAggregate;
using HouseholdExpenseManagement.Domain.AggregatesModel.Person;
using HouseholdExpenseManagement.Domain.AggregatesModel.Transaction;
using HouseholdExpenseManagement.Infrastructure.EntityFramework;
using HouseholdExpenseManagement.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

namespace HouseholdExpenseManagement.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration;
            var services = builder.Services;
            var host = builder.Host;

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(
                    "logs/log-.txt", 
                    rollingInterval: RollingInterval.Day, 
                    retainedFileCountLimit: 7)
                .CreateLogger();

            host.UseSerilog();

            services.AddControllers();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            services.AddMediatR(typeof(ApplicationAssemblyMarker).Assembly);

            services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Household Expense Management API",
                    Version = "v1",
                    Description = "API para gerenciamento de despesas domésticas."
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy
                        .WithOrigins("*")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            app.UseSerilogRequestLogging();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
            }

            app.UseCors("AllowFrontend");
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
