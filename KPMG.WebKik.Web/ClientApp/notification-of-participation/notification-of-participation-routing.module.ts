import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { NotificationOfParticipationListComponent } from './notification-of-participation-list/notification-of-participation-list.component';
import { NotificationOfParticipationEditComponent } from './notification-of-participation-edit/notification-of-participation-edit.component';
import { NotificationOfParticipationSupportingDocumentsComponent } from './notification-of-participation-supportingdocuments/notification-of-participation-supportingdocuments.component';

const routes: Routes = [
    {
        path: 'projects/:projectid/notificationsofparticipation',
        children: [
            { path: '', component: NotificationOfParticipationListComponent },
			{ path: ':notificationid', component: NotificationOfParticipationEditComponent },
			{ path: ':notificationid/documents', component: NotificationOfParticipationSupportingDocumentsComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class NotificationOfParticipationRoutingModule { }
