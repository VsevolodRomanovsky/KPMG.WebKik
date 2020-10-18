using KPMG.WebKik.Contracts.Service.Registers;
using System;
using KPMG.WebKik.Contracts.Repository;
using KPMG.WebKik.Models.Registers;
using KPMG.WebKik.Data;
using System.Linq;
using KPMG.WebKik.Models;

namespace KPMG.WebKik.Services.Registers
{
    public class Register1Service : EntityService<Register1, int>, IRegister1Service
    {
        const double StandartKKIKProfit2015 = 50000000;
        const double StandartKKIKProfit2016 = 30000000;
        const double StandartKKIKProfit2017 = 10000000;

        IRegister2Service register2Service;

        public Register1Service(IEntityRepository<Register1, int> repository, IRegister2Service register2Service) : base(repository)
        {
            this.register2Service = register2Service;
        }

        public Register1 GetRegister1ByCompanyId(int companyId, int year)
        {
            Register1 register;
            using (var context = new WebKikDataContext())
            {
                register = context.Registers1.Where(rg1 => (rg1.OwnerProjectCompanyId == companyId && rg1.Year == (Year)year)).FirstOrDefault();
                return register;
            }
        }

        public Register1 CalculateRegisterFields(Register1 register)
        {
            return CalculateRegister1Fields(register);
        }

        private Register1 CalculateRegister1Fields(Register1 register)
        {

            var reg0102 = register2Service.GetRegister2ByCompanyId(register.OwnerProjectCompanyId, (int)register.Year);


            //Доходы, не учитываемые при определении прибыли (убытка) КИК (всего), в том числе:
            register.IncomeKIKNotIncluded =
                Round(
                        () => register.IncomeFromRegisteredCapitals + register.IncomeFromShares
                            + register.IncomeFromSecurities + register.IncomeFromDerivatives
                            + register.IncomeFromSubsidiary + register.IncomeFromReserveRestore
                    );

            //Расходы, не учитываемые при определении прибыли (убытка) КИК (всего), в том числе:
            register.CostsKIKNotIncluded =
                Round(
                        () => register.CostsFromRegisteredCapitals + register.CostsFromShares
                            + register.CostsFromSecurities + register.CostsFromDerivatives
                            + register.CostsFromSubsidiary + register.CostsFromReserveRestore
                    );

            //Общая сумма доходов и расходов,  не учитываемых при определении прибыли (убытка)
            register.IncomeAndCostsTotalAmount =
                Round(
                        () => register.IncomeKIKNotIncluded - register.CostsKIKNotIncluded
                     );

            //Переоценка долей в уставном (складочном) капитале (фонде),  паев в паевых фондах кооперативов
            // и паевых инвестиционных фондах, ценных бумаг и производных финансовых инструментов, реализованных КИК, в том числе:
            register.ReassessmentTotalAmount =
                Round(
                        () => register.ReassessmentFromRegisteredCapitals + register.ReassessmentFromShares
                            + register.ReassessmentFromSecurities + register.ReassessmentFromDerivatives
                    );

            // Общая сумма корректировки прибыли (убытка) КИК
            register.ProfitTotalAmountCorrection =
                Round(
                        () => register.ReassessmentTotalAmount + register.IncomeSymmetricCorrection
                            + register.IncomeNotIncludedInProfit - register.CostsIncludedInProfit
                     );

            // Скорректированная величина прибыли (убытка) КИК
            register.AdjustedProfitAmount =
                Round(
                        () => register.ProfitAmountBeforeTax - register.IncomeAndCostsTotalAmount
                            - register.ProfitTotalAmountCorrection
                      );

            // Суммы, исключаемые из прибыли КИК - всего, в том числе:
            register.ProfitExclusion =
                Round(
                        () => register.DividendsCurrentYear + register.DistributedProfitAmount
                            + register.IncomeProperty - register.LossProperty
                     );

            // Величина прибыли (убытка)
            register.ProfitAmount =
                Round(
                        () => register.AdjustedProfitAmount - register.ProfitExclusion
                    );

            // Величина прибыли КИК, пересчитанная по среднему курсу, руб.
            register.ProfitAmountConvertedCurrency =
                Round(
                        () => register.ProfitAmount * register.AverageForeignCurrency
                    );

            //Величина прибыли (убытка) для целей налогообложения
            register.ProfitAmountForTax = register.ProfitAmountConvertedCurrency < register.StandartKKIKProfit
                ? 0
                : Round(
                            () => register.ProfitAmount - register.ReceivedDividends
                                - register.ProfitAmountCurrentYear
                        );

            //Сумма убытка прошлых лет, уменьшающая прибыль КИК за отчетный финансовый год

            var lossSumTaxBase = reg0102 != null ? reg0102.LossSumTaxBase : 0;

            register.LossKIKFromPastYears =
                register.ProfitAmountForTax > 0 ? lossSumTaxBase : 0;

            //Величина прибыли, подлежащая к учету
            register.CountableProfitAmountForTax = register.ProfitAmountForTax - register.LossKIKFromPastYears < 0
                ? 0
                : Round(
                            () => register.ProfitAmountForTax - register.LossKIKFromPastYears
                        );
            
            //Норматив прибыли КИК, освобождаемой от налогообложения, руб.
            register.StandartKKIKProfit =
                register.Year == Models.Year.Year2015 ? StandartKKIKProfit2015 :
                register.Year == Models.Year.Year2016 ? StandartKKIKProfit2016 :
                    StandartKKIKProfit2017;

            register.ProfitAmountForTax =
                register.ProfitAmountConvertedCurrency < register.StandartKKIKProfit ? 0 :
                register.ProfitAmount - register.ReceivedDividends - register.ProfitAmountCurrentYear;

            

            /*
            register.ShareInKIKProfit =
                register.LossKIKFromPastYears - register.ProfitAmountForTax > 0 ?
                register.LossKIKFromPastYears - register.ProfitAmountForTax : 0;
            */

            register.PartKIKProfit =
                Round(
                        () => register.CountableProfitAmountForTax * register.AverageForeignCurrency * register.ShareInKIKProfit
                    );

            //Налоговая база для исчисления налога, руб.
            register.KIKTaxBase =
                Round(
                        () => register.PartKIKProfit - register.ControlledProfitAmount
                     );



            return register;
        }

        private double Round(Func<double> func)
        {
            return Math.Round(func(), 2);
        }

    }
}
