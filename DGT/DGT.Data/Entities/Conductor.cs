using System.Collections.Generic;

namespace DGT.Data.Entities
{
    public class Conductor
    {
        public int Id { get; set; }

        public string DNI { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public double Puntos { get; set; }

        public ICollection<Vehiculo> Vehiculos { get; set; }

        public virtual ICollection<Infraccion> Infracciones { get; set; }
    }
}
