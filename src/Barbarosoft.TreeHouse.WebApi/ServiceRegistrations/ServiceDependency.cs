using Barbarosoft.TreeHouse.Domain.Ports;
using Barbarosoft.TreeHouse.Repository.SqlRepository.Contexts;
using Barbarosoft.TreeHouse.Repository.SqlRepository.Repositories;
using Barbarosoft.TreeHouse.Service;

namespace Barbarosoft.TreeHouse.WebApi.ServiceRegistrations;

public static class ServiceDependency
{
    public static void AddServiceDependencies(this IServiceCollection services)
    {
        services.AddScoped<ICourseApplicationContext, CourseApplicationContext>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<ICourseRepository, CourseRepository>();
    }
}
