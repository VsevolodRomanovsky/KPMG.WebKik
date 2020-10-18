using KPMG.WebKik.Contracts.Service;
using System.Web.Http;

namespace KPMG.WebKik.Web.Controllers
{
    public abstract class BaseController : ApiController
    {
        protected readonly IBaseService Service;

        protected BaseController(IBaseService service)
        {
            Service = service;
        }
        protected override void Dispose(bool disposing) {
            Service.Dispose();
            base.Dispose(disposing);
        }
    }
}
