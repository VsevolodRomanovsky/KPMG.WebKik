using AutoMapper.Configuration;
using KPMG.WebKik.Models;
using KPMG.WebKik.Web.Automapper;
using KPMG.WebKik.Web.Controllers.Role;

namespace KPMG.WebKik.Web.Controllers.User
{
    public class UserViewModel : IEntity<int>
    {
        public int Id { get; set; }

        public string UserLogin { get; set; }
        public string DisplayName { get; set; }

        public int RoleId { get; set; }
        public RoleViewModel Role { get; set; }

        public bool IsDisabled { get; set; }

        [AutomapperInitialization]
        public static void ConfigureMap(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Models.User, UserViewModel>();
            cfg.CreateMap<UserViewModel, Models.User>()
                .ForMember(x => x.Projects, y => y.Ignore())
                .ForMember(x => x.Role, y => y.Ignore());
        }
    }
}
