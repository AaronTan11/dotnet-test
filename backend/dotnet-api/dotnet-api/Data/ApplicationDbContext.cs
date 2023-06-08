using Microsoft.EntityFrameworkCore;
using dotnet_api.Models;

public class ApplicationDbContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public ApplicationDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to postgres with connection string from app settings
        options.UseNpgsql(Configuration.GetConnectionString("PostgresServer"));
    }

    // public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    // {
    // }

    public DbSet<User> User { get; set; }
}
