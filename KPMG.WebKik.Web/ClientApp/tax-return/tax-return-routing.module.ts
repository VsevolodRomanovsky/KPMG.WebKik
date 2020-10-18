import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { TaxReturnListComponent } from './tax-return-list/tax-return-list.component';
import { TaxReturnEditComponent } from './tax-return-edit/tax-return-edit.component';
import { NotificationOfParticipationSupportingDocumentsComponent } from '../notification-of-participation/notification-of-participation-supportingdocuments/notification-of-participation-supportingdocuments.component';


const routes: Routes = [
    {
        path: 'projects/:projectid/taxreturn',
        children: [
            { path: '', component: TaxReturnListComponent },
			{ path: ':taxreturnid', component: TaxReturnEditComponent },
			{ path: ':taxreturnid/documents', component: NotificationOfParticipationSupportingDocumentsComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class TaxReturnRoutingModule { }
