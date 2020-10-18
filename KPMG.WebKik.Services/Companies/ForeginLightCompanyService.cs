using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPMG.WebKik.Contracts.Service.Companies;
using KPMG.WebKik.DocumentProcessing;
using KPMG.WebKik.Models.Companies;
using KPMG.WebKik.Data;
using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.Services.Companies
{
    public class ForeginLightCompanyService : BaseService, IForeginLightCompanyService
    {
        public Task<ForeignLightCompany> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ForeignLightCompany> Create(ForeignLightCompany entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(ForeignLightCompany entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ForeignLightCompany>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
