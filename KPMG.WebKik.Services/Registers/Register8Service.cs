using KPMG.WebKik.Contracts.Service.Registers;
using System;
using KPMG.WebKik.Contracts.Repository;
using KPMG.WebKik.Models.Registers;
using KPMG.WebKik.Data;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace KPMG.WebKik.Services.Registers
{
	public class Register8Service : BaseService, IRegister8Service
	{
		WebKikDataContext context;
		static readonly int RESERVE_DATATYPE_SUMMARY = 11;
		public Register8Service()
		{
			context = new WebKikDataContext();
		}

		public Register8 GetRegister8(int id)
		{
			Register8 register;
			using (var context = new WebKikDataContext())
			{
				register = context.Registers8.Where(rg8 => (rg8.Id == id)).Include(x => x.Register8Data).FirstOrDefault();
				return register;
			}
		}

		public Register8 Create(Register8 model)
		{
			Register8 register;
			using (var context = new WebKikDataContext())
			{
				register = context.Registers8.Add(new Register8
				{
					Year = model.Year,
					OwnerProjectCompanyId = model.OwnerProjectCompanyId,
					Currency = model.Currency,
					Type = RegisterType.Register8
				});
				int dataTypeId = 0;
				foreach (var reserve in model.Register8Data)
				{
					if (dataTypeId != RESERVE_DATATYPE_SUMMARY)
					{
						context.Registers8Data.Add(new Register8Data
						{
							Register8DataTypeId = dataTypeId,
							Register8Id = register.Id,
							ExpensesFormationOfReserve = reserve.ExpensesFormationOfReserve,
							ExpensesReducedOfReserve = reserve.ExpensesReducedOfReserve,
							IncomeFromRecoveryOfReserve = reserve.IncomeFromRecoveryOfReserve
						});
						dataTypeId++;
					}
				}
				context.SaveChanges();
				return register;
			}
		}

		public Register8 Edit(Register8 model)
		{
			Register8 register;
			using (var context = new WebKikDataContext())
			{
				register = context.Registers8.Where(r => r.Id == model.Id).FirstOrDefault();
				if (register != null)
				{
					register.Currency = model.Currency;
				}
				int dataTypeId = 0;
				foreach (var reserve in model.Register8Data)
				{
					if (dataTypeId != RESERVE_DATATYPE_SUMMARY)
					{
						var register8Data = context.Registers8Data.Where(data => data.Register8Id == model.Id && data.Register8DataTypeId == reserve.Register8DataTypeId).FirstOrDefault();
						if (register8Data != null)
						{
							register8Data.ExpensesFormationOfReserve = reserve.ExpensesFormationOfReserve;
							register8Data.ExpensesReducedOfReserve = reserve.ExpensesReducedOfReserve;
							register8Data.IncomeFromRecoveryOfReserve = reserve.IncomeFromRecoveryOfReserve;
							dataTypeId++;
						}
					}
				}
				context.SaveChanges();
				return register;
			}
		}


		public Register8 CalculateRegisterFields(Register8 register)
		{
			return CalculateRegister7Fileds(register);
		}

		private Register8 CalculateRegister7Fileds(Register8 entity)
		{
			return entity;
		}
	}
}
