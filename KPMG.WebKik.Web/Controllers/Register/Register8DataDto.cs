using AutoMapper.Configuration;
using KPMG.WebKik.Models;
using KPMG.WebKik.Models.Registers;
using KPMG.WebKik.Web.Automapper;
using KPMG.WebKik.Web.Controllers.ProjectCompanies;
using KPMG.WebKik.Web.Controllers.ProjectCompanyShare;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KPMG.WebKik.Web.Controllers.Register
{
	[DataContract(IsReference = true)]
	[JsonObject(IsReference = false)]
	public class Register8DataDto
	{ 

		public int Id { get; set; }

		public string ReserveTitle { get; set; }

		public Register8Dto Register8 { get; set; }

		public int Register8DataTypeId { get; set; }

		public int Register8Id { get; set; }

		public double? ExpensesFormationOfReserve { get; set; }

		public double? ExpensesReducedOfReserve { get; set; }

		public double? IncomeFromRecoveryOfReserve { get; set; }


		public double? ExpensesNotConsideredInProfit { get; set; }

		[AutomapperInitialization]
		public static void ConfigureMap(MapperConfigurationExpression cfg)
		{
			cfg.CreateMap<Models.Registers.Register8Data, Register8DataDto>()
				.ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
				.ForMember(d => d.Register8DataTypeId, o => o.MapFrom(s => s.Register8DataTypeId))
				.ForMember(d => d.ExpensesFormationOfReserve, o => o.MapFrom(s => s.ExpensesFormationOfReserve))
				.ForMember(d => d.ExpensesReducedOfReserve, o => o.MapFrom(s => s.ExpensesReducedOfReserve))
				.ForMember(d => d.IncomeFromRecoveryOfReserve, o => o.MapFrom(s => s.IncomeFromRecoveryOfReserve))
				.ForAllOtherMembers(x => x.Ignore());

			cfg.CreateMap<Register8DataDto, Models.Registers.Register8Data>()
				.ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
				.ForMember(d => d.Register8DataTypeId, o => o.MapFrom(s => s.Register8DataTypeId))
				.ForMember(d => d.ExpensesFormationOfReserve, o => o.MapFrom(s => s.ExpensesFormationOfReserve))
				.ForMember(d => d.ExpensesReducedOfReserve, o => o.MapFrom(s => s.ExpensesReducedOfReserve))
				.ForMember(d => d.IncomeFromRecoveryOfReserve, o => o.MapFrom(s => s.IncomeFromRecoveryOfReserve))
				.ForAllOtherMembers(x => x.Ignore());
		}

	}
}