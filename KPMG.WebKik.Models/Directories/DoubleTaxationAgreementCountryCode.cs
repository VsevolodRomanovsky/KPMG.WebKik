using System.ComponentModel;

namespace KPMG.WebKik.Models.Directories
{
    [DisplayName("Коды стран СИДН")]
    public class DoubleTaxationAgreementCountryCode : IDirectoryEntry
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
