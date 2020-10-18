using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPMG.WebKik.Contracts.Service.Registers;
using KPMG.WebKik.Data;
using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Services.Registers
{
    public class Register10Service : BaseService, IRegister10Service
    {
        WebKikDataContext context;
        public Register10Service()
        {
            context = new WebKikDataContext();
        }

        public Register10 GetRegister10(int id)
        {
            Register10 register;
            using (var context = new WebKikDataContext())
            {
                register = context.Registers10.Where(x => (x.Id == id)).Include(x => x.Register10Data).FirstOrDefault();
                return register;
            }
        }

        public Register10 Create(Register10 model)
        {
            Register10 register;
            using (var context = new WebKikDataContext())
            {
                register = context.Registers10.Add(new Register10
                {
                    Year = model.Year,
                    OwnerProjectCompanyId = model.OwnerProjectCompanyId,
                    Currency = model.Currency,
                    Type = RegisterType.Register10
                });
                context.SaveChanges();
            }
            return register;
        }

        public Register10Data DeleteRegisterData(int id)
        {
            Register10Data data10 = null;
            using (var context = new WebKikDataContext())
            {
                data10 = context.Registers10Data.FirstOrDefault(d => d.Id == id);
                context.Registers10Data.Remove(data10);
                context.SaveChanges();
            }
            return data10;
        }

        public Register10Data CreateRegisterData(Register10Data data)
        {
            using (var context = new WebKikDataContext())
            {
                var data10 = context.Registers10Data.Add(new Register10Data
                {
                    Register10Id = data.Register10Id,
                    SectionId = data.SectionId,
                    AssetType = data.AssetType,
                    CauseDisposal = data.CauseDisposal,
                    Num1 = data.Num1,
                    Num2 = data.Num2,
                    Num3 = data.Num3,
                    Num4 = data.Num4
                });
                context.SaveChanges();
                return data10;
            }
        }

        public Register10Data EditRegisterData(Register10Data data)
        {
            Register10Data data10 = null;
            using (var context = new WebKikDataContext())
            {
                data10 = context.Registers10Data.FirstOrDefault(d => d.Id == data.Id);
                if (data10 != null)
                {
                    data10.SectionId = data.SectionId;
                    data10.AssetType = data.AssetType;
                    data10.CauseDisposal = data.CauseDisposal;
                    data10.Register10Id = data.Register10Id;
                    data10.Num1 = data.Num1;
                    data10.Num2 = data.Num2;
                    data10.Num3 = data.Num3;
                    data10.Num4 = data.Num4;
                }
                context.SaveChanges();
            }
            return data10;
        }

        public Register10 CalculateRegisterFields(Register10 register)
        {
            throw new NotImplementedException();
        }
    }
}
