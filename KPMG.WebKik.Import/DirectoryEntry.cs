using KPMG.WebKik.Models.Directories;

namespace KPMG.WebKik.Import
{
    public class DirectoryEntry : IDirectoryEntry
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Code1 { get; set; }
        public string Code2 { get; set; }
        public string FullName { get; set; }
    }
}
