using KPMG.WebKik.Contracts.Service.Registers;
using System;
using KPMG.WebKik.Contracts.Repository;
using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Services.Registers
{
    public class Register7Service : EntityService<Register7, int>, IRegister7Service
    {
        public Register7Service(IEntityRepository<Register7, int> repository) : base(repository)
        {
        }

        public Register7 CalculateRegisterFields(Register7 register)
        {
            return CalculateRegister7Fileds(register);
        }

        private Register7 CalculateRegister7Fileds(Register7 entity)
        {
            entity.PartPercentSRPIncome = entity.KikTotalSumIncome != 0
                ? Round(() => entity.SumIncomeSRP / entity.KikTotalSumIncome * 100)
                : 0;

            return entity;
        }

        private double Round(Func<double> func)
        {
            return Math.Round(func(), 2);
        }
    }
}
