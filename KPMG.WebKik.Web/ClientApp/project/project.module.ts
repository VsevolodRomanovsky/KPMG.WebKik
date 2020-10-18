import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';

import { ProjectListComponent } from './project-list/project-list.component';
import { ProjectDetailComponent } from './project-detail/project-detail.component';
import { ProjectEditComponent } from './project-edit/project-edit.component';
import { ProjectOwnershipComponent } from './project-ownership/project-ownership.component';
import { ProjectInCompanyComponent } from './project-in-company/project-in-company.component'
import { ProjectService } from './project.service';
import { ProjectRoutingModule } from './project-routing.module';
import { UserModule } from '../user/user.module';

@NgModule({
    imports: [
        SharedModule,
        ProjectRoutingModule,
        UserModule
    ],
    declarations: [
        ProjectListComponent,
        ProjectDetailComponent,
        ProjectInCompanyComponent,
        ProjectEditComponent,
        ProjectOwnershipComponent
    ],
    providers: [ProjectService],
    exports: [ProjectInCompanyComponent]
})
export class ProjectModule { }
