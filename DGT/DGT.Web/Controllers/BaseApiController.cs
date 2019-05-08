using DGT.Web.Models;
using System.Web.Http;
using DGT.Data;
using System.Linq;
using System.Web.Http.Routing;
using DGT.Data.Entities;

namespace DGT.Web.Controllers
{
    public class BaseApiController : ApiController
    {
        private IDGTRepository _dgtRepository;
        private ModelFactory _modelFactory;

        public BaseApiController(IDGTRepository dgtRepository)
        {
            _dgtRepository = dgtRepository;
        }

        protected ModelFactory TheModelFactory
        {
            get
            {
                if (_modelFactory == null)
                {
                    _modelFactory = new ModelFactory(Request, TheRepository);
                }
                return _modelFactory;
            }
        }

        protected IDGTRepository TheRepository
        {
            get
            {
                return _dgtRepository;
            }
        }

        protected object CreateResponse(IQueryable<Conductor> query, int page, int pageSize, string controller)
        {
            page = page - 1;
            var totalCount = query.Count();
            var totalPages = (int)System.Math.Ceiling((double)totalCount / pageSize);

            var urlHelper = new UrlHelper(Request);
            var prevLink = page > 0 ? urlHelper.Link(controller, new { page = page - 1, pageSize = pageSize }) : "";
            var nextLink = page < totalPages - 1 ? urlHelper.Link(controller, new { page = page + 1, pageSize = pageSize }) : "";

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

        protected object CreateResponse(IQueryable<Vehiculo> query, int page, int pageSize, string controller)
        {
            page = page - 1;
            var totalCount = query.Count();
            var totalPages = (int)System.Math.Ceiling((double)totalCount / pageSize);

            var urlHelper = new UrlHelper(Request);
            var prevLink = page > 0 ? urlHelper.Link(controller, new { page = page - 1, pageSize = pageSize }) : "";
            var nextLink = page < totalPages - 1 ? urlHelper.Link(controller, new { page = page + 1, pageSize = pageSize }) : "";

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

        protected object CreateResponse(IQueryable<Infraccion> query, int page, int pageSize, string controller)
        {
            page = page - 1;
            var totalCount = query.Count();
            var totalPages = (int)System.Math.Ceiling((double)totalCount / pageSize);

            var urlHelper = new UrlHelper(Request);
            var prevLink = page > 0 ? urlHelper.Link(controller, new { page = page - 1, pageSize = pageSize }) : "";
            var nextLink = page < totalPages - 1 ? urlHelper.Link(controller, new { page = page + 1, pageSize = pageSize }) : "";

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
