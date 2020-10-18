using AutoMapper.Configuration;
using KPMG.WebKik.Models;
using KPMG.WebKik.Web.Automapper;

namespace KPMG.WebKik.Web.Controllers.Role
{
    public class RoleViewModel: IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [AutomapperInitialization]
        public static void ConfigureMap(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Models.Role, RoleViewModel>();
            cfg.CreateMap<RoleViewModel, Models.Role>()
                .ForMember(x => x.Users, y => y.Ignore());
        }
    }
}
