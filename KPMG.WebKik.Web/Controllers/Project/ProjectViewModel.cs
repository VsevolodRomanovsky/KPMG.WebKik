using System;
using AutoMapper.Configuration;
using KPMG.WebKik.Models;
using KPMG.WebKik.Web.Automapper;

namespace KPMG.WebKik.Web.Controllers.Project
{
    public class ProjectViewModel : IEntity<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset? CreationDate { get; set; }

        [AutomapperInitialization]
        public static void ConfigureMap(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Models.Project, ProjectViewModel>();
            cfg.CreateMap<ProjectViewModel, Models.Project>()
                .ForMember(dest => dest.Users, opt => opt.Ignore())
                .ForMember(x => x.ProjectCompanies, y => y.Ignore());
        }
    }
}
