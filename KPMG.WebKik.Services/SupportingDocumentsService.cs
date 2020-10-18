using KPMG.WebKik.Contracts.Service.Registers;
using System;
using KPMG.WebKik.Contracts.Repository;
using KPMG.WebKik.Models.Registers;
using KPMG.WebKik.Data;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using KPMG.WebKik.Models;
using KPMG.WebKik.Contracts.Service;
using KPMG.WebKik.Models.ProjectCompanies;
using KPMG.WebKik.Contracts.Algorithms;
using KPMG.WebKik.DocumentProcessing.NotificationOfParticipation;
using KPMG.WebKik.DocumentProcessing.NotificationOfParticipation.Models;
using System.Threading.Tasks;

namespace KPMG.WebKik.Services
{
    public class SupportingDocumentsService : BaseService, ISupportingDocumentsService
    {
        private ISupportingDocumentsService service;
        WebKikDataContext dbContext;
        //private IProjectCompanyService projectCompanyService;
        //private IProjectCompanyShareService shareService;

        public SupportingDocumentsService()
        {
            //this.projectCompanyService = projectCompanyService;
            //this.shareService = shareService;
            //    dbContext = context;
        }

        public SupportingDocument CreateDocument(int year, int companyType, int companyId, bool isUU, bool isUKIK, bool isND, string uKIKDocType, byte[] fileData, string fileName)
        {
            SupportingDocument doc = new SupportingDocument
            {
                Year = year,
                CompanyType = companyType,
                ProjectCompanyId = companyId,
                IsUU = isUU,
                IsUKIK = isUKIK,
                IsND = isND,
                Data = fileData,
                FileName = fileName,
                UKIKDocType = !string.IsNullOrEmpty(uKIKDocType) ? getUkikDocTypeId(uKIKDocType) : (int?)null
            };
            using (var context = new WebKikDataContext())
            {
                var document = context.SupportingDocuments.Add(doc);
                context.SaveChanges();
                return document;
            }
            // return null;
        }

        private int getUkikDocTypeId(string uKIKDocType)
        {
            if (uKIKDocType.ToLower().Equals("пд для освобождения кик")) return 0;
            return 1;
        }


        public SupportingDocument DownloadDocument()
        {
            throw new NotImplementedException();
        }

        public ICollection<SupportingDocument> GetSupportingDocumentsByCompanyId(int id)
        {
            using (var context = new WebKikDataContext())
            {
                var documents = context.SupportingDocuments.Where(sup => sup.ProjectCompanyId == id).ToList();
                return documents;
            }
        }

        public ICollection<SupportingDocument> GetAllSupportingDocumentsByCompanyId(int id, int type)
        {
            using (var context = new WebKikDataContext())
            {
                var company = context.ProjectCompanies.Where(cmp => cmp.Id == id).FirstOrDefault();

                var checkdate = DateTime.Today;

                var q = context.ProjectCompanyShares.Where(x => x.OwnerProjectCompanyId == company.ProjectId && (x.ShareStartDate <= checkdate && (x.ShareFinishDate >= checkdate || x.ShareFinishDate == null)));
                var allCompany = q.Include(x => x.OwnerProjectCompany).Include(c => c.DependentProjectCompany).ToList();

                var ids = allCompany.Select(c => c.Id).ToList();
                if (!ids.Contains(id))
                {
                    ids.Add(id);
                }
                var documents = context.SupportingDocuments.Where(sup => ids.Contains(sup.ProjectCompanyId) && (type == 0 ? sup.IsUU : (type == 1 ? sup.IsND : sup.IsUKIK)));

                return documents.ToList();
            }
        }

        public SupportingDocument GetById(int id)
        {
            using (var context = new WebKikDataContext())
            {
                return context.SupportingDocuments.Where(sup => sup.Id == id).FirstOrDefault();
            }
        }
    }
}