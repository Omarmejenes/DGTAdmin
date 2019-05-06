using System;
using System.Linq;
using DGT.Data.Entities;

namespace DGT.Data
{
    public interface IDGTRepository
    {
        IQueryable<Conductor> GetAllConductores();

        IQueryable<Vehiculo> GetVehiculosByDNI(string dni);

        IQueryable<Conductor> GetConductorByVehiculo(string matricula);

        IQueryable<Infraccion> GetAllInfracciones();

        IQueryable<Infraccion> GetAllInfraccionesByConductor(string dni);

        IQueryable<Vehiculo> GetAllVehiculos();

        Vehiculo GetVehiculoByMatricula(string matricula);

        IQueryable<Conductor> GetConductorByDNI(string dni);

        IQueryable<Conductor> GetConductorById(int id);
        bool Insert(Conductor conductor);
        bool Update(Conductor conductorOriginal, Conductor conductorActual);
        bool DeleteConductor(int id);

        IQueryable<Vehiculo> GetVehiculoById(int id);
        bool Insert(Vehiculo vehiculo);
        bool Update(Vehiculo vehiculoOriginal, Vehiculo vehiculoActual);
        bool DeleteVehiculo(int id);

        IQueryable<Infraccion> GetInfraccionById(int id);
        bool Insert(Infraccion infraccion);
        bool Update(Infraccion infraccionOriginal, Infraccion infraccionActual);
        bool DeleteInfraccion(int id);

        bool Insert(int infraccionId, int vehiculoId);
        bool DeleteInfraccionConductor(int id);

        bool SaveAll();
    }
}
