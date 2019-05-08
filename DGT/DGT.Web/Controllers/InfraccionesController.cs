using DGT.Data;
using DGT.Data.Entities;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace DGT.Web.Controllers
{
    public class InfraccionesController : BaseApiController
    {

        public InfraccionesController(IDGTRepository dgtRepository) : base(dgtRepository)
        {
        }

        public object Get(int page = 1, int pageSize = 5)
        {
            IQueryable<Infraccion> query;

            query = TheRepository.GetAllInfracciones().OrderBy(c => c.Puntos);

            return CreateResponse(query, page, pageSize, "Infracciones");

        }

        public HttpResponseMessage Post([FromBody] Infraccion infraccion)
        {
            try
            {
                if (TheRepository.Insert(infraccion) && TheRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, TheModelFactory.Create(infraccion));
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
        public HttpResponseMessage Put(int id, [FromBody] Infraccion infraccion)
        {
            try
            {

                var infraccionOriginal = TheRepository.GetInfraccionById(id).FirstOrDefault();

                if (infraccionOriginal == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotModified, "No se encuentra la infracción");
                }

                infraccion.Id = infraccionOriginal.Id;

                if (TheRepository.Update(infraccionOriginal, infraccion) && TheRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, TheModelFactory.Create(infraccion));
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
                var infraccion = TheRepository.GetInfraccionById(id);

                if (infraccion == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                var conductores = TheRepository.GetAllConductoresByInfraccion(id);
                if (conductores.Count() > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No se puede eliminar infracción porque hay conductores con esta infracción.");
                }

                if (TheRepository.DeleteInfraccion(id) && TheRepository.SaveAll())
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
