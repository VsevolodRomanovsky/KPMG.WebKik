using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KPMG.WebKik.Contracts.Service;
using KPMG.WebKik.Models;
using System.Linq;
using KPMG.WebKik.Contracts.Repository;

namespace KPMG.WebKik.Services
{
    public class ProjectService : EntityService<Project, int>, IProjectService
    {
        private readonly IEntityRepository<User, int> userRepository;
        public ProjectService(IEntityRepository<Project, int> repository, IEntityRepository<User, int> userRepository) : base(repository)
        {
            this.userRepository = userRepository;
        }

        public override async Task<Project> GetById(int id)
        {
            FilterById(id);
            FilterByUser();
            return await repository.SingleAsync();
        }

        public override async Task<IList<Project>> GetAll()
        {
            FilterByUser();
            return await repository.ToListAsync();
        }

        public async Task<Project> GetProjectOwnership(int id)
        {
            FilterById(id);
            FilterByUser();
            repository.Include(x => x.ProjectCompanies);
            return await repository.SingleAsync();
        }

        public override async Task<Project> Create(Project entity)
        {
            var userLogin = Identity.Name;
            var user = await userRepository.Where(x => x.UserLogin == userLogin).SingleAsync();

            entity.Users.Add(user);
            entity.CreationDate = DateTime.UtcNow;
            repository.Add(entity);
            await repository.SaveChangesAsync();
            return entity;
        }

        private void FilterById(int id)
        {
            repository.Where(x => x.Id == id);
        }

        private void FilterByUser()
        {
            var userLogin = Identity.Name;
            repository.Where(x => x.Users.Any(y => y.UserLogin == userLogin));
        }
    }
}
