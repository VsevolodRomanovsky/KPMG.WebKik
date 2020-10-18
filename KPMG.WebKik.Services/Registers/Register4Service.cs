using KPMG.WebKik.Contracts.Service.Registers;
using KPMG.WebKik.Contracts.Repository;
using KPMG.WebKik.Models.Registers;
using System;

namespace KPMG.WebKik.Services.Registers
{
    public class Register4Service : EntityService<Register4, int>, IRegister4Service
    {
        public Register4Service(IEntityRepository<Register4, int> repository) : base(repository)
        {
        }

        public Register4 CalculateRegisterFields(Register4 register)
        {
            return CalculateRegister4Fields(register);
        }

        private Register4 CalculateRegister4Fields(Register4 register)
        {
            // Доходы, не учитываемые при расчете доли пассивных доходов
            register.IncomeNotIncluded =
                Round(
                        () => register.IncomeFromRateDifference + register.IncomeFromRegisteredCapitals
                            + register.IncomeFromShares + register.IncomeFromSecurities
                            + register.IncomeFromDerivatives + register.IncomeFromSubsidiary
                            + register.IncomeFromReserveRestore
                    );

            // Общая сумма доходов КИК, уменьшенная на величину доходов, неучитываемых при расчете доли пассивных доходов
            register.IncomeKIKSummary =
                Round(
                        () => register.IncomeKIKTotalAmount - register.IncomeNotIncluded
                     );

            // Дивиденды, полученные иностранной компанией (всего), в том числе:
            register.DividendsSum =
                Round(
                        () => register.DividendsFromActiveCompanies + register.DividendsFromHoldingCompanies
                     );

            // Доходы от оказания услуг
            register.ServicesIncome =
            Round(
                    () => register.ServicesConsultingIncome + register.ServicesLegalIncome
                        + register.ServicesAccountingIncome + register.ServicesAuditIncome
                        + register.ServicesEngineeringIncome + register.ServicesAdvertisingIncome
                        + register.ServicesMarketingIncome + register.ServicesInformationProcessingIncome
                        + register.ServicesReseachAndDevelopmentIncome
                 );

            // Сумма доходов КИК от пассивной деятельности (всего), в том числе:
            register.PassiveIncomeKIKSummary =
                Round(
                        () => register.DividendsSum + register.IncomeFromAppropriationProfit
                            + register.IncomeFromDebentures + register.IntellectialPropRightsIncome
                            + register.SharedPartsIncome + register.FISSIncome
                            + register.ImmovablePropertyIncome + register.LeasePropertyIncome
                            + register.InvestmentUnitsIncome + register.ServicesIncome
                            + register.ServicesStaffProvidingIncome + register.OtherIncome
                    );
            //Сумма доходов, указанных в пункте 4 статьи 309.1 НК РФ (за исключением дивидендов от активных иностранных компаний)
            register.IncomeSumExceptDividendsFromActiveCompanies =
                Round(
                        () => register.PassiveIncomeKIKSummary - register.DividendsFromActiveCompanies
                    );

            //Сумма доходов, указанных в пункте 4 статьи 309.1 НК РФ (за исключением дивидендов от активных иностранных компаний и (или) активных иностранных субхолдинговых компаний)
            register.IncomeSumExceptDividendsFromHoldingCompanies =
                Round(
                        () => register.PassiveIncomeKIKSummary - register.DividendsFromActiveCompanies
                            - register.DividendsFromHoldingCompanies
                    );

            //Доля пассивных доходов в общей сумме доходов*, %
            register.PassivePartIncomeValue = register.IncomeKIKSummary != 0
                ? Round(
                            () => register.PassiveIncomeKIKSummary / register.IncomeKIKSummary * 100
                        )
                : 0;

            //Доля пассивных доходов в общей сумме доходов, рассчитанная без учета дивидендов от активных иностранных компаний
            register.PassivePartWithoutDividendsIncomeValue = register.IncomeKIKSummary != 0
              ? Round(
                          () => register.IncomeSumExceptDividendsFromActiveCompanies / register.IncomeKIKSummary * 100
                      )
              : 0;

            //Доля пассивных доходов в общей сумме доходов, рассчитанная без учета дивидендов от активных иностранных компаний и активных иностранных субхолдинговых компаний
            register.PassivePartWithoutDividendsAndHoldingsIncomeValue = register.IncomeKIKSummary != 0
              ? Round(
                          () => register.IncomeSumExceptDividendsFromHoldingCompanies / register.IncomeKIKSummary * 100
                      )
              : 0;

            return register;
        }

        private double Round(Func<double> func)
        {
            return Math.Round(func(), 2);
        }
    }
}
