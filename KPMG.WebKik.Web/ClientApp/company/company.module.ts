import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { ProjectModule } from '../project/project.module'; 
import { DirectoryModule } from '../directory/directory.module';
import { CompanyRoutingModule } from './company-routing.module';

import { CompanyListComponent } from './company-list/company-list.component';
import { CompanyDetailComponent } from './company-detail/company-detail.component';
import { CompanyCreateComponent } from './company-create/company-create.component';
import { DomesticCompanyCreateComponent } from './company-create/domestic-company-create.component';
import { ForeignCompanyCreateComponent } from './company-create/foreign-company-create.component';
import { ForeignLightCompanyCreateComponent } from './company-create/foreignlight-company-create.component';
import { IndividualCompanyCreateComponent } from './company-create/individual-company-create.component';
import { CompanyShareCreateComponent } from './company-share-create/company-share-create.component';
import { CompanyShareEditComponent } from './company-share-edit/company-share-edit.component';
import { DomesticCompanyEditComponent } from './company-edit/domestic-company-edit.component';
import { ForeignCompanyEditComponent } from './company-edit/foreign-company-edit.component';
import { ForeignCompanyLightEditComponent } from './company-edit/foreignlight-company-edit.component';
import { IndividualCompanyEditComponent } from './company-edit/individual-company-edit.component';
import { CompanyTaxExemptionComponent } from './company-tax-exemption/company-tax-exemption.component'
import { CompanyTaxExemptionListComponent } from './company-tax-exemption-list/company-tax-exemption-list.component'
import { CompanyInRegisterComponent } from './company-in-register/company-in-register.component'
import { CompanyShareOwnershipComponent } from './company-share-ownership/company-share-ownership.component'
import { CompanyKikListComponent } from './kik-company-list/kik-company-list.component'
import { NotificationCompaniesOptionsComponent } from './notification-companies-options/notification-companies-options.component'

import { CompanyService, TransferDataService } from './company.service'

@NgModule({
    imports: [
        SharedModule,
        CompanyRoutingModule,
        DirectoryModule,
        ProjectModule],
    declarations: [
        CompanyInRegisterComponent,
        CompanyListComponent,
        CompanyDetailComponent,
        CompanyCreateComponent,
        DomesticCompanyCreateComponent,
        ForeignCompanyCreateComponent,
        ForeignLightCompanyCreateComponent,
        IndividualCompanyCreateComponent,
        DomesticCompanyEditComponent,
        ForeignCompanyEditComponent,
        ForeignCompanyLightEditComponent,
        IndividualCompanyEditComponent,
        CompanyShareEditComponent,
        CompanyShareCreateComponent,
        CompanyTaxExemptionComponent,
        CompanyShareOwnershipComponent,
        CompanyTaxExemptionListComponent,
        CompanyKikListComponent,
        NotificationCompaniesOptionsComponent
    ],
    providers: [CompanyService, TransferDataService],
    exports: [
        CompanyInRegisterComponent,
        NotificationCompaniesOptionsComponent
    ]

})
export class CompanyModule { }
