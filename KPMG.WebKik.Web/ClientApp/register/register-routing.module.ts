import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Register1Component } from './register1/register1.component'
import { Register2Component } from './register2/register2.component'
import { Register3Component } from './register3/register3.component'
import { Register4Component } from './register4/register4.component'
import { Register5Component } from './register5/register5.component'
import { Register6Component } from './register6/register6.component'
import { Register7Component } from './register7/register7.component'
import { Register8Component } from './register8/register8.component'
import { Register9Component } from './register9/register9.component'
import { Register10Component } from './register10/register10.component'
import { Register11Component } from './register11/register11.component'
import { RegisterListComponent } from './register-list/register-list.component'

const routes: Routes = [
    {
        path: 'projects/:projectid/companies/:companyid/share/:shareid/registers',
        children: [
            { path: '', component: RegisterListComponent },
            { path: 'register1/:registerid/:year', component: Register1Component },
            { path: 'register2/:registerid/:year', component: Register2Component },
            { path: 'register3/:registerid/:year', component: Register3Component },
            { path: 'register4/:registerid/:year', component: Register4Component },
            { path: 'register5/:registerid/:year', component: Register5Component },
            { path: 'register6/:registerid/:year', component: Register6Component },
            { path: 'register7/:registerid/:year', component: Register7Component },
            { path: 'register8/:registerid/:year', component: Register8Component },
			{ path: 'register9/:registerid/:year', component: Register9Component },
			{ path: 'register10/:registerid/:year', component: Register10Component },
			{ path: 'register11/:registerid/:year', component: Register11Component }
		]
	}
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class RegisterRoutingModule { }