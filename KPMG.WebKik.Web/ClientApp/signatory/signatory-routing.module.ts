import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SignatoryListComponent } from './signatory-list/signatory-list.component';
import { SignatoryEditComponent } from './signatory-edit/signatory-edit.component';

const routes: Routes = [
    {
        path: 'projects/:projectid/companies/:companyid/signatories',
        children: [
            { path: '', component: SignatoryListComponent },
            { path: ':signatoryid', component: SignatoryEditComponent },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class SignatoryRoutingModule { }
