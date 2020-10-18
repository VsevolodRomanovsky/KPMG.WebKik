using System.ComponentModel;

namespace KPMG.WebKik.Models.Directories
{
    [DisplayName("Коды стран ЕвразЭС")]
    public class EAECCountryCode : IDirectoryEntry
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
