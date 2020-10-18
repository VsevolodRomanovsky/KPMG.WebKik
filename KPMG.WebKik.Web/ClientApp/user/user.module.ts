import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { UserRoutingModule } from './user-routing.module';
import { UserInProjectComponent } from './user-in-project/user-in-project.component';
import { UserListComponent } from './user-list/user-list.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { UserService } from './user.service';
import { RoleService } from './role.service';

@NgModule({
    imports: [
        SharedModule,
        UserRoutingModule,
    ],
    declarations: [
        UserInProjectComponent,
        UserListComponent,
        UserEditComponent
    ],
    providers: [UserService, RoleService],
    exports: [
        UserInProjectComponent
    ]
})
export class UserModule { }
