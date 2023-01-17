using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Boxers;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Configurations;

namespace Persistence.Data;

public class VirtuaBoxerDbContext : DbContext
{
    public VirtuaBoxerDbContext(DbContextOptions<VirtuaBoxerDbContext> options) : base(options)
    {

    }

    public DbSet<Boxer> Boxers { get; set; }
    public DbSet<Stat> Stats { get; set; } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new BoxerEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new StatEntityTypeConfiguration());
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VirtuaBoxerDbContext).Assembly);

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.EnableSensitiveDataLogging();
    }
}
