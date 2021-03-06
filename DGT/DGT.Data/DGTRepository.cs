﻿using System;
using System.Linq;
using DGT.Data.Entities;

namespace DGT.Data
{
    public class DGTRepository : IDGTRepository
    {
        private DGTContext _dgtContext;

        public DGTRepository(DGTContext dgtContext)
        {
            _dgtContext = dgtContext;
        }

        public IQueryable<Conductor> GetAllConductores()
        {
            return _dgtContext.Conductores.AsQueryable();
        }

        public IQueryable<Vehiculo> GetVehiculosByDNI(string dni)
        {
            return _dgtContext.Vehiculos.Include("Conductor").Where(c => c.Conductor.DNI.Equals(dni)).AsQueryable();
        }

        public IQueryable<Conductor> GetConductorByVehiculo(string matricula)
        {
            return _dgtContext.Vehiculos.Include("Conductor").Where(v => v.Matricula.Equals(matricula)).Select(v=> v.Conductor);
        }

        public IQueryable<Infraccion> GetAllInfracciones()
        {
            return _dgtContext.Infracciones.AsQueryable();
        }

        public IQueryable<Infraccion> GetAllInfraccionesByConductor(string dni)
        {
            return _dgtContext.ConductoresInfracciones.Include("Conductor").Include("Infraccion").Where(c=> c.Conductor.DNI.Equals(dni)).Select(ci=> ci.Infraccion).AsQueryable();
        }

        public IQueryable<Conductor> GetAllConductoresByInfraccion(int id)
        {
            return _dgtContext.ConductoresInfracciones.Include("Conductor").Include("Infraccion").Where(ci => ci.Infraccion.Id.Equals(id)).Select(ci=> ci.Conductor).AsQueryable();
        }

        public IQueryable<Vehiculo> GetAllVehiculos()
        {
            return _dgtContext.Vehiculos.Include("Conductor").AsQueryable();
        }

        public Vehiculo GetVehiculoByMatricula(string matricula)
        {
            return _dgtContext.Vehiculos.FirstOrDefault(c => c.Matricula.Equals(matricula));
        }

        public IQueryable<Conductor> GetConductorByDNI(string dni)
        {
            return _dgtContext.Conductores.Where(c => c.DNI.Equals(dni));
        }

        public IQueryable<Conductor> GetConductorById(int id)
        {
            return _dgtContext.Conductores.Where(c => c.Id.Equals(id));
        }

        public bool Insert(Conductor conductor)
        {
            try
            {
                _dgtContext.Conductores.Add(conductor);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Conductor conductorOriginal, Conductor conductorActual)
        {
            _dgtContext.Entry(conductorOriginal).CurrentValues.SetValues(conductorActual);
            return true;
        }

        public bool DeleteConductor(int id)
        {
            try
            {
                Conductor conductor = _dgtContext.Conductores.Find(id);
                if (conductor != null)
                {
                    _dgtContext.Conductores.Remove(conductor);
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        public IQueryable<Vehiculo> GetVehiculoById(int id)
        {
            return _dgtContext.Vehiculos.Include("Conductor").Where(v => v.Id.Equals(id));
        }

        public bool Insert(Vehiculo vehiculo)
        {
            try
            {
                _dgtContext.Vehiculos.Add(vehiculo);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Vehiculo vehiculoOriginal, Vehiculo vehiculoActual)
        {
            _dgtContext.Entry(vehiculoOriginal).CurrentValues.SetValues(vehiculoActual);
            return true;
        }

        public bool DeleteVehiculo(int id)
        {
            try
            {
                Vehiculo vehiculo = _dgtContext.Vehiculos.Find(id);
                if (vehiculo != null)
                {
                    _dgtContext.Vehiculos.Remove(vehiculo);
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        public IQueryable<Infraccion> GetInfraccionById(int id)
        {
            return _dgtContext.Infracciones.Where(i => i.Id.Equals(id));
        }

        public bool Insert(Infraccion infraccion)
        {
            try
            {
                _dgtContext.Infracciones.Add(infraccion);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Infraccion infraccionOriginal, Infraccion infraccionActual)
        {
            _dgtContext.Entry(infraccionOriginal).CurrentValues.SetValues(infraccionActual);
            return true;
        }

        public bool DeleteInfraccion(int id)
        {
            try
            {
                Infraccion infraccion = _dgtContext.Infracciones.Find(id);
                if (infraccion != null)
                {
                    _dgtContext.Infracciones.Remove(infraccion);
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        public bool Insert(ConductorInfraccion conductorInfraccion)
        {
            try
            {
                _dgtContext.Entry(conductorInfraccion);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteConductorInfraccion(int id)
        {
            try
            {
                ConductorInfraccion infraccion = _dgtContext.ConductoresInfracciones.Include("Conductor").Include("Infraccion").FirstOrDefault(i=> i.Id.Equals(id));
                if (infraccion != null)
                {
                    _dgtContext.ConductoresInfracciones.Remove(infraccion);
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        public bool SaveAll()
        {
            return _dgtContext.SaveChanges() > 0;
        }
    }
}
