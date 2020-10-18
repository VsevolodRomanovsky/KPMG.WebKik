using System.ComponentModel;

namespace KPMG.WebKik.Models.Directories
{
    [DisplayName("Коды учредителей иностранных структур")]
    public class ForeignStructuresFoundersCode : IDirectoryEntry
    {
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
    }
}
