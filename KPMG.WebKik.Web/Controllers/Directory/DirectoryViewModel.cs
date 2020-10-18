using System.Collections.Generic;
using KPMG.WebKik.Models.Directories;

namespace KPMG.WebKik.Web.Controllers.Directory
{
    public class DirectoryViewModel
    {
        public string Name { get; set; }
        public IEnumerable<IDirectoryEntry> Entries { get; set; }
    }
}