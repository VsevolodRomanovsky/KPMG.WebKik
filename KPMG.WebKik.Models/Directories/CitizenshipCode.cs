using System.ComponentModel;

namespace KPMG.WebKik.Models.Directories
{
    [DisplayName("Данные о гражданстве")]
    public class CitizenshipCode : IDirectoryEntry
    {
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
    }
}
