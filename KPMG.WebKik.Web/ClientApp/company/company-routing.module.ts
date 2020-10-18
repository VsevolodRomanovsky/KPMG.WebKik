import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CompanyListComponent } from './company-list/company-list.component';
import { CompanyDetailComponent } from './company-detail/company-detail.component';
import { CompanyCreateComponent } from './company-create/company-create.component';
import { DomesticCompanyCreateComponent } from './company-create/domestic-company-create.component';
import { ForeignCompanyCreateComponent } from './company-create/foreign-company-create.component';
import { ForeignLightCompanyCreateComponent } from './company-create/foreignlight-company-create.component';
import { IndividualCompanyCreateComponent } from './company-create/individual-company-create.component';
import { DomesticCompanyEditComponent } from './company-edit/domestic-company-edit.component';
import { ForeignCompanyEditComponent } from './company-edit/foreign-company-edit.component';
import { ForeignCompanyLightEditComponent } from './company-edit/foreignlight-company-edit.component';
import { IndividualCompanyEditComponent } from './company-edit/individual-company-edit.component';
import { CompanyShareCreateComponent } from './company-share-create/company-share-create.component';
import { CompanyShareEditComponent } from './company-share-edit/company-share-edit.component';
import { CompanyTaxExemptionComponent } from './company-tax-exemption/company-tax-exemption.component'
import { CompanyTaxExemptionListComponent } from './company-tax-exemption-list/company-tax-exemption-list.component'
import { CompanyShareOwnershipComponent } from './company-share-ownership/company-share-ownership.component'
import { CompanyKikListComponent } from './kik-company-list/kik-company-list.component'

const routes: Routes = [
    {
        path: 'projects/:projectid/companies',
        children: [
            { path: '', component: CompanyListComponent },
            { path: 'create', component: CompanyCreateComponent },
            { path: 'create/domestic', component: DomesticCompanyCreateComponent },
            { path: 'create/foreign', component: ForeignCompanyCreateComponent },
            { path: 'create/foreignlight', component: ForeignLightCompanyCreateComponent },
            { path: 'create/individual', component: IndividualCompanyCreateComponent },
            { path: ':companyid', component: CompanyDetailComponent },
            { path: ':companyid/edit/domestic', component: DomesticCompanyEditComponent },
            { path: ':companyid/edit/foreign', component: ForeignCompanyEditComponent },
            { path: ':companyid/edit/foreignlight', component: ForeignCompanyLightEditComponent },
            { path: ':companyid/edit/individual', component: IndividualCompanyEditComponent },
            { path: ':companyid/share/create', component: CompanyShareCreateComponent },
            { path: ':companyid/share/:shareid/edit', component: CompanyShareEditComponent },
            { path: ':companyid/share/:shareid/taxexemption', component: CompanyTaxExemptionComponent },
            { path: ':companyid/taxexemptionlist', component: CompanyTaxExemptionListComponent },
            { path: ':companyid/share', component: CompanyShareOwnershipComponent },
            { path: ':companyid/kiklist', component: CompanyKikListComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class CompanyRoutingModule { }
