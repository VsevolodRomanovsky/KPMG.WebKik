using System.Data.Entity.Migrations;
using System.Linq;
using KPMG.Webkik.Utils;
using KPMG.WebKik.Import;

namespace KPMG.WebKik.Data.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<WebKikDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
			AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(WebKikDataContext context)
        {
            string shouldImport;
            if (!AppSettings.TryGet("WebKik.Import.ShouldImport", out shouldImport) || 
                shouldImport.Trim().ToLower() != "true")
            {
                return;
            }

            var importer = new Importer(context);
            string filePath;
            if (!AppSettings.TryGet("WebKik.Import.Directories.RelativeFilePath", out filePath))
            {
                return;
            }

            var data = importer.GetDirectoriesData(filePath);
            importer.ImportDirectories(data);
            context.SaveChanges();

            ImportEntities(importer, context);
            context.SaveChanges();
        }

        private static void ImportEntities(Importer importer, WebKikDataContext context)
        {
            var roles = importer.GetRoles();
            var users = importer.GetUsers(roles.ToList());
            var projects = importer.GetProjects(users);
            var companies = importer.GetCompanies(projects);
            var shares = importer.GetShares(companies);

            context.Roles.AddOrUpdate(x => x.Name, roles.ToArray());
            context.Users.AddOrUpdate(p => p.UserLogin, users.ToArray());
            context.Projects.AddOrUpdate(p => p.Name, projects.ToArray());
            context.ProjectCompanies.AddOrUpdate(x => x.Name, companies.ToArray());
            context.ProjectCompanyShares.AddOrUpdate(x => x.Id, shares.ToArray());
        }
    }
}
