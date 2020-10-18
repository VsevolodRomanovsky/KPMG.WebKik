using KPMG.WebKik.Contracts.Service.Registers;
using KPMG.WebKik.Contracts.Repository;
using KPMG.WebKik.Models.Registers;
using System;
using KPMG.WebKik.Data;
using System.Linq;
using KPMG.WebKik.Models;

namespace KPMG.WebKik.Services.Registers
{
    public class Register2Service : EntityService<Register2, int>, IRegister2Service
    {
        public Register2Service(IEntityRepository<Register2, int> repository) : base(repository)
        {
        }

        public Register2 GetRegister2ByCompanyId(int companyId, int year)
        {
            Register2 register;
            using (var context = new WebKikDataContext())
            {
                register = context.Registers2.Where(rg2 => (rg2.OwnerProjectCompanyId == companyId && rg2.Year == (Year)year)).FirstOrDefault();
                return register;
            }
        }

        public Register2 CalculateRegisterFields(Register2 register)
        {
            return CalculateRegister2Fileds(register);
        }

        private Register2 CalculateRegister2Fileds(Register2 entity)
        {
            entity.BalanceLoss =
                Round(
                        () => entity.BalanceLoss2012 + entity.BalanceLoss2013
                            + entity.BalanceLoss2014 + entity.BalanceLoss00
                            + entity.BalanceLoss01
                    );

            entity.FullBalanceLoss2012 =
                Round(
                        () => entity.BalanceLoss2012 - entity.SumLoss2012
                     );

            entity.FullBalanceLoss2013 =
                Round(
                        () => entity.BalanceLoss2013 - entity.SumLoss2013
                     );

            entity.FullBalanceLoss2014 =
                Round(
                        () => entity.BalanceLoss2014 - entity.SumLoss2014
                     );

            entity.FullBalanceLoss00 =
                Round(
                        () => entity.BalanceLoss00 - entity.SumLoss00
                    );

            entity.FullBalanceLoss01 =
                Round(
                        () => entity.BalanceLoss01 - entity.SumLoss01
                     );

            entity.FullBalanceLoss =
                Round(
                        () => entity.FullBalanceLoss2012 + entity.FullBalanceLoss2013
                            + entity.FullBalanceLoss2014 + entity.FullBalanceLoss00
                            + entity.FullBalanceLoss01
                     );

            if (entity.TaxBaseForTaxPeriod > 0)
            {
                entity.LossSumTaxBase =
                    Round(
                            () => entity.SumLoss2012 + entity.SumLoss2013
                                + entity.SumLoss2014 + entity.SumLoss00
                                + entity.SumLoss01
                         );
            }

            return entity;
        }

        private double Round(Func<double> func)
        {
            return Math.Round(func(), 2);
        }
    }
}
