using KPMG.WebKik.Data;
using KPMG.WebKik.Models;
using KPMG.WebKik.Models.ProjectCompanies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPMG.WebKik.Contracts.Service.Registers;
using KPMG.WebKik.Contracts.Algorithms;
using KPMG.WebKik.Contracts.Service;
using System.Data.Entity;
using KPMG.WebKik.Services.Helpers;

namespace KPMG.WebKik.Services.Helpers
{
	class TaxStatusHelper
	{
		WebKikDataContext dbContext;
		private IRegister4Service register4Service;
		IFactShareCalculation factShareCalculator;
		IKIKCompanyCalculation kikCompanyCalculation;

		public TaxStatusHelper(WebKikDataContext dbContext, IRegister4Service register4Service, IFactShareCalculation factShareCalculator, IKIKCompanyCalculation kikCompanyCalculation)
		{
			this.register4Service = register4Service;
			this.dbContext = dbContext;
			this.factShareCalculator = factShareCalculator;
			this.kikCompanyCalculation = kikCompanyCalculation;
		}

		internal bool IsISK(TaxExemption entity)
		{
			var result = false;
			bool isISK = false;
			DateTime startDate = new DateTime(entity.Year, 1, 1);
			DateTime endDate = new DateTime(entity.Year, 12, 31);

			var shares = dbContext.ProjectCompanyShares.Where(c => c.DependentProjectCompanyId == entity.DependentProjectCompanyId)
			.Where(x => x.ShareStartDate <= endDate)
			.Where(x => x.ShareFinishDate >= startDate || x.ShareFinishDate == null).ToList();

			if (shares != null && shares.Any())
			{
				var parentCompanyIds = shares.Select(share => share.OwnerProjectCompanyId).Distinct();
				foreach (var parentCompany in parentCompanyIds)
				{
					// Проверяю что Owner=КЛ
					var parentOwner = entity.OwnerProjectCompanyId;
					bool isIHK = IsIHK(entity.Year, parentCompany, parentOwner);
					if (isIHK)
					{
						// проверяю доли прямого участия этого ИХК в кик
						var shareICHK = shares.Where(share => share.OwnerProjectCompanyId == parentCompany);
						if (shareICHK != null && shareICHK.Any())
						{
							// Проверяю долю ИХК в КИК
							var factShares = GetFactShares(parentCompany, entity.Year, shareICHK);
							var calcShare = CalculateShare(factShares);
							isISK = calcShare.All(val => val >= 75);

							// Проверяю что ИХК является КИКом
							var kikShares = dbContext.ProjectCompanyShares.Where(c => c.DependentProjectCompanyId == parentCompany)
								.Where(y => y.OwnerProjectCompanyId == parentOwner)
							.Where(x => x.ShareStartDate <= endDate)
							.Where(x => x.ShareFinishDate >= startDate || x.ShareFinishDate == null)
							.Include(x => x.OwnerProjectCompany).Include(c => c.DependentProjectCompany)
							.Include(x => x.OwnerProjectCompany.DomesticCompany).ToList();

							var factKikShares = GetFactShares(parentOwner, entity.Year, kikShares);
							bool isKik = IsKIK(factKikShares);

							//isISK = shareICHK.Select(val => val.SharePart).All(val => val >= 75);
							isISK = calcShare.All(val => val >= 75);
							if (isISK && isKik)
							{
								result = true;
								return result;
							}
						}
					}
				}
			}
			return result;
		}

