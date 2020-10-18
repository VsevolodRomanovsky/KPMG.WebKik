import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Register5ViewModel } from '../models/register5.model';
import { RegisterType } from '../models/register-type.model';

import { Register5Service } from '../services/register5.service'

@Component({
    template: require('./register5.component.html'),
    providers: []
})
export class Register5Component implements OnInit {
    model: Register5ViewModel;
    projectId: number;
    companyId: number;
    registerId: number;
    shareId: number;
    year: number;
    isLoading: boolean;
    isNew: boolean;

    constructor(private route: ActivatedRoute, private router: Router, private registerService: Register5Service) { }

    ngOnInit(): void {
        this.isLoading = true;

        this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);
        this.companyId = parseInt(this.route.snapshot.params['companyid'], 10);
        this.shareId = parseInt(this.route.snapshot.params['shareid'], 10);
        this.year = parseInt(this.route.snapshot.params['year'], 10);
        this.registerId = parseInt(this.route.snapshot.params['registerid'], 10);

        if (this.registerId == 0) {
            this.model = new Register5ViewModel();
            this.model.Year = this.year;
            this.model.OwnerProjectCompanyId = this.shareId;
            this.model.Type = RegisterType.Register5;
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