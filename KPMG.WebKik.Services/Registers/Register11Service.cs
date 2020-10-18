using KPMG.WebKik.Contracts.Service.Registers;
using System;
using KPMG.WebKik.Contracts.Repository;
using KPMG.WebKik.Models.Registers;
using KPMG.WebKik.Data;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using KPMG.WebKik.Models;
using KPMG.WebKik.Contracts.Service;

namespace KPMG.WebKik.Services.Registers
{
	public class Register11Service : BaseService, IRegister11Service
	{
		WebKikDataContext dbContext;

		public Register11Service(WebKikDataContext context)
		{
			dbContext = context;
		}

		public Register11Data CreateRegisterData(Register11Data data)
		{
			Register11Data register;
			using (var context = new WebKikDataContext())
			{
				var data11 = context.Registers11Data.Add(data);
				context.SaveChanges();
				return data11;
			}
		}

		public Register11Data EditRegisterData(Register11Data data)
		{
			Register11Data data11= null;
			using (var context = new WebKikDataContext())
			{
				data11 = context.Registers11Data.Where(d => d.Id == data.Id).FirstOrDefault();
				if (data11 != null)
				{
					data11.BuyerName = data.BuyerName;
					data11.ContractOfRealizationNo = data.ContractOfRealizationNo;
					data11.CostForTransitionOfPropertyRightDateAcquisitionPrice = data.CostForTransitionOfPropertyRightDateAcquisitionPrice;
					data11.CostForTransitionOfPropertyRightDateRevaluationForCurrentYear = data.CostForTransitionOfPropertyRightDateRevaluationForCurrentYear;
					data11.CostForTransitionOfPropertyRightDateRevaluationSummary = data.CostForTransitionOfPropertyRightDateRevaluationSummary;
					data11.CostForTransitionOfPropertyRightDateSummary = data.CostForTransitionOfPropertyRightDateSummary;
					data11.DateOfIssueNumber = data.DateOfIssueNumber;
					data11.IncomeFromRealizationOfAssetOthers = data.IncomeFromRealizationOfAssetOthers;
					data11.IncomeFromRealizationOfAssetSellPrice = data.IncomeFromRealizationOfAssetSellPrice;
					data11.IncomeFromRealizationOfAssetSummary = data.IncomeFromRealizationOfAssetSummary;
					data11.Issuer = data.Issuer;
					data11.MarketValue = data.MarketValue;
					data11.PropertyRightTransitionData = data.PropertyRightTransitionData;
					data11.RealizedFinancialAsset = data.RealizedFinancialAsset;
				}
				context.SaveChanges();
			}
			return data11;
		}

		public Register11Data DeleteRegisterData(int id)
		{
			Register11Data data = null;
			using (var context = new WebKikDataContext())
			{
				data = context.Registers11Data.Where(d => d.Id == id).FirstOrDefault();
				context.Registers11Data.Remove(data);
				context.SaveChanges();
			}
			return null;
		}


		public Register11 Create(Register11 model)
		{
			Register11 register;
			using (var context = new WebKikDataContext())
			{
				register = context.Registers11.Add(new Register11
				{
					Year = model.Year,
					OwnerProjectCompanyId = model.OwnerProjectCompanyId,
					Currency = model.Currency,
					Type = RegisterType.Register11,
					DecisionOfLiquidationData= model.DecisionOfLiquidationData==DateTime.MinValue ? null: model.DecisionOfLiquidationData,
					CompletionOfLiquidationData = model.CompletionOfLiquidationData == DateTime.MinValue ? null : model.DecisionOfLiquidationData
				});
				context.SaveChanges();
			}
			return register;
		}


		//public Register9 CalculateRegisterFields(Register9 register)
		//{
		//	return register;
		//}


		public Register11 GetRegister11(int id)
		{
			Register11 register;
			using (var context = new WebKikDataContext())
			{
				register = context.Registers11.Where(rg11 => (rg11.Id == id)).Include(x => x.Register11Data).FirstOrDefault();
				return register;
			}
		}

		public Register11 CalculateRegisterFields(Register11 register)
		{
			throw new NotImplementedException();
		}
	}
}
