using KPMG.WebKik.Contracts.Service.Registers;
using System;
using KPMG.WebKik.Contracts.Repository;
using KPMG.WebKik.Models.Registers;
using KPMG.WebKik.Data;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using KPMG.WebKik.Models;

namespace KPMG.WebKik.Services.Registers
{
	public class Register9Service : BaseService, IRegister9Service
	{
		WebKikDataContext context;

		public Register9Service()
		{
			context = new WebKikDataContext();
		}

		public Register9 GetRegister9(int id)
		{
			Register9 register;
			using (var context = new WebKikDataContext())
			{
				register = context.Registers9.Where(rg8 => (rg8.Id == id)).Include(x => x.Register9Data).FirstOrDefault();
				return register;
			}
		}

        public Register9 GetRegister9ByCompanyId(int companyId, int year)
        {
            Register9 register;
            using (var context = new WebKikDataContext())
            {
                register = context.Registers9.Where(rg9 => (rg9.OwnerProjectCompanyId == companyId && rg9.Year == year)).Include(x => x.Register9Data).FirstOrDefault();
                return register;
            }
        }

        public Register9Data CreateRegisterData(Register9Data data)
		{
			Register9Data register;
			using (var context = new WebKikDataContext())
			{
				var data9 = context.Registers9Data.Add(new Register9Data
				{
					Register9Id = data.Register9Id,
					StockholderName = data.StockholderName,
					CountryCodeId = data.CountryCodeId,
					LastYearDividendPaymentYear = data.LastYearDividendPaymentYear,
					CurrentYearDividendPaymentData = data.CurrentYearDividendPaymentData.HasValue ? data.CurrentYearDividendPaymentData.Value : new DateTime(0),
					CurrentYearDividendSum = data.CurrentYearDividendSum,
					CurrentYearTransitionalDividendPaymentData = data.CurrentYearTransitionalDividendPaymentData.HasValue ? data.CurrentYearTransitionalDividendPaymentData.Value : new DateTime(0),
					CurrentYearTransitionalDividendSum = data.CurrentYearTransitionalDividendSum,
					LastYearDividendPaymentData = data.LastYearDividendPaymentData.HasValue ? data.LastYearDividendPaymentData.Value : new DateTime(0),
					LastYearDividendSum = data.LastYearDividendSum
				});
				context.SaveChanges();
				return data9;
			}
		}

		public Register9Data EditRegisterData(Register9Data data)
		{
			Register9Data data9 = null;
			using (var context = new WebKikDataContext())
			{
				data9 = context.Registers9Data.Where(d => d.Id == data.Id).FirstOrDefault();
				if (data9 != null)
				{
					data9.StockholderName = data.StockholderName;
					data9.CountryCodeId = data.CountryCodeId;
					data9.LastYearDividendPaymentYear = data.LastYearDividendPaymentYear;
					data9.CurrentYearDividendPaymentData = data.CurrentYearDividendPaymentData;
					data9.CurrentYearDividendSum = data.CurrentYearDividendSum;
					data9.CurrentYearTransitionalDividendPaymentData = data.CurrentYearTransitionalDividendPaymentData;
					data9.CurrentYearTransitionalDividendSum = data.CurrentYearTransitionalDividendSum;
					data9.LastYearDividendPaymentData = data.LastYearDividendPaymentData;
					data9.LastYearDividendSum = data.LastYearDividendSum;
				}
				context.SaveChanges();
			}
			return data9;
		}

		public Register9Data DeleteRegisterData(int id)
		{
			Register9Data data9 = null;
			using (var context = new WebKikDataContext())
			{
				data9 = context.Registers9Data.Where(d => d.Id == id).FirstOrDefault();
				context.Registers9Data.Remove(data9);
				context.SaveChanges();
			}
			return data9;
		}


		public Register9 Create(Register9 model)
		{
			Register9 register;
			using (var context = new WebKikDataContext())
			{
				register = context.Registers9.Add(new Register9
				{
					Year = model.Year,
					OwnerProjectCompanyId = model.OwnerProjectCompanyId,
					Currency = model.Currency,
					Type = RegisterType.Register9
				});
				foreach (var data in model.Register9Data)
				{
					var data9 = context.Registers9Data.Add(new Register9Data
					{
						Register9Id = register.Id,
						StockholderName = data.StockholderName,
						CountryCodeId = data.CountryCodeId,
						LastYearDividendPaymentYear = data.LastYearDividendPaymentYear,
						CurrentYearDividendPaymentData = data.CurrentYearDividendPaymentData,
						CurrentYearDividendSum = data.CurrentYearDividendSum,
						CurrentYearTransitionalDividendPaymentData = data.CurrentYearTransitionalDividendPaymentData,
						CurrentYearTransitionalDividendSum = data.CurrentYearTransitionalDividendSum,
						LastYearDividendPaymentData = data.LastYearDividendPaymentData,
						LastYearDividendSum = data.LastYearDividendSum
					});
				}
				context.SaveChanges();
			}
			return register;
			}
		//}

		//public Register8 Edit(Register8 model)
		//{
		//	Register8 register;
		//	using (var context = new WebKikDataContext())
		//	{
		//		register = context.Registers8.Where(r => r.Id == model.Id).FirstOrDefault();
		//		if (register != null)
		//		{
		//			register.Currency = model.Currency;
		//		}
		//		int dataTypeId = 0;
		//		foreach (var reserve in model.Register8Data)
		//		{
		//			if (dataTypeId != RESERVE_DATATYPE_SUMMARY)
		//			{
		//				var register8Data = context.Registers8Data.Where(data => data.Register8Id == model.Id && data.Register8DataTypeId == reserve.Register8DataTypeId).FirstOrDefault();
		//				if (register8Data != null)
		//				{
		//					register8Data.ExpensesFormationOfReserve = reserve.ExpensesFormationOfReserve;
		//					register8Data.ExpensesReducedOfReserve = reserve.ExpensesReducedOfReserve;
		//					register8Data.IncomeFromRecoveryOfReserve = reserve.IncomeFromRecoveryOfReserve;
		//					dataTypeId++;
		//				}
		//			}
		//		}
		//		context.SaveChanges();
		//		return register;
		//	}
		//}


		public Register9 CalculateRegisterFields(Register9 register)
		{
			return register;
		}
    }
}
