using System.Data.Entity;
using System.Web.Http;
using KPMG.WebKik.Contracts.Repository;
using KPMG.WebKik.Data;
using KPMG.WebKik.Services;
using Microsoft.Practices.Unity;
using KPMG.WebKik.Contracts.Service;
using KPMG.WebKik.Contracts.Algorithms;
using KPMG.WebKik.Algorithms;
using KPMG.WebKik.Contracts.Service.Companies;
using KPMG.WebKik.Contracts.Service.Registers;
using KPMG.WebKik.Services.Registers;
using KPMG.WebKik.Services.Companies;

namespace KPMG.WebKik.Web
{
    public static class UnityConfig
    {
        public static IUnityContainer UseUnity(this HttpConfiguration config, string connectionName)
        {
            var container = BuildContainer(connectionName);
            config.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            return container;
        }

        private static IUnityContainer BuildContainer(string connectionName)
        {
            var container = new UnityContainer();

            container.RegisterType<DbContext>(new HierarchicalLifetimeManager(), new InjectionFactory(c => new WebKikDataContext(connectionName)));
            container.RegisterType<WebKikDataContext>(new HierarchicalLifetimeManager(), new InjectionFactory(c => new WebKikDataContext(connectionName)));

            RegisterRepositories(container);
            RegisterServices(container);
            RegisterCalculations(container);

            return container;
        }

        private static void RegisterRepositories(IUnityContainer container)
        {
            container.RegisterType(typeof(IEntityRepository<,>), typeof(EntityRepository<,>));
            container.RegisterType<IProjectCompanyRepository, ProjectCompanyRepository>();
            container.RegisterType<IProjectCompanyShareRepository, ProjectCompanyShareRepository>();
        }

        private static void RegisterServices(IUnityContainer container)
        {
            container.RegisterType(typeof(IBaseService), typeof(BaseService));
            container.RegisterType(typeof(IEntityService<,>), typeof(EntityService<,>));
            container.RegisterType(typeof(IProjectService), typeof(ProjectService));
            container.RegisterType(typeof(IProjectCompanyService), typeof(ProjectCompanyService));
            container.RegisterType(typeof(IProjectCompanyShareService), typeof(ProjectCompanyShareService));
            container.RegisterType(typeof(IUserService), typeof(UserService));
            container.RegisterType(typeof(IPermissionService), typeof(PermissionService));
            container.RegisterType(typeof(IRegisterService), typeof(RegisterService));
            container.RegisterType(typeof(IRegister1Service), typeof(Register1Service));
            container.RegisterType(typeof(IRegister2Service), typeof(Register2Service));
            container.RegisterType(typeof(IRegister3Service), typeof(Register3Service));
            container.RegisterType(typeof(IRegister4Service), typeof(Register4Service));
            container.RegisterType(typeof(IRegister5Service), typeof(Register5Service));
            container.RegisterType(typeof(IRegister6Service), typeof(Register6Service));
            container.RegisterType(typeof(IRegister7Service), typeof(Register7Service));
			container.RegisterType(typeof(IRegister8Service), typeof(Register8Service));
			container.RegisterType(typeof(IRegister9Service), typeof(Register9Service));
            container.RegisterType(typeof(IRegister10Service), typeof(Register10Service));
			container.RegisterType(typeof(IRegister11Service), typeof(Register11Service));
            container.RegisterType(typeof(ISignatoryService), typeof(SignatoryService));
            container.RegisterType(typeof(INotificationOfParticipationService), typeof(NotificationOfParticipationService));
            container.RegisterType(typeof(ITaxReturnService), typeof(TaxReturnService));
            container.RegisterType(typeof(INotificationOfKIKService), typeof(NotificationOfKIKService));
			container.RegisterType(typeof(ISupportingDocumentsService), typeof(SupportingDocumentsService));
		}

        private static void RegisterCalculations(IUnityContainer container)
        {
            container.RegisterType(typeof(IFactShareCalculation), typeof(FactShareCalculation));
            container.RegisterType(typeof(IControlCompanyCalculation), typeof(ControlCompanyCalculation));
            container.RegisterType(typeof(IKIKCompanyCalculation), typeof(KIKCompanyCalculation));
            container.RegisterType(typeof(INotificationOfParticipationCalculation), typeof(NotificationOfParticipationCalculation));
        }
    }
}