import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { DirectoryRoutingModule } from './directory-routing.module';
import { DirectoryListComponent } from './directory-list/directory-list.component';
import { DirectoryEditComponent } from './directory-edit/directory-edit.component';
import { DirectoryOptionsComponent } from './directory-options/directory-options.component';
import { DirectoryAuthService } from './directory-auth.service';
import { DirectoryService } from './directory.service';

@NgModule({
    imports: [
        SharedModule,
        DirectoryRoutingModule,
    ],
    declarations: [
        DirectoryListComponent,
        DirectoryEditComponent,
        DirectoryOptionsComponent,
    ],
    providers: [DirectoryAuthService, DirectoryService],
    exports: [
        DirectoryOptionsComponent
    ]
})
export class DirectoryModule { }
