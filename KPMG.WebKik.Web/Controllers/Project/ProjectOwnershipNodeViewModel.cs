using AutoMapper.Configuration;
using KPMG.WebKik.Web.Automapper;

namespace KPMG.WebKik.Web.Controllers.Project
{
    public class ProjectOwnershipNodeViewModel
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }

        [AutomapperInitialization]
        public static void ConfigureMap(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Models.ProjectCompanies.ProjectCompany, ProjectOwnershipNodeViewModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.Name))
                .ForAllOtherMembers(x => x.Ignore());

            cfg.CreateMap<ProjectOwnershipNodeViewModel, Models.ProjectCompanies.ProjectCompany>()
                .ForAllMembers(o => o.Ignore());
        }
    }
}