import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Register2ViewModel } from '../models/register2.model';
import { RegisterType } from '../models/register-type.model';

import { Register2Service } from '../services/register2.service'

@Component({
    template: require('./register2.component.html'),
    providers: []
})
export class Register2Component implements OnInit {
    model: Register2ViewModel;
    projectId: number;
    companyId: number;
    shareId: number;
    registerId: number;
    year: number;
    isLoading: boolean;
    isNew: boolean;

    constructor(private route: ActivatedRoute, private router: Router, private registerService: Register2Service) { }

    ngOnInit(): void {
        this.isLoading = true;

        this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);
        this.companyId = parseInt(this.route.snapshot.params['companyid'], 10);
        this.shareId = parseInt(this.route.snapshot.params['shareid'], 10);
        this.year = parseInt(this.route.snapshot.params['year'], 10);
        this.registerId = parseInt(this.route.snapshot.params['registerid'], 10);

        if (this.registerId == 0) {
            this.model = new Register2ViewModel();
            this.model.Year = this.year;
            this.model.OwnerProjectCompanyId = this.shareId;
            this.model.Type = RegisterType.Register2;
            this.isNew = true;
            this.isLoading = false;
            return;
        }

        this.isNew = false;
        this.registerService.getRegisterById(this.registerId).then(result => {
            this.isLoading = false;
            this.model = result;
        });
    }

    Save(): void {
        this.isLoading = true;

        if (this.isNew) {
            this.registerService.createRegister(this.model).then(result => {
                this.isLoading = false;
            });
        }
        else {
            this.registerService.updateRegister(this.model).then(result => {
                this.isLoading = false;
            });
        }

        this.router.navigate(['../../../'], { relativeTo: this.route });
    }

    Calculate(): void {
        this.registerService.calculateRegister(this.model).then(result => {
            this.model = result;
        });
    }
}