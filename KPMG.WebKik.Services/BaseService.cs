using KPMG.WebKik.Contracts.Service;
using System.Security.Principal;

namespace KPMG.WebKik.Services
{
    public class BaseService : IBaseService
    {
        public IIdentity Identity => (System.Threading.Thread.CurrentPrincipal as IPrincipal).Identity;

        public virtual void Dispose()
        {
        }
    }
}
