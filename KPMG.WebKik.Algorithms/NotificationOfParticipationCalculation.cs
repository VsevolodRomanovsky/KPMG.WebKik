using KPMG.WebKik.Contracts.Algorithms;
using KPMG.WebKik.Models.ProjectCompanies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KPMG.WebKik.Algorithms
{
    public class NotificationOfParticipationCalculation : INotificationOfParticipationCalculation
    {
        public bool CalculateIsNotificationOfParticipationRequired(
            ProjectCompanyFactShare factShare
            )
        {
            var owner = factShare.OwnerProjectCompany;
            var dependent = factShare.DependentProjectCompany;

            //CheckDependent(dependent);

            if (owner.State == State.Foreign || owner.State == State.ForeignLight || dependent.State == State.Domestic) return false;
           
            if (factShare.IsFounder || factShare.IsControlledBy) return true;
        
            if (factShare.ShareFactPart > 10)return true;
           

            return false;
        }
        
        private void CheckDependent(ProjectCompany dependent)
        {
            if (dependent.State == State.Individual)
            {
                throw new ArgumentException($"Dependent Project Company can not be Individual. Dependent Id = {dependent.Id}");
            }
        }

    }
}
