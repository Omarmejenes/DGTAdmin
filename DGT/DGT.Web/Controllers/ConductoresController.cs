using DGT.Data;
using DGT.Data.Entities;
using DGT.Web.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace DGT.Web.Controllers
{
    public class ConductoresController : BaseApiController
    {
        public ConductoresController(IDGTRepository dgtRepository) : base(dgtRepository)
        {
        }


        public object Get(int page = 1, int pageSize = 5)
        {
            IQueryable<Conductor> query;

            query = TheRepository.GetAllConductores().OrderBy(c => c.DNI);
            int total = query.Count();
            return CreateResponse(query, page, pageSize);
        }

        public object Get(string dni, int page = 1, int pageSize = 5)
        {
            IQueryable<Conductor> query;

            query = TheRepository.GetConductorByDNI(dni).OrderBy(c => c.DNI);

            return CreateResponse(query, page, pageSize);
        }

        public HttpResponseMessage Post([FromBody]ConductorModel conductor)
        {
            try
            {
                Conductor conductorActual = TheModelFactory.Parse(conductor);
                if (conductorActual == null) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Conductor no encontrado");

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

                var conductorActual = TheModelFactory.Parse(conductorModel);

                var conductorOriginal = TheRepository.GetConductorById(id).First();

                if (conductorOriginal == null)
                {
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No se encontro conductor");
                }

                if (TheRepository.Update(conductorOriginal, conductorActual) && TheRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, TheModelFactory.Create(conductorOriginal));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotModified, "No se pudo salvar en la base de datos.");
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
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No se pudo salvar en la base de datos.");
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        private object CreateResponse(IQueryable<Conductor> query, int page, int pageSize)
        {
            page = page- 1;
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var urlHelper = new UrlHelper(Request);
            var prevLink = page > 0 ? urlHelper.Link("conductores", new { page = page - 1, pageSize = pageSize }) : "";
            var nextLink = page < totalPages - 1 ? urlHelper.Link("conductores", new { page = page + 1, pageSize = pageSize }) : "";

            var results = query.Skip(pageSize * page)
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
    }
}
