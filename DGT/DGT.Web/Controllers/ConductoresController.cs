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
    public class ConductoresController : BaseApiController
    {
        public ConductoresController(IDGTRepository dgtRepository) : base(dgtRepository)
        {
        }


        public object Get(int page = 1, int pageSize = 5, string sortBy = "", string direction = "")
        {
            IQueryable<Conductor> query;

            query = TheRepository.GetAllConductores().OrderBy(c => c.Apellidos);

            return CreateResponse(query, page, pageSize, "Conductores");
        }

        public object Get(string dni, int page = 1, int pageSize = 5, string sortBy = "", string direction = "")
        {
            IQueryable<Conductor> query;

            query = TheRepository.GetConductorByDNI(dni).OrderBy(c => c.Apellidos);

            return CreateResponse(query, page, pageSize, "Conductores");
        }

        public HttpResponseMessage Post([FromBody]ConductorModel conductor)
        {
            try
            {
                Conductor conductorActual = TheModelFactory.Parse(conductor);
                if (conductorActual == null) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Conductor no encontrado");

                Conductor conductorExist = TheRepository.GetConductorByDNI(conductorActual.DNI).FirstOrDefault();
                if (conductorExist != null) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Ya existe un Conductor con ese DNI");

                if (TheRepository.Insert(conductorActual))
                {
                    if (TheRepository.SaveAll())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, TheModelFactory.Create(conductorActual));
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

        [HttpPatch]
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] ConductorModel conductorModel)
        {
            try
            {

                Conductor conductorActual = TheModelFactory.Parse(conductorModel);

                Conductor conductorOriginal = TheRepository.GetConductorById(id).First();

                Conductor conductorExist = TheRepository.GetConductorByDNI(conductorActual.DNI).FirstOrDefault();

                if (conductorOriginal == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No se encontro conductor");
                }

                if (conductorExist != null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Ya existe un Conductor con ese DNI");
                }

                if (TheRepository.Update(conductorOriginal, conductorActual) && TheRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, TheModelFactory.Create(conductorOriginal));
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotModified, "No se pudo salvar en la base de datos.");
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
                if (TheRepository.DeleteConductor(id) && TheRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No se pudo salvar en la base de datos.");
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
