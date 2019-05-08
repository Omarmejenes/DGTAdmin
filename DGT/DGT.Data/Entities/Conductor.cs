using System.Collections.Generic;

namespace DGT.Data.Entities
{
    public class Conductor
    {
        public int Id { get; set; }

        public string DNI { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public ICollection<Vehiculo> Vehiculos { get; set; }

        public virtual ICollection<ConductorInfraccion> Infracciones { get; set; }
    }
}
