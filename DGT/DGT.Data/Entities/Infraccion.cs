
using System.Collections.Generic;

namespace DGT.Data.Entities
{
    public class Infraccion
    {
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public double Puntos { get; set; }

        public virtual ICollection<Conductor> Conductores { get; set; }

    }
}
