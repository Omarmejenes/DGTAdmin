using System;

namespace DGT.Data.Entities
{
    public class ConductorInfraccion
    {
        public ConductorInfraccion()
        {
            Conductor = new Conductor();
            Infraccion = new Infraccion();
        }
        public int Id { get; set; }

        public int ConductorId { get; set; }

        public int InfraccionId { get; set; }

        public Conductor Conductor { get; set; }

        public Infraccion Infraccion { get; set; }

        public DateTime Fecha { get; set; }
    }
}
