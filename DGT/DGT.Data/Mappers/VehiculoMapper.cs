using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DGT.Data.Entities;

namespace DGT.Data.Mappers
{
    class VehiculoMapper : EntityTypeConfiguration<Vehiculo>
    {
        public VehiculoMapper()
        {
            ToTable("Vehiculos");

            HasKey(v => v.Id);
            Property(v => v.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(v => v.Id).IsRequired();

            Property(v => v.Matricula).IsRequired();
            Property(v => v.Matricula).HasMaxLength(50);
            Property(v => v.Matricula).IsUnicode(false);

            Property(v => v.Marca).IsRequired();
            Property(v => v.Marca).HasMaxLength(50);
            Property(v => v.Marca).IsUnicode(false);

            Property(v => v.Modelo).IsRequired();
            Property(v => v.Modelo).HasMaxLength(50);
            Property(v => v.Modelo).IsUnicode(false);

            HasOptional(v => v.Conductor).WithMany(v => v.Vehiculos).Map(c => c.MapKey("ConductorID")).WillCascadeOnDelete(false);
        }
    }
}
