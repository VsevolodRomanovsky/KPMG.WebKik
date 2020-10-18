import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { SignatoryRoutingModule } from './signatory-routing.module';
import { SignatoryListComponent } from './signatory-list/signatory-list.component';
import { SignatoryEditComponent } from './signatory-edit/signatory-edit.component';
import { SignatoryOptionsComponent } from './signatory-options/signatory-options.component';
import { SignatoryService } from './signatory.service';
import { DirectoryModule } from '../directory/directory.module';
import { ProjectModule } from '../project/project.module'; 

@NgModule({
    imports: [
        SharedModule,
        SignatoryRoutingModule,
        DirectoryModule,
        ProjectModule
    ],
    declarations: [
        SignatoryListComponent,
        SignatoryEditComponent,
        SignatoryOptionsComponent
    ],
    providers: [
        SignatoryService
    ],
    exports: [
        SignatoryOptionsComponent
    ]
})
export class SignatoryModule { }
