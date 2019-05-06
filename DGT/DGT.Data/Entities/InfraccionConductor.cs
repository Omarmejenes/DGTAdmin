
namespace DGT.Data.Entities
{
    public class InfraccionConductor
    {
        public InfraccionConductor()
        {
            Conductor = new Conductor();
            Infraccion = new Infraccion();
        }
        public int Id { get; set; }

        public Conductor Conductor { get; set; }

        public Infraccion Infraccion { get; set; }
    }
}
