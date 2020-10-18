import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DirectoryListComponent } from './directory-list/directory-list.component';
import { DirectoryEditComponent } from './directory-edit/directory-edit.component';
import { DirectoryAuthService } from './directory-auth.service';

const routes: Routes = [
    {
        path: 'directories',
        canActivate: [DirectoryAuthService],
        children: [
            { path: ':directory', component: DirectoryEditComponent, canActivateChild: [DirectoryAuthService] },
            { path: '', component: DirectoryListComponent, canActivateChild: [DirectoryAuthService] },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class DirectoryRoutingModule { }
