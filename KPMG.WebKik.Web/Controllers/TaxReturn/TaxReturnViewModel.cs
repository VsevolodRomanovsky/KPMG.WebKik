using KPMG.WebKik.Models;
using System;
using KPMG.WebKik.Web.Automapper;
using AutoMapper.Configuration;
using KPMG.WebKik.Web.Controllers.ProjectCompanies;

namespace KPMG.WebKik.Web.Controllers.TaxReturns
{
    public class TaxReturnViewModel : IEntity<int>
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public int ProjectCompanyId { get; set; }
        public int Year { get; set; }
        public int SignatoryId { get; set; }
        public Signatory Signatory { get; set; }
        public int Correction { get; set; }

        public ProjectCompanyViewModel ProjectCompany { get; set; }
        public byte[] File { get; set; }

        [AutomapperInitialization]
        public static void ConfigureMap(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<TaxReturn, TaxReturnViewModel>()
                .ForMember(x => x.File, o => o.Ignore())
                ;

            cfg.CreateMap<TaxReturnViewModel, TaxReturn>()
                .ForMember(x => x.ProjectCompany, o => o.Ignore())
                .ForMember(x => x.Signatory, o => o.Ignore())
                .ForMember(x => x.Correction, o => o.Ignore())
                ;
        }
    }
}