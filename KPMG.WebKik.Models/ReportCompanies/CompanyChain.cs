using System;
using System.Collections.Generic;
using System.Linq;

namespace KPMG.WebKik.Models.ReportCompanies
{
    public class CompanyChain
    {
        private IEnumerable<ChainParticipant> participants;
        private readonly IList<ReportCompany> companies;

        public CompanyChain(int number, IList<ReportCompany> companies)
        {
            Number = number;
            this.companies = companies;
        }

        public ReportCompany TargetCompany => companies.Last();

        public readonly int Number;

        public double IndirectSharePart
        {
            get
            {
                return Participants.Last().IndirectSharePart;
            }
        }
        public IEnumerable<ChainParticipant> Participants
        {
            get
            {
                if (participants == null)
                {
                    participants = GetParticipants();
                }
                return participants;
            }
        }

        private IEnumerable<ChainParticipant> GetParticipants()
        {
            var participants = new List<ChainParticipant>();

            var participant = new ChainParticipant(null, companies.First());
            participants.Add(participant);

            foreach (var company in companies.Skip(1))
            {
                participant = new ChainParticipant(participant, company);
                participants.Add(participant);
            }

            return participants;
        }

    }
}
