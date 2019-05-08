
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DGT.Data.Entities
{
    public class Infraccion
    {
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public double Puntos { get; set; }

        public virtual ICollection<ConductorInfraccion> Conductores { get; set; }

    }
}
