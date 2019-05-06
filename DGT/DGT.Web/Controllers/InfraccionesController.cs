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
            page = page - 1;
            query = TheRepository.GetAllInfracciones().OrderBy(c => c.Puntos);
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var urlHelper = new UrlHelper(Request);
            var prevLink = page > 0 ? urlHelper.Link("Infracciones", new { page = page - 1, pageSize = pageSize }) : "";
            var nextLink = page < totalPages - 1 ? urlHelper.Link("Infracciones", new { page = page + 1, pageSize = pageSize }) : "";

            var results = query
                          .Skip(pageSize * page)
                          .Take(pageSize)
                          .ToList()
                          .Select(s => TheModelFactory.Create(s));

            return new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PrevPageLink = prevLink,
                NextPageLink = nextLink,
                Results = results
            };

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
        public HttpResponseMessage Put(string dni, [FromBody] Infraccion infraccion)
        {
            try
            {

                var infraccionOriginal = TheRepository.GetAllInfraccionesByConductor(dni);

                if (infraccionOriginal == null || infraccionOriginal.Count() < 1)
                {
                    return Request.CreateResponse(HttpStatusCode.NotModified, "No se encuentra");
                }
                //infraccion.Id = infraccionOriginal.Id;

                //if (TheRepository.Update(infraccionOriginal, infraccion) && TheRepository.SaveAll())
                //{
                //    return Request.CreateResponse(HttpStatusCode.OK, TheModelFactory.Create(infraccion));
                //}
                //else
                //{
                //    return Request.CreateResponse(HttpStatusCode.NotModified);
                //}
                return Request.CreateResponse(HttpStatusCode.NotModified);
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
                var infraccion = TheRepository.GetAllInfraccionesByConductor(id.ToString());

                if (infraccion == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                //if (infraccion.Count > 0)
                //{
                //    return Request.CreateResponse(HttpStatusCode.BadRequest, "No se puede eliminar infracción.");
                //}

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
