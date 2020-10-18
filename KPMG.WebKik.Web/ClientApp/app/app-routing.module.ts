import { NgModule }     from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthErrorComponent } from './auth-error/auth-error.component';

const routes: Routes = [
    { path: 'error/auth', component: AuthErrorComponent },
    { path: '', redirectTo: '/projects', pathMatch: 'full' },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }


