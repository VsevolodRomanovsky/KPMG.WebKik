import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProjectListComponent } from './project-list/project-list.component';
import { ProjectDetailComponent } from './project-detail/project-detail.component';
import { ProjectEditComponent } from './project-edit/project-edit.component';
import { ProjectOwnershipComponent } from './project-ownership/project-ownership.component';

const routes: Routes = [
    {
        path: 'projects',
        children: [
            { path: '', component: ProjectListComponent },
            { path: ':projectid', component: ProjectDetailComponent },
            { path: ':projectid/edit', component: ProjectEditComponent },
            { path: ':projectid/ownership', component: ProjectOwnershipComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ProjectRoutingModule { }
