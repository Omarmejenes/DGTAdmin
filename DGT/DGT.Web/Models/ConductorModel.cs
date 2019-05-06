using System.Collections.Generic;
using DGT.Data.Entities;

namespace DGT.Web.Models
{
    public class ConductorModel
    {
        public int Id { get; set; }

        public string DNI { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public double Puntos { get; set; }

    }
}