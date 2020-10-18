using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using KPMG.Webkik.Utils;
using KPMG.WebKik.Contracts.Service;
using KPMG.WebKik.Models.Directories;

namespace KPMG.WebKik.Web.Controllers.Directory
{
    [Authorize(Roles = Models.Role.Administrator)]
    [RoutePrefix("api/directories")]
    public class DirectoryController : BaseController
    {
        protected dynamic DynamicService;
        public DirectoryController(IBaseService service) : base(service)
        { }

        [HttpGet, Route("")]
        public IList<KeyValuePair<string, string>> GetDirectoryList()
        {
            var directoryEntryTypes = GetDirectoryEntryTypes();
            var result = new List<KeyValuePair<string, string>>();
            foreach (var directoryEntryType in directoryEntryTypes)
            {
                var attribute = (DisplayNameAttribute)Attribute.GetCustomAttribute(directoryEntryType, typeof(DisplayNameAttribute));
                result.Add(new KeyValuePair<string, string>(directoryEntryType.Name, attribute.DisplayName));
            }
            return result;
        }

        [HttpGet, Route("{directory}")]
        public async Task<DirectoryViewModel> GetByName(string directory)
        {
            var directoryEntryType = GetDirectoryEntryTypes().Single(x => x.Name == directory);
            DynamicService = GetService(directoryEntryType);
            DynamicService.ReloadDataContext();
            var serviceResult = await DynamicService.GetAll();
            var entries = new List<IDirectoryEntry>();
            foreach (var item in serviceResult)
            {
                entries.Add(ToDirectoryEntry(directoryEntryType, item));
            }
            var directoryEntryTypeName = GetDirectoryList().Single(x => x.Key == directoryEntryType.Name).Value;
            return new DirectoryViewModel { Name = directoryEntryTypeName, Entries = entries };
        }

        [HttpPost, Route("{directory}")]
        public virtual async Task<IDirectoryEntry> Create(string directory, [FromBody]DirectoryEntryViewModel model)
        {
            var directoryEntryType = GetDirectoryEntryTypes().Single(x => x.Name == directory);
            DynamicService = GetService(directoryEntryType);
            var entity = ToDirectoryEntry(directoryEntryType, model);
            MethodInfo createMethodInfo = DynamicService.GetType().GetMethod("Create");
            var task = createMethodInfo.Invoke(DynamicService, new object[] { entity });
            var serviceResult = await task;
            return ToDirectoryEntry(directoryEntryType, serviceResult);
        }

        public IList<Type> GetDirectoryEntryTypes()
        {
            var assemblies = new[] { typeof(IDirectoryEntry).Assembly };
            return ReflectionHelper.GetTypeByInterface<IDirectoryEntry>(assemblies).ToList();
        }

        private dynamic GetService(Type directoryEntryType)
        {
            var genericServiceType = typeof(IEntityService<,>);
            var serviceType = genericServiceType.MakeGenericType(directoryEntryType, typeof(int));
            return ControllerContext.Configuration.DependencyResolver.GetService(serviceType) as dynamic;
        }

        private IDirectoryEntry ToDirectoryEntry(Type directoryEntryType, dynamic entry)
        {
            var directoryEntry = (IDirectoryEntry)Activator.CreateInstance(directoryEntryType);
            directoryEntry.Id = entry.Id;
            directoryEntry.Code = entry.Code;
            directoryEntry.Name = entry.Name;
            return directoryEntry;
        }
    }
}