		internal bool IsAskShareMoreThan75(TaxExemption entity)
		{
			var result = false;
			DateTime startDate = new DateTime(entity.Year, 1, 1);
			DateTime endDate = new DateTime(entity.Year, 12, 31);

			var shares = dbContext.ProjectCompanyShares.Where(c => c.OwnerProjectCompany.Id == entity.DependentProjectCompanyId)

			.Where(x => x.ShareStartDate <= endDate)
			.Where(x => x.ShareFinishDate >= startDate || x.ShareFinishDate == null).ToList();

			if (shares != null && shares.Any())
			{
				var dependentCompanyIds = shares.Select(share => share.DependentProjectCompanyId).Distinct();
				foreach (var dependentCompany in dependentCompanyIds)
				{
					bool checkIncomeKIKTotalAmount = false;
					bool passivePartWithoutDividendsIncomeValue = false;

					//	Прямое участие в АК >= 50 % за 365 дней
					bool isAskDirectShare = false;

					var askShares = shares.Where(share => share.DependentProjectCompanyId == dependentCompany);
					var factShares = GetFactShares(entity.DependentProjectCompanyId, entity.Year, askShares);
					var calcShare = CalculateShare(factShares);
					isAskDirectShare = calcShare.All(val => val >= 75);

					//isAkDirectShare = askShares.Select(val => val.SharePart).All(val => val >= 50);
					if (isAskDirectShare)
					{
						checkIncomeKIKTotalAmount = CheckIncomeKIKTotalAmount(entity.Year, dependentCompany);
						passivePartWithoutDividendsIncomeValue = CheckPassivePartWithoutDividendsIncomeValue(entity.Year, dependentCompany);
						// проверяю регистр 
						if (checkIncomeKIKTotalAmount || passivePartWithoutDividendsIncomeValue)
						{
							// Проверяю прямое участие в АК 
							bool isAK = false;
							isAK = IsAkShareMoreThan50(entity.Year, dependentCompany);
							if (isAK)
							{
								result = true;
								return result;
							}
						}
					}
				}
			}
			return result;
		}

		internal bool CheckPassivePartWithoutDividendsIncomeValue(int year, int dependentCompany)
		{
			bool passivePartWithoutDividendsIncomeValue = false;
			var register4 = dbContext.Registers4.FirstOrDefault(
							rg4 => (rg4.Year == (Year)year &&
									rg4.OwnerProjectCompanyId == dependentCompany));
			if (register4 != null)
			{
				var register4Full = register4Service.CalculateRegisterFields(register4);
				if (register4Full != null)
				{
					passivePartWithoutDividendsIncomeValue = register4Full.PassivePartWithoutDividendsIncomeValue <= 5;
				}
			}
			return passivePartWithoutDividendsIncomeValue;
		}

		internal bool IsAkShareMoreThan50(int year, int companyId)
		{
			var result = false;
			//int kl = entity.OwnerProjectCompanyId;
			DateTime startDate = new DateTime(year, 1, 1);
			DateTime endDate = new DateTime(year, 12, 31);

			var shares = dbContext.ProjectCompanyShares.Where(c => c.OwnerProjectCompany.Id == companyId)
				.Where(x => x.ShareStartDate <= endDate)
				.Where(x => x.ShareFinishDate >= startDate || x.ShareFinishDate == null).ToList();

			if (shares != null && shares.Any())
			{
				var dependentCompanyIds = shares.Select(share => share.DependentProjectCompanyId).Distinct();
				foreach (var dependentCompany in dependentCompanyIds)
				{
					bool isAK;
					var register4 = dbContext.Registers4.FirstOrDefault(
							rg4 => (rg4.Year == (Year)year &&
									rg4.OwnerProjectCompanyId == dependentCompany));
					if (register4 != null)
					{
						isAK = register4.PassivePartIncomeValue <= 20;
						if (isAK)
						{
							var akShares = shares.Where(share => share.DependentProjectCompanyId == dependentCompany);
							var factShares = GetFactShares(companyId, year, akShares);
							var calcShare = CalculateShare(factShares);
							if (calcShare != null && calcShare.Any())
							{
								if (calcShare.All(val => val >= 50))
								{
									return true;
								}
							}

							//if (akShares.Select(val => val.SharePart).All(val => val >= 50))
							//{
							//	result = true;
							//	return result;
							//}
						}
					}
				}
			}
			return result;
		}

		internal bool CheckPassivePartWithoutDividends(int year, int companyId)
		{
			bool passivePartWithoutDividends = false;
			var register4 = dbContext.Registers4.FirstOrDefault(
										rg4 => (rg4.Year == (Year)year &&
												rg4.OwnerProjectCompanyId == companyId));
			if (register4 != null)
			{
				var register4Full = register4Service.CalculateRegisterFields(register4);
				if (register4Full != null)
				{
					passivePartWithoutDividends = register4Full.PassivePartWithoutDividendsAndHoldingsIncomeValue <= 5;
				}
			}
			return passivePartWithoutDividends;
		}

