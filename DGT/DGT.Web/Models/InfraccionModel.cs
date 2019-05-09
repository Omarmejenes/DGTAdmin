using System;

namespace DGT.Web.Models
{
    public class InfraccionModel
    {
        public string Url { get; set; }

        public int Id { get; set; }

        public string Descripcion { get; set; }

        public double Puntos { get; set; }

        public DateTime? Fecha { get; set; }
    }
}