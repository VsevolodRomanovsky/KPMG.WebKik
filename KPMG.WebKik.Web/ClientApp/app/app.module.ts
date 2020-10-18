import { NgModule, LOCALE_ID } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './navmenu/navmenu.component';
import { AuthErrorComponent } from './auth-error/auth-error.component';

import { ProjectModule } from '../project/project.module';
import { CompanyModule } from '../company/company.module';
import { RegisterModule } from '../register/register.module';
import { UserModule } from '../user/user.module';
import { DirectoryModule } from '../directory/directory.module';
import { SignatoryModule } from '../signatory/signatory.module';
import { NotificationOfParticipationModule } from '../notification-of-participation/notification-of-participation.module';
import { TaxReturnModule } from '../tax-return/tax-return.module';
import { NotificationOfKIKModule } from '../notification-of-kik/notification-of-kik.module';
import { SharedModule } from '../shared/shared.module';

import { AppRoutingModule } from './app-routing.module';
import { DataTableModule, DialogModule, ButtonModule } from 'primeng/primeng';
import { Ng2Bs3ModalModule } from 'ng2-bs3-modal/ng2-bs3-modal';

@NgModule({
    imports: [
        BrowserModule,
        UserModule,
        DirectoryModule,
        SignatoryModule,
        ProjectModule,
        AppRoutingModule,
        SharedModule,
        RegisterModule,
        NotificationOfParticipationModule,
        TaxReturnModule,
        NotificationOfKIKModule,
		CompanyModule,
		DataTableModule,
		DialogModule,
		Ng2Bs3ModalModule
    ],
    declarations: [
        AppComponent,
        NavMenuComponent,
        AuthErrorComponent],
    providers: [
        { provide: LOCALE_ID, useValue: "ru-RU" }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }

