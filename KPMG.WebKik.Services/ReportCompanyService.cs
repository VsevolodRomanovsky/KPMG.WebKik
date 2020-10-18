using System.Collections.Generic;
using System.Threading.Tasks;
using KPMG.WebKik.Contracts.Service;
using KPMG.WebKik.Contracts.Repository;
using KPMG.WebKik.Models;
using KPMG.WebKik.Models.ProjectCompanies;
using KPMG.WebKik.Models.ReportCompanies;
using KPMG.WebKik.Contracts.Algorithms;
using KPMG.WebKik.Contracts.Service.Registers;
using System.Linq;
using QuickGraph;
using QuickGraph.Algorithms;
using System;

namespace KPMG.WebKik.Services
{
    public class ReportCompanyService : IReportCompanyService
    {
        private IEnumerable<ProjectCompanyFactShare> factShares;
        private IList<ProjectCompanyShare> shares;
        private ProjectCompany ownerCompany;
        private CompanyNumberContainer companyNumberContainer;

        public ReportCompanyService(ProjectCompany ownerCompany, IList<ProjectCompanyShare> shares, IEnumerable<ProjectCompanyFactShare> factShares)
        {
            this.ownerCompany = ownerCompany;
            this.shares = shares;
            this.factShares = factShares;

        }

        public IEnumerable<CompanyChain> GetChains()
        {
            companyNumberContainer = new CompanyNumberContainer();

            var reportCompanies = factShares
                .Select(share => new ReportCompany(share, companyNumberContainer))
                .ToArray();

            var companies = reportCompanies.ToList();
            
            var chains = new List<CompanyChain>();

            foreach (var company in companies)
            {
                var chain = GetCompanyChainsWithUpdateReportCompanies(company.ProjectCompany.ProjectId, company.ProjectCompany.Id, reportCompanies, factShares);
                chains.AddRange(chain);
            }
            return chains;
        }

        private IEnumerable<CompanyChain> GetCompanyChainsWithUpdateReportCompanies(int projectId, int targetCompanyId, ICollection<ReportCompany> reportCompanies, IEnumerable<ProjectCompanyFactShare> factShares)
        {
            var chains = new List<CompanyChain>();
            var idirectPaths = GetPaths(projectId, targetCompanyId);
            idirectPaths = idirectPaths.Where(x => x.Count() > 1);
            foreach (var path in idirectPaths)
            {
                var chainCompanies = new List<ReportCompany>();
                foreach (var edge in path)
                {
                    var reportCompany = reportCompanies.SingleOrDefault(x => x.ProjectCompany.Id == edge.Target);
                    //if (reportCompany == null)
                    //{
                    //    var factShare = factShares.First(x => x.DependentProjectCompanyId == edge.Target);//
                    //    reportCompany = new ReportCompany(factShare, companyNumberContainer);
                    //    reportCompanies.Add(reportCompany);
                    //}
                    if (reportCompany != null)
                        chainCompanies.Add(reportCompany);
                }
                chains.Add(new CompanyChain(chains.Count() + 1, chainCompanies));
            }
            return chains;
        }

        private IEnumerable<IEnumerable<Edge<int>>> GetPaths(int projectId, int targetCompanyId)
        {
            const int PathMaxItemsCount = 15;
            var graph = new BidirectionalGraph<int, Edge<int>>();
            foreach (var share in shares)
            {
                graph.AddVerticesAndEdge(new TaggedEdge<int, double>(share.OwnerProjectCompanyId, share.DependentProjectCompanyId, share.SharePart));
            }

            return graph.RankedShortestPathHoffmanPavley(e => 0, ownerCompany.Id, targetCompanyId, PathMaxItemsCount);
        }
    }
}
