using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra.Double;
using KPMG.WebKik.Contracts.Algorithms;
using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.Algorithms
{
    public class FactShareCalculation : IFactShareCalculation
    {
        public IList<ProjectCompanyFactShare> GetFactShares(IEnumerable<ProjectCompanyShare> companyShares)
        {
            var orderedIds = GetOrderedIds(companyShares);
            if (orderedIds.Count() == 0)
            {
                return new List<ProjectCompanyFactShare>();

            }
            var result = GetCalcualtedFactParts(orderedIds, companyShares);
            return result.Where(x => x.ShareFactPart > 0).ToList();
        }

        private ICollection<ProjectCompanyFactShare> GetCalcualtedFactParts(IList<int> orderedIds, IEnumerable<ProjectCompanyShare> shares)
        {
            var factShareList = new List<ProjectCompanyFactShare>();
            var level = orderedIds.Count();
            var cstMatrix = new DenseMatrix(level);

            var shareLookup = shares.ToLookup(x => x.OwnerProjectCompanyId);

            foreach (var share in shares)
            {
                cstMatrix[orderedIds.IndexOf(share.OwnerProjectCompanyId), orderedIds.IndexOf(share.DependentProjectCompanyId)] += share.SharePart / 100;
            }

            var oneMatrix = DenseMatrix.CreateIdentity(level);
            var minusMatrix = oneMatrix - cstMatrix;
            var inverseMatrix = minusMatrix.Inverse();

            for (var y = 0; y < inverseMatrix.RowCount; y++)
            {
                for (var x = 0; x < inverseMatrix.ColumnCount; x++)
                {
                    if (y != x)
                    {
                        var directShares = shareLookup[orderedIds[y]].Where(q => q.DependentProjectCompanyId == orderedIds[x]);

                        var factShare = new ProjectCompanyFactShare
                        {
                            OwnerProjectCompanyId = orderedIds[y],
                            OwnerProjectCompany = shareLookup[orderedIds[y]].FirstOrDefault()?.OwnerProjectCompany,
                            DependentProjectCompanyId = orderedIds[x],
                            DependentProjectCompany = shares.FirstOrDefault(q => q.DependentProjectCompanyId == orderedIds[x])?.DependentProjectCompany,
                            ShareFactPart = inverseMatrix[y, x] * 100,
                            ShareDirectPart = directShares.Sum(q => q.SharePart),
                            IsControlledBy = directShares.Any(q => true.Equals(q.IsControlledBy)),
                            IsFounder = directShares.Any(q => true.Equals(q.IsFounder)),
                            IsOwnInterest = directShares.Any(q => true.Equals(q.IsOwnInterest)),
                            IsPartnerInterest = directShares.Any(q => true.Equals(q.IsPartnerInterest)),
                            IsChildInterest = directShares.Any(q => true.Equals(q.IsChildInterest)),
                            DirectShares = directShares.ToArray(),
                        };
                        factShareList.Add(factShare);
                    }
                }
            }
            return factShareList;

        }

        private IList<int> GetOrderedIds(IEnumerable<ProjectCompanyShare> shares)
        {
            var list = new List<int>();
            foreach (var share in shares)
            {
                list.Add(share.OwnerProjectCompanyId);
                list.Add(share.DependentProjectCompanyId);
            }
            list = list.Distinct().ToList();
            list.Sort();
            return list;
        }
    }
}
