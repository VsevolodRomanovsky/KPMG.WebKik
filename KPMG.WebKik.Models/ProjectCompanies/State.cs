using System.ComponentModel;

namespace KPMG.WebKik.Models.ProjectCompanies
{
    public enum State: byte
    {
        [Description("Российская Организация")]
        Domestic,

        [Description("Иностранная структура без образования ЮЛ")]
        ForeignLight,

        [Description("Иностранная Организация")]
        Foreign,

        [Description("Физическое Лицо")]
        Individual
    }
}