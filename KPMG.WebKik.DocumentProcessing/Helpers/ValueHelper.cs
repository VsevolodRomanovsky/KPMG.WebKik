using KPMG.WebKik.Models.ProjectCompanies;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace KPMG.WebKik.DocumentProcessing.Helpers
{
    internal static class ValueHelper
    {
        public static string FormatCode(this string code, string format)
        {
            return int.Parse(code).ToString(format);
        }

        public static Int64 ToInt(this double value)
        {
            return (Int64)value;
        }

        public static string GetNumbersAfterDot(this double value, int digits)
        {
            var stringValue = value.ToString(CultureInfo.InvariantCulture);
            var dotIndex = stringValue.IndexOf('.');
            if (dotIndex < 0)
            {
                return new string('0', digits);
            }

            var result = stringValue.Substring(dotIndex + 1);
            var additionalZeros = new string('0', Math.Max(digits - result.Length, 0));
            return result + additionalZeros;
        }

        public static ProjectCompany GetCompany(this int id, IEnumerable<ProjectCompany> companies)
        {
            return companies.Single(x => x.Id == id);
        }

        public static IEnumerable<ProjectCompanyShare> GetShares(this ProjectCompanyFactShare factShare, IEnumerable<ProjectCompany> companies)
        {
            return factShare.OwnerProjectCompanyId
                            .GetCompany(companies)
                            .OwnerProjectCompanyShares
                            .Where(x =>
                                x.OwnerProjectCompanyId == factShare.OwnerProjectCompanyId &&
                                x.DependentProjectCompanyId == factShare.DependentProjectCompanyId);
        }
    }
}
