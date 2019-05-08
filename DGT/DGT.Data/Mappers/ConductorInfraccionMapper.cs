using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DGT.Data.Entities;

namespace DGT.Data.Mappers
{
    class ConductorInfraccionMapper : EntityTypeConfiguration<ConductorInfraccion>
    {
        public ConductorInfraccionMapper()
        {
            ToTable("ConductoresInfracciones");

            HasKey(v => v.Id);
            Property(v => v.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(v => v.Id).IsRequired();

            Property(v => v.Fecha).IsRequired();
            Property(v => v.Fecha).HasColumnType("Date");

            HasRequired(v => v.Conductor).WithMany(v => v.Infracciones).HasForeignKey(c => c.ConductorId).WillCascadeOnDelete(false);
            HasRequired(v => v.Infraccion).WithMany(v => v.Conductores).HasForeignKey(c => c.InfraccionId).WillCascadeOnDelete(false);
        }
    }
}
