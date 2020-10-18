import { NgModule} from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { RegisterRoutingModule } from './register-routing.module';
import { ProjectModule } from '../project/project.module';
import { CompanyModule } from '../company/company.module';
import { DirectoryModule } from '../directory/directory.module';
import { DataTableModule } from 'primeng/primeng';
import { Ng2Bs3ModalModule } from 'ng2-bs3-modal/ng2-bs3-modal';

import { Register1Component } from './register1/register1.component';
import { Register2Component } from './register2/register2.component';
import { Register3Component } from './register3/register3.component';
import { Register4Component } from './register4/register4.component';
import { Register5Component } from './register5/register5.component';
import { Register6Component } from './register6/register6.component';
import { Register7Component } from './register7/register7.component';
import { Register8Component } from './register8/register8.component';
import { Register9Component } from './register9/register9.component';
import { Register10Component } from './register10/register10.component';
import { Register11Component } from './register11/register11.component';
import { RegisterListComponent } from './register-list/register-list.component';

import { RegisterService } from './services/register.service'
import { Register1Service } from './services/register1.service'
import { Register2Service } from './services/register2.service'
import { Register3Service } from './services/register3.service'
import { Register4Service } from './services/register4.service'
import { Register5Service } from './services/register5.service'
import { Register6Service } from './services/register6.service'
import { Register7Service } from './services/register7.service'
import { Register8Service } from './services/register8.service'
import { Register9Service } from './services/register9.service'
import { Register10Service } from './services/register10.service'
import { Register11Service } from './services/register11.service'
import { FilterPipe1 } from "./register11/register11.pipe"
import { FilterPipe } from "./register10/register10.pipe"

@NgModule({
    imports: [
        SharedModule,
        RegisterRoutingModule,
		CompanyModule,
		DirectoryModule,
		ProjectModule,
		DataTableModule,
		Ng2Bs3ModalModule
	],
    declarations: [
        Register1Component,
        Register2Component,
        Register3Component,
        Register4Component,
        Register5Component,
        Register6Component,
        Register7Component,
		Register8Component,
        Register8Component,
		Register9Component,
        Register10Component,
		Register11Component,
        RegisterListComponent,
		FilterPipe1,
		FilterPipe
    ],
    providers: [
        RegisterService,
        Register1Service,
        Register2Service,
        Register3Service,
        Register4Service,
        Register5Service,
        Register6Service,
        Register7Service,
		Register8Service,
		Register9Service,
		Register10Service,
		Register11Service
    ]
})

export class RegisterModule { }
