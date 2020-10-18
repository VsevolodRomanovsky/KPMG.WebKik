using AutoMapper.Configuration;
using KPMG.WebKik.Models;
using KPMG.WebKik.Web.Automapper;

namespace KPMG.WebKik.Web.Controllers.User
{
    public class SignatoryViewModel : IEntity<int>
    {
        public int Id { get; set; }

        public int ProjectCompanyId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string SignatoryName { get; set; }
        public int SignatoryCodeId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int ConfirmationDocumentId { get; set; }
        public string Inn { get; set; }

        [AutomapperInitialization]
        public static void ConfigureMap(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Signatory, SignatoryViewModel>()
                .ForMember(d => d.SignatoryName, o => o.ResolveUsing(s => s.SignatoryCode?.Name))
                ;

            cfg.CreateMap<SignatoryViewModel, Signatory>()
                .ForMember(x => x.ProjectCompany, y => y.Ignore())
                .ForMember(x => x.SignatoryCode, y => y.Ignore())
                .ForMember(x => x.ConfirmationDocument, y => y.Ignore())
                ;
        }
    }
}
