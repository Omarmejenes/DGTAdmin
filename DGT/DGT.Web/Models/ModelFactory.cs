using DGT.Data;
using System;
using System.Net.Http;
using System.Web.Http.Routing;
using DGT.Data.Entities;
using DGT.Web.Models;

namespace DGT.Web.Models
{
    public class ModelFactory
    {
        private UrlHelper _UrlHelper;
        private IDGTRepository _dgtRepository;
        public ModelFactory(HttpRequestMessage request, IDGTRepository dgtRepository)
        {
            _UrlHelper = new UrlHelper(request);
            _dgtRepository = dgtRepository;
        }

        public InfraccionModel Create(Infraccion infraccion)
        {
            return new InfraccionModel()
            {
                Url = _UrlHelper.Link("Infracciones", new { id = infraccion.Id }),
                Id = infraccion.Id,
                Descripcion = infraccion.Descripcion,
                Puntos = infraccion.Puntos
            };
        }

        public VehiculoModel Create(Vehiculo vehiculo)
        {
            return new VehiculoModel()
            {
                Url = _UrlHelper.Link("vehiculos", new { id = vehiculo.Id }),
                Id = vehiculo.Id,
                Matricula = vehiculo.Matricula,
                Marca = vehiculo.Marca,
                Modelo = vehiculo.Modelo,
                DNI = vehiculo.Conductor.DNI
            };
        }

        public ConductorModel Create(Conductor conductor)
        {
            return new ConductorModel()
            {
                Id = conductor.Id,
                DNI = conductor.DNI,
                Nombres = conductor.Nombres,
                Apellidos = conductor.Apellidos,
                Puntos = 0
            };
        }

        public InfraccionModel Create(ConductorInfraccion infraccion)
        {
            return new InfraccionModel()
            {
                Url = _UrlHelper.Link("Infracciones", new { id = infraccion.Infraccion.Id }),
                Id = infraccion.Infraccion.Id,
                Descripcion = infraccion.Infraccion.Descripcion,
                Puntos = infraccion.Infraccion.Puntos,
                Fecha = infraccion.Fecha
            };
        }

        public Conductor Parse(ConductorModel conductor)
        {
            try
            {
                return new Conductor()
                {
                    Id = conductor.Id,
                    Nombres = conductor.Nombres,
                    Apellidos = conductor.Apellidos,
                    DNI = conductor.DNI.ToUpper()
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Vehiculo Parse(VehiculoModel vehiculo)
        {
            try
            {
                return new Vehiculo()
                {
                    Id = vehiculo.Id,
                    Matricula = vehiculo.Matricula.ToUpper(),
                    Marca = vehiculo.Marca,
                    Modelo = vehiculo.Modelo
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}