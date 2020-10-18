import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { ProjectModule } from '../project/project.module';
import { CompanyModule } from '../company/company.module';
import { DirectoryModule } from '../directory/directory.module';
import { SignatoryModule } from '../signatory/signatory.module';

import { NotificationOfKIKRoutingModule } from './notification-of-kik-routing.module';
import { NotificationOfKIKListComponent } from './notification-of-kik-list/notification-of-kik-list.component';
import { NotificationOfKIKService } from './notification-of-kik.service';
import { NotificationOfKIKEditComponent } from './notification-of-kik-edit/notification-of-kik-edit.component';

@NgModule({
    imports: [
        SharedModule,
        ProjectModule,
        CompanyModule,
        SignatoryModule,
        DirectoryModule,
        NotificationOfKIKRoutingModule
    ],
    declarations: [
        NotificationOfKIKListComponent,
        NotificationOfKIKEditComponent
    ],
    providers: [
        NotificationOfKIKService],
    exports: [
    ]
})
export class NotificationOfKIKModule { }