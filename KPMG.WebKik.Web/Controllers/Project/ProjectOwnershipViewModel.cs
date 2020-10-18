using System.Collections.Generic;
using System.Linq;
using AutoMapper.Configuration;
using KPMG.WebKik.Web.Automapper;

namespace KPMG.WebKik.Web.Controllers.Project
{
    public class ProjectOwnershipViewModel
    {
        public ProjectViewModel Project { get; set; }

        public IEnumerable<ProjectOwnershipNodeViewModel> Nodes { get; set; }

        public IEnumerable<ProjectOwnershipLinkViewModel> Links { get; set; }

        [AutomapperInitialization]
        public static void ConfigureMap(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ProjectOwnershipViewModel, Models.Project>()
                .ForAllMembers(o => o.Ignore());

            cfg.CreateMap<Models.Project, ProjectOwnershipViewModel>()
                .ForMember(d => d.Project, o => o.MapFrom(s => s))
                .ForMember(d => d.Nodes, o => o.MapFrom(s => s.ProjectCompanies))
                .ForMember(d => d.Links, o => o.ResolveUsing(ToLinksResolver));
        }

        private static IEnumerable<ProjectOwnershipLinkViewModel> ToLinksResolver(Models.Project project)
        {
            return null;

            //var links = new List<ProjectOwnershipLinkViewModel>();
            //foreach (var projectCompany in project.ProjectCompanies)
            //{
            //    links.AddRange(
            //        projectCompany.DependentProjectCompanyFactShares
            //        .Where(x => x.ShareFactPart > 0)
            //        .Select(
            //            share => new ProjectOwnershipLinkViewModel()
            //            {
            //                SourceId = share.OwnerProjectCompanyId,
            //                TargetId = share.DependentProjectCompanyId,
            //                Share = share.ShareFactPart
            //            }));
            //}
            //return links;
        }

    }
}
