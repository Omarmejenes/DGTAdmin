using System.Data.Entity;
using DGT.Data.Entities;
using DGT.Data.Mappers;

namespace DGT.Data
{
    public class DGTContext : DbContext
    {
        public DGTContext() : base("DGTConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DGTContext, DGTContextMigrationConfiguration>());
        }

        public DbSet<Infraccion> Infracciones { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Conductor> Conductores { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new InfraccionMapper());
            modelBuilder.Configurations.Add(new VehiculoMapper());
            modelBuilder.Configurations.Add(new ConductorMapper());
            modelBuilder.Entity<Conductor>()
                .HasMany<Infraccion>(c => c.Infracciones)
                .WithMany(i => i.Conductores)
                .Map(cs =>
                {
                    cs.ToTable("ConductoresInfracciones");
                    cs.MapLeftKey("ConductorId");
                    cs.MapRightKey("InfraccionId");
                    cs.ToTable("ConductoresInfracciones");
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
