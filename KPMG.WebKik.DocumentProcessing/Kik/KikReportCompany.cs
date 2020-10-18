using System;
using System.Collections.Generic;
using System.Linq;
using KPMG.WebKik.DocumentProcessing.Kik.Models;
using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.DocumentProcessing.Kik
{
    internal class KikReportCompany
    {
        private const string fullNumberFormat = "D5";
        private ProjectCompanyShare share;
        private bool isShareSearched;
        private ProjectCompanyFactShare factShare;
        private IEnumerable<ControlGround> controlGrounds;

        public readonly ProjectCompany ProjectCompany;
        public readonly ProjectCompany OwnerCompany;

        public readonly int Number;

        public KikReportCompany(ProjectCompany ownerCompany, ProjectCompany projectCompany, CompanyNumberContainer numbers)
        {
            ProjectCompany = projectCompany;
            OwnerCompany = ownerCompany;
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

        public string FullName => ProjectCompany.ForeignCompany?.FullName ?? ProjectCompany.ForeignLightCompany.RussianName;
        public int Id => ProjectCompany.ForeignCompany?.Id ?? ProjectCompany.ForeignLightCompany.Id;

        public string FullNumber => GetFormattedNumber(fullNumberFormat);
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

        public ProjectCompanyShare Share
        {
            get
            {
                if (!isShareSearched)
                {
                    share = ProjectCompany.DependentProjectCompanyShares.SingleOrDefault(x => x.OwnerProjectCompanyId == OwnerCompany.Id);
                    isShareSearched = true;
                }
                return share;
            }
        }

        public ProjectCompanyFactShare FactShare
        {
            get
            {
                return null;//
                // заменить нормальным решением
                //if (factShare == null)
                //{
                //    factShare = ProjectCompany.DependentProjectCompanyFactShares.Single(x => x.OwnerProjectCompanyId == OwnerCompany.Id);
                //}
                //return factShare;
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

            if (Share?.IsOwnInterest == true)
            {
                grounds.Add(ControlGround._103);
            }

            if (Share?.IsPartnerInterest == true)
            {
                grounds.Add(ControlGround._104);
            }

            if (Share?.IsChildInterest == true)
            {
                grounds.Add(ControlGround._105);
            }

            return grounds;
        }
    }
}
