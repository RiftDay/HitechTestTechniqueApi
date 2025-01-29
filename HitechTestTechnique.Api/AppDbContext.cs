using Microsoft.EntityFrameworkCore;

using HitechSoftware.TestTechnique.Modeles;

namespace HitechSoftware.TestTechnique.Donnees;

public class AppDbContext : DbContext
{
    public DbSet<Depense> Depenses { get; set; }
}