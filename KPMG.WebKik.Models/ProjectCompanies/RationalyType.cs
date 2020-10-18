using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPMG.WebKik.Models.ProjectCompanies
{
    public enum RationalyType : byte
    {
        [Description("Некоммерческая организация, которая в соответствии со своим личным законом не распределяет полученную прибыль (доход) между акционерами (участниками, учредителями) или иными лицами")]
        NonProfitOrganization,

        [Description("Оператор нового морского месторождения углеводородного сырья")]
        OffshoreFieldOperator,

        [Description("Непосредственный акционер (участник) оператора нового морского месторождения углеводородного сырья")]
        OffshoreFieldAuctioneer,

        [Description("Член ЕвразЭС")]
        EurAsECMember,

        [Description("Страхования организация, осуществляющая деятельность со своим личным законом, на основании лицензии или иного специального разрешения")]
        InsuranceAgencyWithLexPersonalis,

        [Description("Банк, осуществляющий деятельность в соответствии со своим личным законом, на основании лицензии или иного специального разрешения")]
        BankWithLexPersonalis,

        [Description("Эмитент обращающихся облигаций")]
        TradedBondsIssuer,

        [Description("Организация, которой были уступлены права и обязанности по выпущенным обращающимся облигациям")]
        CededRightsOrganization,

        [Description("Участник проектов по добыче полезных ископаемых, осуществляемых на основании заключаемых с иностранным государством (территорией) или с уполномоченными правительством такого государства (территории) организациями СРП, концессионных соглашений, лицензионных соглашений или сервисных соглашений (контрактов), аналогичных СРП, либо на основании иных аналогичных соглашений, заключаемых на условиях распределения риска")]
        ProjectMemberMining,

        [Description("По «ЭСНП»")]
        ByESPN,

        [Description("Активная иностранная компания")]
        ActiveForeignCompany,

        [Description("Активная холдинговая компания")]
        ActiveHoldingCompany,

        [Description("Активная субхолдинговая компания")]
        ActiveSubholdingCompany
    }
}
