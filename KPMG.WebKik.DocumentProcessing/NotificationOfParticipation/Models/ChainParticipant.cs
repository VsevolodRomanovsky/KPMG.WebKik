using System.Linq;

namespace KPMG.WebKik.DocumentProcessing.NotificationOfParticipation.Models
{
    internal class ChainParticipant
    {
        private readonly ChainParticipant previousParticipant;
        private double? directSharePart;
        private double? indirectSharePart;

        public ChainParticipant(ChainParticipant previousParticipant, NPReportCompany company)
        {
            this.previousParticipant = previousParticipant;
            Company = company;
        }

        public readonly NPReportCompany Company;

        public string CompanyNumber => Company.FullNumber;

        public double DirectSharePart
        {
            get
            {
                if (!directSharePart.HasValue)
                {
                    directSharePart = calculateDirectSharePart();
                }
                return directSharePart.Value;
            }
        }

        public double IndirectSharePart
        {
            get
            {
                if (!indirectSharePart.HasValue)
                {
                    indirectSharePart = calculateIndirectSharePart();
                }
                return indirectSharePart.Value;
            }
        }

        private double calculateDirectSharePart()
        {
            var ownerCompany = previousParticipant?.Company.ProjectCompany ?? Company.OwnerCompany;

            return ownerCompany
                        .OwnerProjectCompanyShares
                        .FirstOrDefault(x => x.DependentProjectCompanyId == Company.ProjectCompany.Id)
                        .SharePart;
        }

        private double calculateIndirectSharePart()
        {
            if (previousParticipant == null)
            {
                return 0;
            }

            var previousIndirectValue = previousParticipant.IndirectSharePart / 100;

            if (previousIndirectValue == 0)
            {
                previousIndirectValue = previousParticipant.DirectSharePart / 100;
            }

            var directValue = DirectSharePart / 100;
            return previousIndirectValue * directValue * 100;
        }
    }
}
