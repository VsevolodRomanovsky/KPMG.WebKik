using AutoMapper.Configuration;
using KPMG.WebKik.Models;
using KPMG.WebKik.Models.Registers;
using KPMG.WebKik.Web.Automapper;
using KPMG.WebKik.Web.Controllers.ProjectCompanies;
using KPMG.WebKik.Web.Controllers.ProjectCompanyShare;
using System;
using System.Collections.Generic;

namespace KPMG.WebKik.Web.Controllers.Register
{
	[Serializable]
    public class Register8ViewModel: IEntity<int>
    {
        public int Id { get; set; }
        public int OwnerProjectCompanyId { get; set; }
        public ProjectCompanyViewModel OwnerProjectCompany { get; set; }
        public RegisterType Type { get; set; }
        public Year Year { get; set; }
        public string Currency { get; set; }

		public object[] Register8Data { get; set; }

		[AutomapperInitialization]
        public static void ConfigureMap(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Register8, Register8ViewModel>();
			cfg.CreateMap<Register8ViewModel, Register8>()
			.ForMember(r => r.OwnerProjectCompany, c => c.Ignore())
			.ForMember(r => r.Register8Data, c => c.Ignore());

		}
    }
}