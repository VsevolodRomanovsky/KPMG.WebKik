using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPMG.WebKik.Contracts.Service.Companies;
using KPMG.WebKik.Data;
using KPMG.WebKik.DocumentProcessing;
using KPMG.WebKik.Models;
using KPMG.WebKik.Models.Companies;
using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.Services.Companies
{
    public class IndividualCompanyService:BaseService, IIndividualCompanyService
    {
        public Task<IndividualCompany> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IndividualCompany> Create(IndividualCompany entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(IndividualCompany entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<IndividualCompany>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
