using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DGT.Data.Entities;

namespace DGT.Data.Mappers
{
    class ConductorMapper : EntityTypeConfiguration<Conductor>
    {
        public ConductorMapper()
        {
            ToTable("Conductores");

            HasKey(c => c.Id);
            Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.Id).IsRequired();

            Property(c => c.DNI).IsRequired();
            Property(c => c.DNI).HasMaxLength(50);

            Property(c => c.Nombres).IsRequired();
            Property(c => c.Nombres).HasMaxLength(50);

            Property(c => c.Apellidos).IsRequired();
            Property(c => c.Apellidos).HasMaxLength(50);

            Property(c => c.Puntos).IsRequired();
        }
    }
}
