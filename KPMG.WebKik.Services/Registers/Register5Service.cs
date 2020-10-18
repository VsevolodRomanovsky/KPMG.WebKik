using KPMG.WebKik.Contracts.Service.Registers;
using System;
using KPMG.WebKik.Contracts.Repository;
using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Services.Registers
{
    public class Register5Service : EntityService<Register5, int>, IRegister5Service
    {
        public Register5Service(IEntityRepository<Register5, int> repository) : base(repository)
        {
        }

        public Register5 CalculateRegisterFields(Register5 register)
        {
            return CalculateRegister5Fields(register);
        }

        private Register5 CalculateRegister5Fields(Register5 entity)
        {
            entity.PartPercentForBondsProfit = entity.SumProfit != 0
                ? Round(() => entity.SumPercentForBondsProfit / entity.SumProfit * 100)
                : 0;

            return entity;
        }

        private double Round(Func<double> func)
        {
            return Math.Round(func(), 2);
        }
    }
}
