using System.ComponentModel;

namespace KPMG.WebKik.Models.Directories
{
    [DisplayName("Коды лиц, имеющих право на доход")]
    public class IncomeEntitledPersonCode : IDirectoryEntry
    {
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
    }
}
