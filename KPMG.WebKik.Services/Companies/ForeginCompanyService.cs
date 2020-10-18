using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPMG.WebKik.Contracts.Service.Companies;
using KPMG.WebKik.Data;
using KPMG.WebKik.DocumentProcessing;
using KPMG.WebKik.Models.Companies;
using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.Services.Companies
{
    public class ForeginCompanyService : BaseService, IForeginCompanyService
    {
        public Task<ForeignCompany> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ForeignCompany> Create(ForeignCompany entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(ForeignCompany  entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ForeignCompany>> GetAll()
        {
            throw new NotImplementedException();
        }

        public void ImportFromExcel(byte[] file)
        {
            throw new NotImplementedException();
        }
    }
}
