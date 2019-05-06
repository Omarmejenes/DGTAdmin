using System;
using System.Collections.Generic;

namespace DGT.Data.Entities
{
    public class Vehiculo
    {
        public Vehiculo()
        {
            Conductor = new Conductor();
        }

        public int Id { get; set; }

        public string Matricula { get; set; }

        public string Marca { get; set; }

        public string Modelo { get; set; }

        public Conductor Conductor { get; set; }
    }
}
