using DGT.Data;
using DGT.Data.Entities;
using DGT.Web.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DGT.Web.Controllers
{
    public class ConductoresInfraccionesController : BaseApiController
    {
        //
        // GET: /ConductoresInfracciones/

        public ConductoresInfraccionesController(IDGTRepository dgtRepository) : base(dgtRepository)
        {
        }

        public HttpResponseMessage Post(int conductorId, int infraccionId)
        {
            try
            {
                Conductor conductorOriginal = TheRepository.GetConductorById(conductorId).First();

                if (conductorOriginal == null) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Conductor no encontrado");

                Infraccion infraccionOriginal = TheRepository.GetInfraccionById(infraccionId).FirstOrDefault();

                if (infraccionOriginal == null) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Infraccion no encontrada");

                ConductorInfraccion conductorInfraccion = new ConductorInfraccion
                {
                    Conductor = conductorOriginal,
                    Infraccion = infraccionOriginal,
                    Fecha = DateTime.Now
                };

                if (TheRepository.Insert(conductorInfraccion))
                {
                    if (TheRepository.SaveAll())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, TheModelFactory.Create(conductorInfraccion));
                    }
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No se pudo salvar en la base de datos.");
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Post(string matricula, int infraccionId)
        {
            try
            {
                Conductor conductorOriginal = TheRepository.GetConductorByVehiculo(matricula).First();

                if (conductorOriginal == null) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Conductor no encontrado");

                Infraccion infraccionOriginal = TheRepository.GetInfraccionById(infraccionId).FirstOrDefault();

                if (infraccionOriginal == null) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Infraccion no encontrada");

                ConductorInfraccion conductorInfraccion = new ConductorInfraccion
                {
                    Conductor = conductorOriginal,
                    Infraccion = infraccionOriginal,
                    Fecha = DateTime.Now
                };

                if (TheRepository.Insert(conductorInfraccion))
                {
                    if (TheRepository.SaveAll())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, TheModelFactory.Create(conductorInfraccion));
                    }
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No se pudo salvar en la base de datos.");
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
