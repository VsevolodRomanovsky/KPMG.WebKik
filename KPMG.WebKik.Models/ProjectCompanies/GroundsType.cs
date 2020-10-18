using System.ComponentModel;

namespace KPMG.WebKik.Models.ProjectCompanies
{
    public enum GroundsType : byte
    {
        [Description("учредитель")]
        Founder,

        [Description("лицо, осуществляющее контроль в отношении ИС")]
        Supervising,

        [Description("лицо, имеющее фактическое право на доход")]
        BeneficialIncomeOwner
    }
}
