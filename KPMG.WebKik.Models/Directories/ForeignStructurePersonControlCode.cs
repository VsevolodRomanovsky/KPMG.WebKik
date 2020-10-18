using System.ComponentModel;

namespace KPMG.WebKik.Models.Directories
{
    [DisplayName("Коды лиц, осуществляющих контроль над иностранной структурой")]
    public class ForeignStructurePersonControlCode : IDirectoryEntry
    {
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
    }
}
