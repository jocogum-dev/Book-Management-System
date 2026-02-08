using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BMS.Data;

public class BookDbContextFactory : IDesignTimeDbContextFactory<BookDbContext>
{
    public BookDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../BMS.Web"))
            .AddJsonFile("appsettings.Development.json", optional: false)
            .Build();

        var connectionString = config.GetConnectionString("BookDb");
        var options = new DbContextOptionsBuilder<BookDbContext>().UseSqlite(connectionString).Options;
        return new BookDbContext(options);
    }
}
