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
                Modelo = vehiculo.Modelo
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
                Puntos = conductor.Puntos
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
                    DNI = conductor.DNI,
                    Puntos = conductor.Puntos
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}