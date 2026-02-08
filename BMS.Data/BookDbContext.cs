using BMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BMS.Data;

public class BookDbContext : IdentityDbContext
{
    public DbSet<BookFile> Books { get; set; } = null!;

    public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookFile>().HasKey(b => b.Id);
        base.OnModelCreating(modelBuilder);
    }
}
