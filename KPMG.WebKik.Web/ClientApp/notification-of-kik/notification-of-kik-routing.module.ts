import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { NotificationOfKIKListComponent } from './notification-of-kik-list/notification-of-kik-list.component';
import { NotificationOfKIKEditComponent } from './notification-of-kik-edit/notification-of-kik-edit.component';
import { NotificationOfParticipationSupportingDocumentsComponent } from '../notification-of-participation/notification-of-participation-supportingdocuments/notification-of-participation-supportingdocuments.component';


const routes: Routes = [
    {
        path: 'projects/:projectid/notificationofkik',
        children: [
            { path: '', component: NotificationOfKIKListComponent },
            { path: ':notificationofkikid', component: NotificationOfKIKEditComponent },
            { path: ':notificationofkikid/documents', component: NotificationOfParticipationSupportingDocumentsComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class NotificationOfKIKRoutingModule { }