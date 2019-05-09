using DGT.Data.Entities;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using DGT.Data;
using DGT.Web.Models;

namespace DGT.Web.Controllers
{
    public class VehiculosController : BaseApiController
    {
        public VehiculosController(IDGTRepository dgtRepository) : base(dgtRepository)
        {
        }

        public object Get(int page = 1, int pageSize = 5)
        {
            IQueryable<Vehiculo> query;

            query = TheRepository.GetAllVehiculos().OrderBy(c => c.Conductor.DNI);

            return CreateResponse(query, page, pageSize, "Vehiculos");
        }

        public HttpResponseMessage Post([FromBody] VehiculoModel vehiculo)
        {
            try
            {
                Vehiculo vehiculoActual = TheModelFactory.Parse(vehiculo);

                Vehiculo vehiculoExist = TheRepository.GetVehiculoByMatricula(vehiculo.Matricula);

                Conductor conductor = TheRepository.GetConductorByDNI(vehiculo.DNI).FirstOrDefault();

                if (conductor == null || vehiculoExist != null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "El vehiculo ya esta resgistrado o no está registrado el DNI como conductor.");
                }
                else
                {
                    vehiculoActual.Conductor = conductor;
                }

                IQueryable<Vehiculo> vehiculos = TheRepository.GetVehiculosByDNI(conductor.DNI);

                if (vehiculos.Count() > 10)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "El conductor ya esta resgistrado con el maximo de vehiculos.");
                }


                if (TheRepository.Insert(vehiculoActual) && TheRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, TheModelFactory.Create(vehiculoActual));
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No se pudo salvar en la base de datos.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpPatch]
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] VehiculoModel vehiculo)
        {
            try
            {
                Vehiculo vehiculoActual = TheModelFactory.Parse(vehiculo);

                Vehiculo vehiculoOriginal = TheRepository.GetVehiculoById(id).FirstOrDefault();

                Conductor conductorExist = TheRepository.GetConductorByDNI(vehiculo.DNI).FirstOrDefault();

                if (vehiculoOriginal == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotModified, "No se encuentra el vehiculo regstrado");
                }
                if (conductorExist == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No está registrado el DNI como conductor.");
                }
                else
                {
                    vehiculoActual.Conductor = conductorExist;
                }
                vehiculoActual.Id = vehiculoOriginal.Id;

                IQueryable<Vehiculo> vehiculos = TheRepository.GetVehiculosByDNI(conductorExist.DNI);

                if (vehiculos.Count() > 10)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "El conductor ya esta resgistrado con el maximo de vehiculos.");
                }

                if (TheRepository.Update(vehiculoOriginal, vehiculoActual) && TheRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, TheModelFactory.Create(vehiculoActual));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotModified);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Vehiculo vehiculo = TheRepository.GetVehiculoById(id).FirstOrDefault();

                if (vehiculo == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                if (TheRepository.DeleteVehiculo(vehiculo.Id) && TheRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}
