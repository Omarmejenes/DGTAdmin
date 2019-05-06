using DGT.Data.Entities;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using DGT.Data;

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
            page = page - 1;
            query = TheRepository.GetAllVehiculos().OrderBy(c => c.Conductor.DNI);

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var urlHelper = new UrlHelper(Request);
            var prevLink = page > 0 ? urlHelper.Link("vehiculos", new { page = page - 1, pageSize = pageSize }) : "";
            var nextLink = page < totalPages - 1 ? urlHelper.Link("vehiculos", new { page = page + 1, pageSize = pageSize }) : "";

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

        public HttpResponseMessage Post([FromBody] Vehiculo vehiculo)
        {
            try
            {
                if (TheRepository.Insert(vehiculo) && TheRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, TheModelFactory.Create(vehiculo));
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
        public HttpResponseMessage Put(string matricula, [FromBody] Vehiculo vehiculo)
        {
            try
            {

                var vehiculoOriginal = TheRepository.GetVehiculoByMatricula(matricula);

                if (vehiculoOriginal == null || vehiculoOriginal.Matricula != matricula)
                {
                    return Request.CreateResponse(HttpStatusCode.NotModified, "No se encuentra el vehiulo regstrado");
                }
                vehiculo.Matricula = vehiculoOriginal.Matricula;

                if (TheRepository.Update(vehiculoOriginal, vehiculo) && TheRepository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, TheModelFactory.Create(vehiculo));
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

        public HttpResponseMessage Delete(string matricula)
        {
            try
            {
                var vehiculo = TheRepository.GetVehiculoByMatricula(matricula);

                if (vehiculo == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                if (vehiculo.Conductor != null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No se puede eliminar.");
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
