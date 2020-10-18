using KPMG.WebKik.Contracts.Service.Registers;
using KPMG.WebKik.Contracts.Repository;
using KPMG.WebKik.Models.Registers;
using System;
using KPMG.WebKik.Data;
using System.Linq;
using KPMG.WebKik.Models;

namespace KPMG.WebKik.Services.Registers
{
    public class Register3Service : EntityService<Register3, int>, IRegister3Service
    {
        private readonly IEntityRepository<Register1, int> register1Repository;

        public Register3Service(IEntityRepository<Register3, int> repository, IEntityRepository<Register1, int> register1Repository) : base(repository)
        {
            this.register1Repository = register1Repository;
        }

        public Register3 CalculateRegisterFields(Register3 register)
        {
            return CalculateRegister3Fields(register);
        }

        public Register3 GetRegister3ByCompanyId(int companyId, int year)
        {
            Register3 register;
            using (var context = new WebKikDataContext())
            {
                register = context.Registers3.Where(rg3 => (rg3.OwnerProjectCompanyId == companyId && rg3.Year == (Year)year)).FirstOrDefault();
                return register;
            }
        }

        private Register3 CalculateRegister3Fields(Register3 register)
        {
            var register1 = register1Repository.Where(x => x.OwnerProjectCompanyId == register.OwnerProjectCompanyId
                && x.Year == register.Year).FirstOrDefaultAsync().Result;

            // Прибыль КИК, валюта (берется из регистра 1, строка Величина прибыли, подлежащая к учету)
            register.KIKProfitCurrency = register1 != null ? register1.CountableProfitAmountForTax : 0;

            //Прибыль КИК, руб
            register.KIKProfitRUR =
                Round(
                        () => register.KIKProfitCurrency * register.KIKConversionRate
                    );

            //Прибыль КИК пропорционально доле участия (доле прибыли КИК), руб
            register.KIKProfitSharePart =
                Round(
                        () => register.KIKSharePart * register.KIKProfitRUR
                    );

            return register;
        }

        private double Round(Func<double> func)
        {
            return Math.Round(func(), 2);
        }
    }
}
