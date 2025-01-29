using Microsoft.EntityFrameworkCore;

using HitechSoftware.TestTechnique.Modeles;

namespace HitechSoftware.TestTechnique.Donnees;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {

    }

    public DbSet<Depense> Depenses { get; set; }
}