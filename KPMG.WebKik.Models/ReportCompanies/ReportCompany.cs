using System;
using System.Collections.Generic;
using System.Linq;
using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.Models.ReportCompanies
{
    public class ReportCompany
    {
        private ProjectCompanyShare share;
        private ProjectCompanyFactShare factShare;
        private IEnumerable<ControlGround> controlGrounds;

        public readonly ProjectCompany ProjectCompany;
        public readonly ProjectCompany OwnerCompany;

        public readonly int Number;

        public ReportCompany(ProjectCompanyFactShare factShare, CompanyNumberContainer numbers)
        {
            ProjectCompany = factShare.DependentProjectCompany;
            OwnerCompany = factShare.OwnerProjectCompany;
            this.factShare = factShare;

            switch (ProjectCompany.State)
            {
                case State.Domestic:
                    Number = ++numbers.DomesticCompanyNumber;
                    break;
                case State.ForeignLight:
                    Number = ++numbers.ForeignLightCompanyNumber;
                    break;
                case State.Foreign:
                    Number = ++numbers.ForeignCompanyNumber;
                    break;
                case State.Individual:
                default:
                    throw new ArgumentException($"Expected Project Company State Domestic, Foreign, ForeignLight. Got {ProjectCompany.State}");
            }
        }

        public string FullName
        {
            get
            {
                switch (ProjectCompany.State)
                {
                    case State.Domestic:
                        return ProjectCompany.Name;
                    case State.ForeignLight:
                        return ProjectCompany.ForeignLightCompany.RussianName;
                    case State.Foreign:
                        return ProjectCompany.ForeignCompany.FullName;
                    case State.Individual:
                    default:
                        throw new ArgumentException($"Expected Project Company State Domestic, Foreign, ForeignLight. Got {ProjectCompany.State}");
                }
            }
        }

        public string FullNumber => GetFormattedNumber("D5");
        public string ShortNumber => GetFormattedNumber(string.Empty).Replace("-", string.Empty).ToLower();

        private string GetFormattedNumber(string format)
        {
            switch (ProjectCompany.State)
            {
                case State.Domestic:
                    return "РО-" + Number.ToString(format);
                case State.ForeignLight:
                    return "ИC-" + Number.ToString(format);
                case State.Foreign:
                    return "ИО-" + Number.ToString(format);
                case State.Individual:
                default:
                    throw new ArgumentException($"Expected Project Company State Domestic, Foreign, ForeignLight. Got {ProjectCompany.State}");
            }
        }

        public ProjectCompanyFactShare FactShare
        {
            get
            {
                return factShare;              
            }
        }

        public ShareType ShareType
        {
            get
            {
                if (factShare.ShareDirectPart > 0 && factShare.ShareFactPart > 0) return ShareType.Complex;
                if (factShare.ShareDirectPart > 0) return ShareType.Direct;
                return ShareType.Indirect;
            }
        }

        public IEnumerable<ControlGround> ControlGrounds
        {
            get
            {
                if (controlGrounds == null)
                {
                    controlGrounds = GetControlGrounds();
                }
                return controlGrounds;
            }
        }

        private IEnumerable<ControlGround> GetControlGrounds()
        {
            var grounds = new List<ControlGround>();
            if (!ProjectCompany.IsKIKCompany)
            {
                return grounds;
            }

            if (FactShare.ShareFactPart > 25)
            {
                grounds.Add(ControlGround._101);
            }

            var totalResidentShare = ProjectCompany.DependentProjectCompanyShares.Sum(x => x.ShareWithResidentsPart);
            if (FactShare.ShareFactPart > 10 && totalResidentShare > 50)
            {
                grounds.Add(ControlGround._102);
            }

            if (factShare.IsOwnInterest) grounds.Add(ControlGround._103);
            if (factShare.IsPartnerInterest) grounds.Add(ControlGround._104);
            if (factShare.IsChildInterest) grounds.Add(ControlGround._105);

            return grounds;
        }
    }
}
