import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { ProjectModule } from '../project/project.module'; 
import { CompanyModule } from '../company/company.module';
import { DirectoryModule } from '../directory/directory.module';
import { SignatoryModule } from '../signatory/signatory.module';

import { NotificationOfParticipationRoutingModule } from './notification-of-participation-routing.module';
import { NotificationOfParticipationListComponent } from './notification-of-participation-list/notification-of-participation-list.component';
//import { NotificationOfParticipationSupportingDocumentsComponent } from './notification-of-participation-supportingdocuments/notification-of-participation-supportingdocuments.component';
import { NotificationOfParticipationService } from './notification-of-participation.service';
import { NotificationOfParticipationEditComponent } from './notification-of-participation-edit/notification-of-participation-edit.component';

@NgModule({
    imports: [
        SharedModule,
        ProjectModule,
        CompanyModule,
        SignatoryModule,
        DirectoryModule,
        NotificationOfParticipationRoutingModule
    ],
    declarations: [
        NotificationOfParticipationListComponent,
		NotificationOfParticipationEditComponent,
//		NotificationOfParticipationSupportingDocumentsComponent
    ],
    providers: [
        NotificationOfParticipationService],
    exports: [
    ]
})
export class NotificationOfParticipationModule { }
