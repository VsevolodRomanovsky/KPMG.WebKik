using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    public class DomesticCompanyService : BaseService, IDomesticCompanyService
    {
        public Task<DomesticCompany> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DomesticCompany> Create(DomesticCompany entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(DomesticCompany entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<DomesticCompany>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
