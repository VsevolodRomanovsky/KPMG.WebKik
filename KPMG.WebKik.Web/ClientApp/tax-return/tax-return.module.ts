import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { ProjectModule } from '../project/project.module';
import { CompanyModule } from '../company/company.module';
import { DirectoryModule } from '../directory/directory.module';
import { SignatoryModule } from '../signatory/signatory.module';

import { TaxReturnRoutingModule } from './tax-return-routing.module';
import { TaxReturnListComponent } from './tax-return-list/tax-return-list.component';
import { TaxReturnService } from './tax-return.service';
import { TaxReturnEditComponent } from './tax-return-edit/tax-return-edit.component';
//import { NotificationOfParticipationSupportingDocumentsComponent } from '../notification-of-participation/notification-of-participation-supportingdocuments/notification-of-participation-supportingdocuments.component';


@NgModule({
    imports: [
        SharedModule,
        ProjectModule,
        CompanyModule,
        SignatoryModule,
        DirectoryModule,
        TaxReturnRoutingModule
    ],
    declarations: [
        TaxReturnListComponent,
		TaxReturnEditComponent,
//		NotificationOfParticipationSupportingDocumentsComponent
    ],
    providers: [
        TaxReturnService],
    exports: [
    ]
})
export class TaxReturnModule { }