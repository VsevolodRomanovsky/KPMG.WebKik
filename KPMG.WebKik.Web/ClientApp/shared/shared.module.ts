import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from "@angular/http";

import { PreloaderComponent } from './preloader/preloader.component';
import { OwnershipGraphComponent } from './ownership-graph/ownership-graph.component';
import { BaseService } from './base.service';
import { PermissionService } from './permission/permission.service';
import { DateValueAccessor } from './date-value-accessor/date-value-accessor';
import { DateDirective } from './date.directive';
import { NotificationOfParticipationSupportingDocumentsComponent } from '../notification-of-participation/notification-of-participation-supportingdocuments/notification-of-participation-supportingdocuments.component';


@NgModule({
    imports: [CommonModule],
    declarations: [
        PreloaderComponent,
        OwnershipGraphComponent,
        DateValueAccessor,
		DateDirective,
		NotificationOfParticipationSupportingDocumentsComponent],
    providers: [
        BaseService,
        PermissionService
    ],
    exports: [
        CommonModule,
        FormsModule,
        HttpModule,

        PreloaderComponent,
        OwnershipGraphComponent,
        DateValueAccessor,
        DateDirective
    ]
})
export class SharedModule { }
