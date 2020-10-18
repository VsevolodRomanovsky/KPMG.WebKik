using KPMG.WebKik.Contracts.Service.Registers;
using System;
using KPMG.WebKik.Contracts.Repository;
using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Services.Registers
{
    public class Register6Service : EntityService<Register6, int>, IRegister6Service
    {
        public Register6Service(IEntityRepository<Register6, int> repository) : base(repository)
        {
        }

        public Register6 CalculateRegisterFields(Register6 register)
        {
            return CalculateRegister6Fileds(register);
        }

        private Register6 CalculateRegister6Fileds(Register6 entity)
        {
           if (entity.KIKProfit == 0)
           {
                entity.IncomeTaxEffected = 0;
                entity.KIKDividends = 0;
                entity.KIKProfitMinusDividends = 0;
                entity.AverageTaxRate = 0;
                entity.AverageTaxRatePart = 0;
                return entity;
           }

            entity.IncomeTaxEffected =
                Round(
                        () => (entity.IncomeTaxRated + entity.IncomeTaxDeducted + entity.IncomeTaxCorrection)
                            / entity.KIKProfit * 100
                     );

            entity.KIKProfitMinusDividends =
                Round(
                        () => entity.KIKProfit - entity.KIKDividends
                     );

            entity.AverageTaxRate = entity.KIKProfitMinusDividends + entity.KIKDividends != 0
                ? Round(
                            () => (entity.KIKProfitMinusDividends * Register6.ProfitTaxRateArticle1
                                + entity.KIKDividends * Register6.ProfitTaxRateArticel3)
                                / (entity.KIKProfitMinusDividends + entity.KIKDividends) * 100
                        )
                : 0;

            entity.AverageTaxRatePart = entity.AverageTaxRate != 0
                ? Round(
                            () => entity.IncomeTaxEffected / entity.AverageTaxRate * 100
                        )
                : 0;

            return entity;
        }

        private double Round(Func<double> func)
        {
            return Math.Round(func(), 2);
        }
    }
}
