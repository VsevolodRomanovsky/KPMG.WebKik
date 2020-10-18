using KPMG.WebKik.Contracts.Service.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPMG.WebKik.Models.Registers;
using KPMG.WebKik.Contracts.Repository;
using KPMG.WebKik.Models;
using System.Security.Principal;
using KPMG.WebKik.Data;

namespace KPMG.WebKik.Services.Registers
{
    public class RegisterService : IRegisterService
    {
        private readonly IEntityRepository<Register1, int> register1Repository;
        private readonly IEntityRepository<Register2, int> register2Repository;
        private readonly IEntityRepository<Register3, int> register3Repository;
        private readonly IEntityRepository<Register4, int> register4Repository;
        private readonly IEntityRepository<Register5, int> register5Repository;
        private readonly IEntityRepository<Register6, int> register6Repository;
        private readonly IEntityRepository<Register7, int> register7Repository;

        public RegisterService(IEntityRepository<Register1, int> register1Repository, IEntityRepository<Register2, int> register2Repository,
            IEntityRepository<Register3, int> register3Repository, IEntityRepository<Register4, int> register4Repository,
            IEntityRepository<Register5, int> register5Repository, IEntityRepository<Register6, int> register6Repository,
            IEntityRepository<Register7, int> register7Repository)
        {
            this.register1Repository = register1Repository;
            this.register2Repository = register2Repository;
            this.register3Repository = register3Repository;
            this.register4Repository = register4Repository;
            this.register5Repository = register5Repository;
            this.register6Repository = register6Repository;
            this.register7Repository = register7Repository;
        }

        public async Task<IEnumerable<RegisterListItem>> GetRegistersByShareIdAndYear(int projectCompanyId, int year)
        {
            var registersList = new List<RegisterListItem>();

            /// todo: Refactor into loop

            //register1
            var register1 = await register1Repository.Where(x => x.Year == (Year)year && x.OwnerProjectCompanyId == projectCompanyId).FirstOrDefaultAsync();
            registersList.Add(new RegisterListItem
                {
                    Id = register1 != null ? register1.Id : 0,
                    Type = register1 != null ? register1.Type : RegisterType.Register1,
                    IsFilled = register1 != null
                });

            //register2
            var register2 = await register2Repository.Where(x => x.Year == (Year)year && x.OwnerProjectCompanyId == projectCompanyId).FirstOrDefaultAsync();
            registersList.Add(new RegisterListItem
            {
                Id = register2 != null ? register2.Id : 0,
                Type = register2 != null ? register2.Type : RegisterType.Register2,
                IsFilled = register2 != null
            });

            //register3
            var register3 = await register3Repository.Where(x => x.Year == (Year)year && x.OwnerProjectCompanyId == projectCompanyId).FirstOrDefaultAsync();
            registersList.Add(new RegisterListItem
            {
                Id = register3 != null ? register3.Id : 0,
                Type = register3 != null ? register3.Type : RegisterType.Register3,
                IsFilled = register3 != null
            });

            //register4
            var register4 = await register4Repository.Where(x => x.Year == (Year)year && x.OwnerProjectCompanyId == projectCompanyId).FirstOrDefaultAsync();
            registersList.Add(new RegisterListItem
            {
                Id = register4 != null ? register4.Id : 0,
                Type = register4 != null ? register4.Type : RegisterType.Register4,
                IsFilled = register4 != null
            });

            //register5
            var register5 = await register5Repository.Where(x => x.Year == (Year)year && x.OwnerProjectCompanyId == projectCompanyId).FirstOrDefaultAsync();
            registersList.Add(new RegisterListItem
            {
                Id = register5 != null ? register5.Id : 0,
                Type = register5 != null ? register5.Type : RegisterType.Register5,
                IsFilled = register5 != null
            });


            //register6
            var register6 = await register6Repository.Where(x => x.Year == (Year)year && x.OwnerProjectCompanyId == projectCompanyId).FirstOrDefaultAsync();
            registersList.Add(new RegisterListItem
            {
                Id = register6 != null ? register6.Id : 0,
                Type = register6 != null ? register6.Type : RegisterType.Register6,
                IsFilled = register6 != null
            });

            //register7
            var register7 = await register7Repository.Where(x => x.Year == (Year)year && x.OwnerProjectCompanyId == projectCompanyId).FirstOrDefaultAsync();
            registersList.Add(new RegisterListItem
            {
                Id = register7 != null ? register7.Id : 0,
                Type = register7 != null ? register7.Type : RegisterType.Register7,
                IsFilled = register7 != null
            });
			Register8 register8 = null;
			using (var context = new WebKikDataContext())
			{
				register8 = context.Registers8.Where(rg8 => (rg8.Year == year && rg8.OwnerProjectCompanyId == projectCompanyId)).FirstOrDefault();
				
			}
			registersList.Add(new RegisterListItem
			{
				Id = register8 != null ? register8.Id : 0,
				Type = register8 != null ? register8.Type : RegisterType.Register8,
				IsFilled = register8 != null
			});
			Register9 register9 = null;
			using (var context = new WebKikDataContext())
			{
				register9 = context.Registers9.Where(rg9 => (rg9.Year == year && rg9.OwnerProjectCompanyId == projectCompanyId)).FirstOrDefault();

			}
			registersList.Add(new RegisterListItem
			{
				Id = register9 != null ? register9.Id : 0,
				Type = register9 != null ? register9.Type : RegisterType.Register9,
				IsFilled = register9 != null
			});
			
            Register10 register10;
            using (var context = new WebKikDataContext())
            {
                register10 = context.Registers10.FirstOrDefault(x => x.Year == year && x.OwnerProjectCompanyId == projectCompanyId);

            }
            registersList.Add(new RegisterListItem
            {
                Id = register10?.Id ?? 0,
                Type = register10?.Type ?? RegisterType.Register10,
                IsFilled = register10 != null
            });
			Register11 register11;
			using (var context = new WebKikDataContext())
			{
				register11 = context.Registers11.FirstOrDefault(x => x.Year == year && x.OwnerProjectCompanyId == projectCompanyId);

			}
			registersList.Add(new RegisterListItem
			{
				Id = register11 != null ? register11.Id : 0,
				Type = register11 != null ? register11.Type : RegisterType.Register11,
				IsFilled = register11 != null
			});
			return registersList;
        }
    }
}
