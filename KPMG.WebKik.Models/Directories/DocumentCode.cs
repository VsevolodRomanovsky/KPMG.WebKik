using System.Collections.Generic;
using System.ComponentModel;

namespace KPMG.WebKik.Models.Directories
{
    [DisplayName("Коды документов")]
    public class DocumentCode : IDirectoryEntry
    {
        public DocumentCode()
        {
            DocumentInformationts = new HashSet<DocumentInformation>();
            Signatories = new HashSet<Signatory>();
        }
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public ICollection<DocumentInformation> DocumentInformationts { get; set; }
        public ICollection<Signatory> Signatories { get; set; }
    }
}
