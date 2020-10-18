using System;
using System.Security.Principal;

namespace KPMG.WebKik.Contracts.Service
{
    public interface IBaseService : IDisposable
    {
        IIdentity Identity { get; }
    }
}
