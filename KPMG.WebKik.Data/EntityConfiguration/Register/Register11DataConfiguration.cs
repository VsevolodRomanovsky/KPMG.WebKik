using System.Data.Entity.ModelConfiguration;
using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Data.EntityConfiguration.Register
{
	public class Register11DataConfiguration : EntityTypeConfiguration<Register11Data>
	{
		public Register11DataConfiguration()
		{
			ToTable("Register11Data");
			HasKey(r => r.Id);
			Property(r => r.Register11DataTypeId).IsRequired();
			Property(r => r.IncomeFromRealizationOfAssetSummary).IsRequired();
			Property(r => r.IncomeFromRealizationOfAssetSellPrice).IsRequired();
			Property(r => r.IncomeFromRealizationOfAssetOthers).IsRequired();
			Property(r => r.MarketValue).IsRequired();
			Property(r => r.CostForTransitionOfPropertyRightDateSummary).IsRequired();
			Property(r => r.CostForTransitionOfPropertyRightDateAcquisitionPrice).IsRequired();
			Property(r => r.CostForTransitionOfPropertyRightDateRevaluationSummary).IsRequired();
			Property(r => r.CostForTransitionOfPropertyRightDateRevaluationForCurrentYear).IsRequired();

			HasRequired(x => x.Register11).WithMany(x => x.Register11Data).HasForeignKey(x => x.Register11Id);
		}
	}
}
