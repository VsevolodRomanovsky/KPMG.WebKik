using System;
using System.Collections;
using System.Collections.Generic;

namespace KPMG.WebKik.DocumentProcessing.Kik.Models
{
    internal class CompanyChain : IEnumerable<ChainParticipant>
    {
        public int Number { get; }
        public double IndirectSharePart
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public readonly IList<KikReportCompany> Companies;

        public CompanyChain(int number, IList<KikReportCompany> companies)
        {
            Number = number;
            Companies = companies;
        }

        public IEnumerator<ChainParticipant> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
