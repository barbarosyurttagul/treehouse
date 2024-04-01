using Barbarosoft.TreeHouse.Repository.SqlRepository.Contexts;
using Barbarosoft.TreeHouse.WebApi.Middlewares;
using Barbarosoft.TreeHouse.WebApi.ServiceRegistrations;
using Microsoft.EntityFrameworkCore;

namespace Barbarosoft.TreeHouse.WebApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddLocalizationSupport();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // AddDbContext
            builder.Services.AddDbContext<CourseApplicationContext>(options =>
                    options.UseSqlServer(
                        Environment.GetEnvironmentVariable("SQL_CONNECTION"))
                    );
            builder.Services.AddServiceDependencies();

            var app = builder.Build();
            app.UseLocalization();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
