using KPMG.WebKik.Models.ProjectCompanies;
using System.Collections.Generic;

namespace KPMG.WebKik.Contracts.Algorithms
{
    public interface INotificationOfParticipationCalculation
    {
        bool CalculateIsNotificationOfParticipationRequired(ProjectCompanyFactShare factShare);
    }
}