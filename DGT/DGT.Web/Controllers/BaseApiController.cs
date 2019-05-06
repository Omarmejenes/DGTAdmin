using DGT.Web.Models;
using System.Web.Http;
using DGT.Data;

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
    }
}
