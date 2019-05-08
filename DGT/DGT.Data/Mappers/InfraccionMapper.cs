using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DGT.Data.Entities;

namespace DGT.Data.Mappers
{
    class InfraccionMapper : EntityTypeConfiguration<Infraccion>
    {
        public InfraccionMapper()
        {
            ToTable("Infracciones");

            HasKey(i => i.Id);
            Property(i => i.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(i => i.Id).IsRequired();

            Property(i => i.Descripcion).IsRequired();
            Property(i => i.Descripcion).HasMaxLength(255);
            Property(i => i.Descripcion).IsUnicode(false);

        }
    }
}
