using System.Collections.Generic;
using System.Threading.Tasks;
using KPMG.WebKik.Contracts.Service;
using KPMG.WebKik.Models;
using System.Linq;
using KPMG.WebKik.Contracts.Repository;

namespace KPMG.WebKik.Services
{
    public class UserService : EntityService<User, int>, IUserService
    {
        private readonly IEntityRepository<Project, int> projectRepository;
        public UserService(IEntityRepository<User, int> repository, IEntityRepository<Project, int> projectRepository) : base(repository)
        {
            this.projectRepository = projectRepository;
        }

        public async Task<User> AddUserToProject(int projectId, int userId)
        {
            var project = await projectRepository.GetByIdAsync(projectId);
            var user = await repository.GetByIdAsync(userId);
            user.Projects.Add(project);
            await repository.SaveChangesAsync();
            return user;
        }

        public async Task<IList<User>> GetUsersByProjectId(int projectId)
        {
            return await repository.Where(x => x.Projects.Any(y => y.Id == projectId)).ToListAsync();
        }

        public async Task RemoveUserFromProject(int projectId, int userId)
        {
            var user = await repository.Where(x => x.Id == userId).Include(x => x.Projects).SingleAsync();
            var project = user.Projects.Single(x => x.Id == projectId);
            user.Projects.Remove(project);
            await repository.SaveChangesAsync();
        }

        public override async Task<IList<User>> GetAll()
        {
            return await repository.Include(x => x.Role).ToListAsync();
        }

    }
}
