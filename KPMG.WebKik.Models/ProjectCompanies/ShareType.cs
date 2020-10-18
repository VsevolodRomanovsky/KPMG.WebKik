using System.ComponentModel;

namespace KPMG.WebKik.Models.ProjectCompanies
{
    public enum ShareType: byte
    {
        [Description("Прямое")]
        Direct,

        [Description("Косвенное")]
        Indirect,

        [Description("Смешанное")]
        Complex,
    }
}