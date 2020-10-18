using System.Collections.Generic;
using System.ComponentModel;

namespace KPMG.WebKik.Models.Directories
{
    [DisplayName("Коды подписантов")]
    public class SignatoryCode : IDirectoryEntry
    {
        public SignatoryCode()
        {
            Signatories = new HashSet<Signatory>();
        }
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public ICollection<Signatory> Signatories { get; set; }
    }
}