		internal bool CheckIncomeKIKTotalAmount(int year, int companyId)
		{
			var checkIncomeKIKTotalAmount = false;
			var register4 = dbContext.Registers4.FirstOrDefault(
										rg4 => (rg4.Year == (Year)year &&
												rg4.OwnerProjectCompanyId == companyId));

			if (register4 != null)
			{
				checkIncomeKIKTotalAmount = register4.IncomeKIKTotalAmount == 0;
			}
			return checkIncomeKIKTotalAmount;
		}

		internal bool IsIHK(int year, int companyId, int owner)
		{
			var isIHK = false;
			DateTime startDate = new DateTime(year, 1, 1);
			DateTime endDate = new DateTime(year, 12, 31);

			var shares = dbContext.ProjectCompanyShares.Where(c => c.OwnerProjectCompany.Id == owner)
				.Where(c => c.DependentProjectCompany.Id == companyId)
				.Where(x => x.ShareStartDate <= endDate)
				.Where(x => x.ShareFinishDate >= startDate || x.ShareFinishDate == null).ToList();

			var factShares = GetFactShares(owner, year, shares);
			var calcShare = CalculateShare(factShares);
			if (calcShare != null && calcShare.Any())
			{
				isIHK = calcShare.All(val => val >= 75);
			}
			//if (shares != null && shares.Any())
			//{
			//	isIHK = shares.Select(val => val.SharePart).All(val => val >= 75);
			//}
			return isIHK;
		}



		internal IList<ProjectCompanyFactShare> GetFactShares(int companyId, int year, IEnumerable<ProjectCompanyShare> shares)
		{
			IEnumerable<ProjectCompanyFactShare> factShares = new List<ProjectCompanyFactShare>();

			if (shares != null && shares.Any())
			{

				IList<DateTime> accum = new List<DateTime>();
				// Заполняем началом и концом года 
				accum.Add(new DateTime(year, 1, 1));
				accum.Add(new DateTime(year, 12, 31));
				foreach (var share in shares)
				{
					accum.Add(share.ShareStartDate);
					// Добавляем окрестности владения для проверки пустот
					accum.Add(share.ShareStartDate.AddDays(-1));
					accum.Add(share.ShareStartDate.AddDays(1));
					if (share.ShareFinishDate.HasValue)
					{
						accum.Add(share.ShareFinishDate.Value);
						// Добавляем окрестности владения для проверки пустот
						accum.Add(share.ShareFinishDate.Value.AddDays(-1));
						accum.Add(share.ShareFinishDate.Value.AddDays(1));
					}
				}
				// выбрасываем повторы 
				accum = accum.Distinct().ToList();
				//Выбрасываем если попал другой год из окрестности
				accum = accum.Where(ac => ac.Year == year).ToList();
				//accum.OrderBy(x => x);

				// owner 
				var ownerId = shares.First().OwnerProjectCompanyId;
				// dependent 
				var dependent = shares.First().DependentProjectCompanyId;

				foreach (DateTime datePoint in accum)
				{
					var sharesAcc = dbContext.ProjectCompanyShares.Where(c => c.OwnerProjectCompany.Id == ownerId)
					.Where(c => c.DependentProjectCompany.Id == dependent)
					.Where(x => x.ShareStartDate <= datePoint)
					.Where(x => x.ShareFinishDate >= datePoint || x.ShareFinishDate == null).ToList();

					if (sharesAcc != null && sharesAcc.Any())
					{
						var factPointShares = factShareCalculator.GetFactShares(sharesAcc).Where(x => x.OwnerProjectCompanyId == companyId);
						factShares = factShares.Concat(factPointShares);
					}
					else
					{
						factShares = factShares.Append<ProjectCompanyFactShare>(new ProjectCompanyFactShare { ShareDirectPart = 0 });
					}
				}

			}
			return factShares.ToList();
		}

		internal IList<double> CalculateShare(IList<ProjectCompanyFactShare> shares)
		{
			IList<double> directSharesValues = new List<double>();
			foreach (var share in shares)
			{
				directSharesValues.Add(share.ShareDirectPart);
			}
			return directSharesValues;
		}

		bool IsKIK(IList<ProjectCompanyFactShare> shares)
		{
			foreach (var share in shares)
			{
				if (!kikCompanyCalculation.IsKIKCompany(share))
				{
					return false;
				}
			}
			return true;
		}
	}
	public static class EnumerableExtensions
	{
		public static IEnumerable<T> Append<T>(this IEnumerable<T> source, params T[] tail)
		{
			return source.Concat(tail);
		}
	}
}
