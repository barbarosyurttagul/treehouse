using Barbarosoft.TreeHouse.Repository.SqlRepository.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Barbarosoft.TreeHouse.Repository.SqlRepository.Tests;

internal static class DbContextHelper
{
    private static readonly AsyncLocal<CourseApplicationContext> _context = new();

    public static CourseApplicationContext Context
    {
        get
        {
            if (_context.Value == null)
            {
                _context.Value = new CourseApplicationContext(GetOptions());
            }
            return _context.Value;
        }
    }

    public static DbContextOptions<CourseApplicationContext> GetOptions()
    {
        var options = new DbContextOptionsBuilder<CourseApplicationContext>();
        options.EnableSensitiveDataLogging(true);
        options.UseInMemoryDatabase(databaseName: "InMemoryDbForTesting");
        options.ConfigureWarnings(x => x.Ignore
            (InMemoryEventId.TransactionIgnoredWarning));

        return options.Options;
    }
}